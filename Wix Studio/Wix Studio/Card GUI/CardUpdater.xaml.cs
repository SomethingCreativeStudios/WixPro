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

        private void updateCardsWork(object sender , DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            int setCount = 0;
            foreach ( var cardSet in cardSets )
            {
                setCount++;

                Dictionary<String, int> cardList = cardMaker.GetCardsFromUrl(cardSet.Value);
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
    }
}
