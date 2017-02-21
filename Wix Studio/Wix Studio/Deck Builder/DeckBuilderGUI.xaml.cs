using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wix_Studio.WixCardFiles;

namespace Wix_Studio.Deck_Builder
{
    /// <summary>
    /// Interaction logic for DeckBuilderGUI.xaml
    /// </summary>
    public partial class DeckBuilderGUI : Window
    {
        CardCollection cardCollection;

        List<WixossCard> currentSetCards = new List<WixossCard>();
        private Point LastMousePos = new Point(-1 , -1);

        public DeckBuilderGUI()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender , TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CardSelected(object sender , SelectionChangedEventArgs e)
        {
            if ( ImageList.SelectedIndex == -1 )
                return;

            //selectCard(ImageList.SelectedIndex);
        }

        private void selectCard(int selectedIndex, System.Windows.Controls.ListView listView)
        {
            WixossCard selectedCard = (WixossCard)listView.Items[selectedIndex];
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

        private void ImageList_MouseMove(object sender , System.Windows.Input.MouseEventArgs e)
        {
            int selectedIdex = this.GetCurrentIndex(e.GetPosition, (System.Windows.Controls.ListView) sender);
            if ( selectedIdex == -1 )
                return;

            selectCard(selectedIdex, (System.Windows.Controls.ListView)sender);

        }

        private int GetCurrentIndex(GetPositionDelegate getPosition, System.Windows.Controls.ListView listview)
        {
            int index = -1;
            for ( int i = 0; i < listview.Items.Count; ++i )
            {
                System.Windows.Controls.ListViewItem item = GetListViewItem(i, listview);
                if ( this.IsMouseOverTarget(item , getPosition) )
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private bool IsMouseOverTarget(Visual target , GetPositionDelegate getPosition)
        {
            if ( target == null )
                return false;

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = getPosition((IInputElement)target);
            return bounds.Contains(mousePos);
        }

        delegate Point GetPositionDelegate(IInputElement element);

        System.Windows.Controls.ListViewItem GetListViewItem(int index, System.Windows.Controls.ListView listView)
        {
            if ( listView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated )
                return null;

            return listView.ItemContainerGenerator.ContainerFromIndex(index) as System.Windows.Controls.ListViewItem;
        }

        private void SearchBtn_Click(object sender , RoutedEventArgs e)
        {
            
            WixCardSearchModel searchModel = new WixCardSearchModel();

            searchModel.cardEffect = CardEffect.Text;
            searchModel.cardName = CardName.Text;

            searchModel.MinLevel = Convert.ToInt32(MinLevel.Text == "" ? "0" : MinLevel.Text);
            searchModel.MaxLevel = Convert.ToInt32(MaxLevel.Text == "" ? "0" : MaxLevel.Text);
            searchModel.MinPower = Convert.ToInt32(MinPower.Text == "" ? "0" : MinPower.Text);
            searchModel.MaxPower = Convert.ToInt32(MaxPower.Text == "" ? "0" : MaxPower.Text);

            searchModel.Color = CardColor.ComboBox1.Text == "" ? Wix_Studio.CardColor.NoColor : (Wix_Studio.CardColor)Enum.Parse(typeof(Wix_Studio.CardColor) , CardColor.ComboBox1.Text);
            searchModel.Type = CardType.ComboBox1.Text == "" ? Wix_Studio.CardType.NoType : (Wix_Studio.CardType)Enum.Parse(typeof(Wix_Studio.CardType) , CardType.ComboBox1.Text);
            searchModel.Timing = CardTiming.ComboBox1.Text == "" ? Wix_Studio.CardTiming.NoTiming : (Wix_Studio.CardTiming)Enum.Parse(typeof(Wix_Studio.CardTiming) , CardTiming.ComboBox1.Text);

            currentSetCards = new List<WixossCard>(WixCardSearchService.FindCards(searchModel , SortBy.Color , WixCardFiles.SortOrder.ASC).Distinct().ToList());
            String test = "";
            ResultsList.ItemsSource = currentSetCards;
        }
    }
}
