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

        List<WixossCard> resultCards = new List<WixossCard>();
        List<WixossCard> currentDeck = new List<WixossCard>();
        List<WixossCard> lrigDeck = new List<WixossCard>();

        private Point LastMousePos = new Point(-1 , -1);

        public DeckBuilderGUI()
        {
            InitializeComponent();
            DeckList.ItemsSource = currentDeck;
        }

        private void NumberValidationTextBox(object sender , TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CardSelected(object sender , SelectionChangedEventArgs e)
        {
            if ( DeckList.SelectedIndex == -1 )
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

        private void SearchBtn_Click(object sender , RoutedEventArgs e)
        {

            WixCardSearchModel searchModel = new WixCardSearchModel();

            searchModel.cardEffect = CardEffect.Text;
            searchModel.cardName = CardName.Text;

            searchModel.MinLevel = Convert.ToInt32(MinLevel.Text == "" ? "0" : MinLevel.Text);
            searchModel.MaxLevel = Convert.ToInt32(MaxLevel.Text == "" ? "0" : MaxLevel.Text);
            searchModel.MinPower = Convert.ToInt32(MinPower.Text == "" ? "0" : MinPower.Text);
            searchModel.MaxPower = Convert.ToInt32(MaxPower.Text == "" ? "0" : MaxPower.Text);
            searchModel.LifeBurst = LifeBurst.IsChecked;
            searchModel.Guard = Guard.IsChecked;
            searchModel.MultiEner = MultiEner.IsChecked;

            searchModel.Color = CardColor.ComboBox1.Text == "" ? Wix_Studio.CardColor.NoColor : (Wix_Studio.CardColor)Enum.Parse(typeof(Wix_Studio.CardColor) , CardColor.ComboBox1.Text);
            searchModel.Type = CardType.ComboBox1.Text == "" ? Wix_Studio.CardType.NoType : (Wix_Studio.CardType)Enum.Parse(typeof(Wix_Studio.CardType) , CardType.ComboBox1.Text);
            searchModel.Timing = CardTiming.ComboBox1.Text == "" ? Wix_Studio.CardTiming.NoTiming : (Wix_Studio.CardTiming)Enum.Parse(typeof(Wix_Studio.CardTiming) , CardTiming.ComboBox1.Text);

            resultCards = new List<WixossCard>(WixCardSearchService.FindCards(searchModel , SortBy.Color , WixCardFiles.SortOrder.ASC).Distinct().ToList());
            ResultsList.ItemsSource = resultCards;
        }

        #region Deck View
        private void setComboBox_SelectionChanged(object sender , SelectionChangedEventArgs e)
        {
            String setName = "\\" + setComboBox.SelectedItem;
            resultCards = new List<WixossCard>(cardCollection.GetSet(setName)).Distinct().ToList();

            DeckList.ItemsSource = resultCards;
        }
        #endregion

        private void ListView_MouseMove(object sender , System.Windows.Input.MouseEventArgs e)
        {

            int selectedIndex = handleListViewMouseMove(sender , e);

            if ( selectedIndex == -1 )
                return;

            if ( e.LeftButton == MouseButtonState.Pressed )
            {
                System.Windows.Controls.ListView listView = sender as System.Windows.Controls.ListView;

                System.Windows.Forms.DataObject dragData = new System.Windows.Forms.DataObject("selectedIndex" , selectedIndex);
                DragDrop.DoDragDrop(listView , resultCards[selectedIndex] , System.Windows.DragDropEffects.Move);
            }

        }

        private void DeckView_MouseMove(object sender , System.Windows.Input.MouseEventArgs e)
        {
            int selectedIndex = handleListViewMouseMove(sender , e);
        }

        private int handleListViewMouseMove(object sender , System.Windows.Input.MouseEventArgs e)
        {
            int selectedIndex = this.GetCurrentIndex(e.GetPosition , (System.Windows.Controls.ListView)sender);
            if ( selectedIndex == -1 )
                return -1;

            selectCard(selectedIndex , (System.Windows.Controls.ListView)sender);

            return selectedIndex;
        }

        #region Mouse Over ListView

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

        #endregion


        #region Drag And Drop
        private void ListView_Drop(object sender , System.Windows.DragEventArgs e)
        {
            if ( e.Data.GetData(typeof(WixossCard)) != null )
            {
                System.Windows.Controls.ListView listView = sender as System.Windows.Controls.ListView;
                WixossCard cardToAdd = (WixossCard)e.Data.GetData(typeof(WixossCard));
                if ( listView.Name == DeckList.Name )
                {
                    if ( canAddCard(cardToAdd , currentDeck , false) )
                    {
                        currentDeck.Add(cardToAdd);
                        DeckList.ItemsSource = currentDeck;
                        DeckList.Items.Refresh();
                    }
                }

                if ( listView.Name == LRIGDeckList.Name )
                {
                    if ( canAddCard(cardToAdd , lrigDeck , true) )
                    {
                        lrigDeck.Add(cardToAdd);
                        LRIGDeckList.ItemsSource = lrigDeck;
                        LRIGDeckList.Items.Refresh();
                    }
                }
            }
        }

        private void ListView_DragEnter(object sender , System.Windows.DragEventArgs e)
        {
            if ( e.Data.GetData(typeof(WixossCard)) == null || sender == e.Source )
            {
                e.Effects = System.Windows.DragDropEffects.None;
            } else if ( e.Data.GetData(typeof(WixossCard)) != null )
            {
                e.Effects = System.Windows.DragDropEffects.Move;
            }
        }

        private void DeckList_MouseRightButtonDown(object sender , MouseButtonEventArgs e)
        {


            if (e.RightButton == MouseButtonState.Pressed )
            {
                var item = sender as System.Windows.Controls.ListViewItem;
                System.Windows.Controls.ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as System.Windows.Controls.ListView;
                int selectedIndex = handleListViewMouseMove(listView , e);
                if ( selectedIndex != -1 )
                {
                    if ( listView.Name == DeckList.Name )
                    {
                        currentDeck.RemoveAt(selectedIndex);
                        DeckList.ItemsSource = currentDeck;
                        DeckList.Items.Refresh();
                    }
                    if ( listView.Name == LRIGDeckList.Name )
                    {
                        lrigDeck.RemoveAt(selectedIndex);
                        LRIGDeckList.ItemsSource = lrigDeck;
                        LRIGDeckList.Items.Refresh();
                    }
                }
            }
        }

        public static T FindParent<T>(FrameworkElement element) where T : FrameworkElement
        {
            FrameworkElement parent = LogicalTreeHelper.GetParent(element) as FrameworkElement;

            while ( parent != null )
            {
                T correctlyTyped = parent as T;
                if ( correctlyTyped != null )
                    return correctlyTyped;
                else
                    return FindParent<T>(parent);
            }

            return null;
        }
        #endregion

        private bool canAddCard(WixossCard cardToAdd , List<WixossCard> deck , bool lrigDeck)
        {
            //Common Rule no more than 4 per card
            if ( WixossDeckHelper.totalCountOfCard(cardToAdd,deck) == 4 )
                return false;

            if ( lrigDeck ) //lrig special rules
            {
                if ( deck.Count == 10 )
                    return false;

                if ( !WixossDeckHelper.cardAllowedILRIGDeck(cardToAdd) )
                {
                    System.Windows.Forms.MessageBox.Show("Not allowed type(" + cardToAdd.Type + ") in LRIG deck");
                    return false;
                }
            } else // normal deck special rules
            {
                if ( deck.Count == 40 )
                    return false;

                if ( !WixossDeckHelper.cardAllowedInMainDeck(cardToAdd) )
                {
                    System.Windows.Forms.MessageBox.Show("Not allowed type(" + cardToAdd.Type + ") in main deck");
                    return false;
                }

                if ( cardToAdd.LifeBurst && WixossDeckHelper.totalCountOfLifeBurst(deck) == 20 )
                {
                    System.Windows.Forms.MessageBox.Show("Can Only Have 20 lifebursts per deck");
                    return false;
                }
            }

            return true;
        }
    }
}
