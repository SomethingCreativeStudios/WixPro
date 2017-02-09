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
using System.Windows.Shapes;

namespace Wix_Studio
{
    /// <summary>
    /// Interaction logic for DeckViewer.xaml
    /// </summary>
    public partial class DeckViewer : Window
    {
        CardCollection cardCollection;

        List<WixossCard> currentSetCards = new List<WixossCard>();
        List<WixossCard> searchResults = new List<WixossCard>();

        public DeckViewer()
        {
            cardCollection = new CardCollection();
            InitializeComponent();
            LoadSets();
            ImageList.SelectionChanged += CardSelected;
        }

        private void CardSelected(object sender , SelectionChangedEventArgs e)
        {
            if ( ImageList.SelectedIndex == -1 )
                return;

            WixossCard selectedCard = (WixossCard)ImageList.Items[ImageList.SelectedIndex];
            try
            {
                Uri uriImage = new Uri(CardCollection.basePath + "\\setimages\\" + selectedCard.CardSet + "\\" + selectedCard.CardNumberInSet + ".jpg");
                cardImageBox.Source = new BitmapImage(uriImage);
            }
            catch ( Exception ex )
            {
                Uri uriImage = new Uri(selectedCard.CardUrl);
                cardImageBox.Source = new BitmapImage(uriImage);
            }
            cardEffectBox.Text = selectedCard.CardEffect;
            CardNameBox.Text = selectedCard.CardName;
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
                    CardCollection.basePath = fbd.SelectedPath;
                    Wix_Studio.Properties.Settings.Default.basePath = CardCollection.basePath;
                }
            } else
            {
                CardCollection.basePath = basePath;
            }

            Wix_Studio.Properties.Settings.Default.Save();
        }

        private void setComboBox_SelectionChanged(object sender , SelectionChangedEventArgs e)
        {
            String setName = "\\" + setComboBox.SelectedItem;
            currentSetCards = new List<WixossCard>(cardCollection.GetSet(setName)).Distinct().ToList();

            ImageList.ItemsSource = currentSetCards;
        }

        private void CardNameSearchBox_TextChanged(object sender , TextChangedEventArgs e)
        {
            String currentSearchTerm = CardNameSearchBox.Text;
            if ( currentSearchTerm == "" )
            {
                ImageList.ItemsSource = currentSetCards;
                return;
            }

            ImageList.ItemsSource = currentSetCards.Where(item => item.CardName.ToLower().Contains(currentSearchTerm.ToLower())).ToList();
        }
    }
}
