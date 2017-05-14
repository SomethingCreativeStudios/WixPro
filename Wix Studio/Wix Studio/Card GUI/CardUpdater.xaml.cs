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
using Wix_Studio.WixCardFiles;

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

            String cardList = "";

            foreach ( var cardSet in cardMaker.GetAllSets(updateDeckUrl) )
            {
               // cardSets.Add(cardSet.Key , cardSet.Value);
            }

            /*foreach ( var cardSet in cardMaker.GetBoosterSets() )
            {
                cardSets.Add(cardSet.Key , cardSet.Value);
                cardList += "cardSets.add(\"" + cardSet.Key + "\",\"" + cardSet.Value + "\");\n";
            }*/

            //For Speed and testing
            cardSets.Add("WX-01 Served Selector" , "http://selector-wixoss.wikia.com/wiki/WX-01_Served_Selector");
           /* cardSets.Add("WX-02 Stirred Selector" , "http://selector-wixoss.wikia.com/wiki/WX-02_Stirred_Selector");
            cardSets.Add("WX-03 Spread Selector" , "http://selector-wixoss.wikia.com/wiki/WX-03_Spread_Selector");
            cardSets.Add("WX-04 Infected Selector" , "http://selector-wixoss.wikia.com/wiki/WX-04_Infected_Selector");
            cardSets.Add("WX-05 Beginning Selector" , "http://selector-wixoss.wikia.com/wiki/WX-05_Beginning_Selector");
            cardSets.Add("WX-06 Fortune Selector" , "http://selector-wixoss.wikia.com/wiki/WX-06_Fortune_Selector");
            cardSets.Add("WX-07 Next Selector" , "http://selector-wixoss.wikia.com/wiki/WX-07_Next_Selector");
            cardSets.Add("WX-08 Incubate Selector" , "http://selector-wixoss.wikia.com/wiki/WX-08_Incubate_Selector");
            cardSets.Add("WX-09 Reacted Selector" , "http://selector-wixoss.wikia.com/wiki/WX-09_Reacted_Selector");
            cardSets.Add("WX-10 Chained Selector" , "http://selector-wixoss.wikia.com/wiki/WX-10_Chained_Selector");
            cardSets.Add("WX-11 Destructed Selector" , "http://selector-wixoss.wikia.com/wiki/WX-11_Destructed_Selector");
            cardSets.Add("WX-12 Replied Selector" , "http://selector-wixoss.wikia.com/wiki/WX-12_Replied_Selector");
            cardSets.Add("WX-13 Unfeigned Selector" , "http://selector-wixoss.wikia.com/wiki/WX-13_Unfeigned_Selector");
            cardSets.Add("WX-14 Succeed Selector" , "http://selector-wixoss.wikia.com/wiki/WX-14_Succeed_Selector");
            cardSets.Add("WX-15 Incited Selector" , "http://selector-wixoss.wikia.com/wiki/WX-15_Incited_Selector");
            cardSets.Add("WX-16 Decided Selector" , "http://selector-wixoss.wikia.com/wiki/WX-16_Decided_Selector");
            cardSets.Add("WX-17 Exposed Selector" , "http://selector-wixoss.wikia.com/wiki/WX-17_Exposed_Selector");
            cardSets.Add("WX-18 Conflated Selector" , "http://selector-wixoss.wikia.com/wiki/WX-18_Conflated_Selector");
            cardSets.Add("WX-19 Unsolved Selector" , "http://selector-wixoss.wikia.com/wiki/WX-19_Unsolved_Selector");
            */




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
                if ( e.ProgressPercentage > setsProgressBar.Maximum )
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
            String fileName = AuditLog.logPath + "Images Updated.txt";
            AuditLog.clear("Images Updated.txt");
            BackgroundWorker worker = (BackgroundWorker)sender;



            int setCount = 0;
            foreach ( var setName in cardCollection.GetAllSets() )
            {
                setCount++;
                int cardCount = 1;
                List<WixossCard> cardList = cardCollection.GetSet(setName);
                worker.ReportProgress(cardList.Count , "Card Set Count");
                worker.ReportProgress(setCount , "Card Set Value");
                if ( !Directory.Exists(CardCollection.setImages + setName) )
                {
                    Directory.CreateDirectory(CardCollection.setImages + setName);
                }
                foreach ( var wixCard in cardList )
                {
                    cardCount++;
                    wixCard.CardSet = setName;
                    if ( !File.Exists(wixCard.CardImagePath) )
                    {
                        
                        using ( WebClient client = new WebClient() )
                        {
                            if ( wixCard.CardNumberInSet == null || wixCard.CardNumberInSet.Contains("???") )
                                wixCard.CardNumberInSet = wixCard.CardName;

                            String newFilePath = CardCollection.setImages + wixCard.CardSet + "\\" + wixCard.CardNumberInSet + ".jpg";
                            if ( wixCard.ImageUrl != null )
                            {
                                String urlName = wixCard.ImageUrl;
                                client.DownloadFileAsync(new Uri(urlName) , newFilePath , wixCard);
                                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                            }
                        }

                    }
                    worker.ReportProgress(cardCount , "Card Value");
                }
                try {
                    cardCollection.SaveSet(setName , cardList);
                }catch(Exception ex )
                {
                    Debug.WriteLine(ex.InnerException);
                }

            }

            Process.Start("notepad.exe" , fileName);
        }

        private void Client_DownloadFileCompleted(object sender , AsyncCompletedEventArgs e)
        {
            WixossCard wixCard = e.UserState as WixossCard;
            String newLine = Environment.NewLine;
            String hadError = e.Error == null ? newLine + "Passed " : newLine + e.Error.InnerException.Message + newLine;
            AuditLog.log( hadError + "Card Name: " + wixCard.CardName + newLine + "\tSet Name: " + wixCard.CardSet + newLine + "\tCard Number: " + wixCard.CardNumberInSet , "Images Updated.txt");

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

                    //for ( int i = 0; i < cardItem.Value; i++ )
                    {
                        setCards.Add(theCard);
                        WixCardService.Create(theCard);
                    }
                }

                worker.ReportProgress(-1 , "Saving Set(" + cardSet.Key + ") To Disk...");
               // cardCollection.SaveSet(cardSet.Key , setCards);
            }
        }
    }
}
