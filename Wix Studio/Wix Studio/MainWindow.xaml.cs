using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wix_Studio.Card_GUI;

namespace Wix_Studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        CardCollection cardCollection;

        List<WixossCard> currentSetCards = new List<WixossCard>();
        public MainWindow()
        {
            cardCollection = new CardCollection();
            InitializeComponent();
            //LoadBasePath();
            LoadSets();
        }

        private void LoadSets()
        {
            AutoCompleteStringCollection autoStringCollection = new AutoCompleteStringCollection();
            foreach ( String set in cardCollection.GetAllSets() )
            {
                autoStringCollection.Add(set);
                setComboBox.Items.Add(set);
            }

            setComboBox.DataContext = autoStringCollection;
        }

        private void LoadBasePath()
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            String basePath = Wix_Studio.Properties.Settings.Default.basePath;
            if ( basePath == "" )
            {
                if ( fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                {
                    CardCollection.baseSetPath = fbd.SelectedPath;
                    Wix_Studio.Properties.Settings.Default.basePath = CardCollection.baseSetPath;
                }
            } else
            {
                CardCollection.baseSetPath = basePath;
            }

            Wix_Studio.Properties.Settings.Default.Save();
        }

        private void setComboBox_SelectionChanged(object sender , SelectionChangedEventArgs e)
        {
            String setName = "\\" + setComboBox.SelectedItem;
            currentSetCards = new List<WixossCard>(cardCollection.GetSet(setName));
            cardCombBox.Items.Clear();

            AutoCompleteStringCollection autoStringCollection = new AutoCompleteStringCollection();
            foreach ( WixossCard wixossCard in currentSetCards )
            {
                autoStringCollection.Add(wixossCard.CardName);
                cardCombBox.Items.Add(wixossCard.CardName);
            }

            cardCombBox.DataContext = autoStringCollection;
        }

        private void cardCombBox_SelectionChanged(object sender , SelectionChangedEventArgs e)
        {
            if ( cardCombBox.SelectedIndex == -1 )
                return;

            try
            {
                Uri uriImage = new Uri(CardCollection.baseSetPath + "\\setimages\\" + currentSetCards[cardCombBox.SelectedIndex].CardSet + "\\" + currentSetCards[cardCombBox.SelectedIndex].CardNumberInSet + ".jpg");
                cardImageBox.Source = new BitmapImage(uriImage);
            }
            catch ( Exception ex )
            {
                Uri uriImage = new Uri(CardCollection.baseSetPath + "setimages\\general\\Card Not Found.jpg");
                cardImageBox.Source = new BitmapImage(uriImage);
            }
            cardEffectBox.Text = currentSetCards[cardCombBox.SelectedIndex].CardEffect;
            LoadCardInfo(currentSetCards[cardCombBox.SelectedIndex]);
        }

        private void LoadCardInfo(WixossCard selectedCard)
        {
            String cardColorStr = "";
            String cardCostStr = "";
            String cardTimingStr = "";
            String classStr = "";

            foreach ( var cardColor in selectedCard.Color )
            {
                cardColorStr += cardColor + " ";
            }

            foreach ( var cardCost in selectedCard.Cost )
            {
                cardCostStr += "(Color: " + cardCost.color + ", Cost: " + cardCost.numberPerColor + ") ";
            }

            foreach ( var cardTiming in selectedCard.Timing )
            {
                cardTimingStr += "[" + cardTiming + "]";
            }

            foreach ( var cardClass in selectedCard.Class )
            {
                classStr += "{" + cardClass + "}";
            }

            cardNameBlock.Text = selectedCard.CardName;
            cardColorBlock.Text = cardColorStr;
            cardTypeBlock.Text = selectedCard.Type.ToString();
            cardCostBlock.Text = cardCostStr;
            cardLimingtConditionBlock.Text = selectedCard.LimitingCondition;
            cardTimingBlock.Text = cardTimingStr;
            cardLevelBlock.Text = selectedCard.Level.ToString();
            cardLimitBlock.Text = selectedCard.Limit.ToString();
            cardPowerBlock.Text = selectedCard.Power.ToString();
            classBlock.Text = classStr;
            hasGuardBlock.Text = selectedCard.Guard.ToString();
            isMultiEnerBlock.Text = selectedCard.MultiEner.ToString();
            hasLifeBurstBlock.Text = selectedCard.LifeBurst.ToString();
            cardUrlBlock.Text = selectedCard.CardUrl;
            imageUrlBlock.Text = selectedCard.ImageUrl;
            
        }

        private void button_Click(object sender , RoutedEventArgs e)
        {
            //Card Urls
            String cardWithDoubleCost = "http://selector-wixoss.wikia.com/wiki/Dragon-Guided_Wave";
            String artsWithBet = "http://selector-wixoss.wikia.com/wiki/Life_Free_from_Worldly_Cares";
            String lrig = "http://selector-wixoss.wikia.com/wiki/Mel%3DAnge";
            String cardWithColorInEffect = "http://selector-wixoss.wikia.com/wiki/Code_Eat_Mayo";
            String cardWithGuard = "http://selector-wixoss.wikia.com/wiki/Servant_D";
            String cardNoEffectcardWthBet = "http://selector-wixoss.wikia.com/wiki/Life_Free_from_Worldly_Cares";
            String cardWithTwoCosts = "http://selector-wixoss.wikia.com/wiki/Iona,_Full/Maiden";
            String cardNoEffect = "http://selector-wixoss.wikia.com/wiki/Code_Eat_Oyako";
            String cardDifferentImg = "http://selector-wixoss.wikia.com/wiki/Code_Piruluk_Alpha";

            //cardMaker.GetCardFromUrl(cardNoEffect);

            CardUpdater cardUpdater = new CardUpdater(true, cardCollection);
            cardUpdater.Show();
        }

        private void ViewDeckViewer(object sender , RoutedEventArgs e)
        {
            /* DeckViewer deckViewer = new DeckViewer();
             deckViewer.Show();*/
            Deck_Builder.DeckBuilderGUI deck = new Deck_Builder.DeckBuilderGUI();
            deck.Show();
        }

        private void updateImagesBtn_Click(object sender , RoutedEventArgs e)
        {
            CardUpdater cardUpdater = new CardUpdater();
            cardUpdater.UpdateImages();
            cardUpdater.Show();
        }
    }
}
