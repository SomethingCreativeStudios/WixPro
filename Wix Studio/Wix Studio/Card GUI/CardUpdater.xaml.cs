using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using Wix_Studio.Card_Parser;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Wix_Studio.Card_GUI
{
    /// <summary>
    /// Interaction logic for CardUpdater.xaml
    /// </summary>
    public partial class CardUpdater : Window
    {
        CardCollection cardCollection;
        CardMaker cardMaker = new CardMaker();
        Dictionary<String , String> cardSets = new Dictionary<string , string>();

        public CardUpdater()
        {
            InitializeComponent();
        }

        public CardUpdater(Boolean cleanUpdate , CardCollection cardCollection)
        {
            InitializeComponent();
            String updateDeckUrl = "http://selector-wixoss.wikia.com/wiki/Category:Pre-built_Decks?display=page";
            String updateSetUrl = "http://selector-wixoss.wikia.com/wiki/Category:Booster_Sets?display=page";
            this.cardCollection = cardCollection;

            setsProgressBar.Value = 0;
            setsProgressBar.Minimum = 0;

            foreach ( var cardSet in cardMaker.GetAllSets(updateDeckUrl) )
            {
                cardSets.Add(cardSet.Key , cardSet.Value);
            }

            foreach ( var cardSet in cardMaker.GetAllSets(updateSetUrl) )
            {
                cardSets.Add(cardSet.Key , cardSet.Value);
            }

            cardsProgressBar.Value = 0;
            cardsProgressBar.Minimum = 0;
            setsProgressBar.Maximum = cardSets.Values.Count;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += updateCardsWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        public void UpdateImages()
        {
            CardCollection cardCollection = new CardCollection();

            setsProgressBar.Value = 0;
            setsProgressBar.Minimum = 0;

            cardsProgressBar.Value = 0;
            cardsProgressBar.Minimum = 0;
            setsProgressBar.Maximum = cardCollection.GetAllSets().Count;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += updateImagesWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();

        }
        private void Worker_ProgressChanged(object sender , ProgressChangedEventArgs e)
        {
            if ( (string)e.UserState == "Card Set Value" )
            {
                setsProgressBar.Value = e.ProgressPercentage;
                setBlock.Text = "Set " + setsProgressBar.Value + " / " + setsProgressBar.Maximum;
                if ( e.ProgressPercentage == setsProgressBar.Maximum )
                {
                    System.Windows.Forms.MessageBox.Show("Update Done");
                    this.Close();
                }
            }

            if ( (string)e.UserState == "Card Set Count" )
            {
                cardsProgressBar.Maximum = e.ProgressPercentage;
            }

            if ( (string)e.UserState == "Card Value" )
            {
                cardsProgressBar.Value = e.ProgressPercentage;
                cardBlock.Text = "Card " + cardsProgressBar.Value + " / " + cardsProgressBar.Maximum;
            }

            if ( e.ProgressPercentage == -1 )
                setBlock.Text = (string)e.UserState;

        }

        private void updateImagesWork(object sender , DoWorkEventArgs e)
        {
            CardCollection cardCollection = new CardCollection();
            String fileName = CardCollection.logPath + "Images Updated.txt";
            BackgroundWorker worker = (BackgroundWorker)sender;

            int setCount = 0;
            foreach ( var setName in cardCollection.GetAllSets() )
            {
                setCount++;
                int cardCount = 1;
                List<WixossCard> cardList = cardCollection.GetSet(setName);
                worker.ReportProgress(cardList.Count , "Card Set Count");
                worker.ReportProgress(setCount , "Card Set Value");
                foreach ( var wixCard in cardList )
                {
                    cardCount++;
                    if ( !File.Exists(wixCard.CardImagePath) )
                    {

                        using ( WebClient client = new WebClient() )
                        {
                            String newFilePath = CardCollection.setImages + wixCard.CardSet + "\\" + wixCard.CardNumberInSet + ".jpg";
                            if ( wixCard.ImageUrl != null )
                            {
                                String urlName = "tET";//HttpUtility.UrlEncode("http://vignette2.wikia.nocookie.net/selector-wixoss/images/0/07/PR-066.jpg/revision/latest?cb=20141023063112"); ;
                                client.DownloadFileAsync(new Uri(urlName) , newFilePath , wixCard);
                                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                            }
                        }

                    }
                    worker.ReportProgress(cardCount , "Card Value");
                }

            }

            Process.Start("notepad.exe" , fileName);
        }

        private void Client_DownloadFileCompleted(object sender , AsyncCompletedEventArgs e)
        {
            WixossCard wixCard = e.UserState as WixossCard;
            String newLine = Environment.NewLine;
            String hadError = e.Error == null ? newLine + "Passed" : e.Error.Message + newLine;
            Audit( hadError + "Card Name: " + wixCard.CardName + newLine + "\tSet Name: " + wixCard.CardSet + newLine + "\tCard Number: " + wixCard.CardNumberInSet , "Images Updated.txt");

        }

        private void updateCardsWork(object sender , DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            int setCount = 0;
            foreach ( var cardSet in cardSets )
            {
                setCount++;

                Dictionary<String , int> cardList = cardMaker.GetCardsFromUrl(cardSet.Value);
                List<WixossCard> setCards = new List<WixossCard>();

                worker.ReportProgress(cardList.Values.Count , "Card Set Count");
                worker.ReportProgress(setCount , "Card Set Value");
                int cardCount = 0;
                foreach ( var cardItem in cardList )
                {
                    cardCount++;
                    worker.ReportProgress(cardCount , "Card Value");

                    WixossCard theCard = cardMaker.GetCardFromUrl(cardItem.Key);

                    for ( int i = 0; i < cardItem.Value; i++ )
                    {
                        setCards.Add(theCard);
                    }
                }

                worker.ReportProgress(-1 , "Saving Set(" + cardSet.Key + ") To Disk...");
                cardCollection.SaveSet(cardSet.Key , setCards);
            }
        }

        public void Audit(String message , String logFileName = "wixStudioLog.txt" , bool newline = true , bool clearLog = false)
        {
            String filePath = CardCollection.logPath + logFileName;
            StreamWriter writer = null;
            if ( !clearLog )
                writer = File.AppendText(filePath);
            else
                writer = File.CreateText(filePath);
            writer.WriteLine(message);
            writer.Close();
        }
    }
}
