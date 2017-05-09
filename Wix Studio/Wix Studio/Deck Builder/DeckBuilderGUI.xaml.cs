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
using Wix_Studio.Dialog_Helpers;
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
        WixossDeck currentDeck = new WixossDeck();

        private Point LastMousePos = new Point(-1 , -1);

        public DeckBuilderGUI()
        {
            InitializeComponent();
            DeckList.ItemsSource = currentDeck.MainDeck;
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

        private void selectCard(int selectedIndex , System.Windows.Controls.ListView listView)
        {
            WixossCard selectedCard = (WixossCard)listView.Items[selectedIndex];
            try
            {
                Uri uriImage = new Uri(CardCollection.baseSetPath + "\\setimages\\" + selectedCard.CardSet + "\\" + selectedCard.CardNumberInSet + ".jpg");
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

        private void SearchBtn_Click(object sender , RoutedEventArgs e)
        {

            WixCardSearchModel searchModel = new WixCardSearchModel();

            searchModel.cardEffect = CardEffect.Text;
            searchModel.cardName = CardName.Text;
            searchModel.setName = SetName.Text;

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

            resultCards = new List<WixossCard>(WixCardSearchService.Search(searchModel , SortBy.Color , WixCardFiles.SortOrder.ASC).Distinct().ToList());
            ResultsList.ItemsSource = resultCards;
        }

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

        private int GetCurrentIndex(GetPositionDelegate getPosition , System.Windows.Controls.ListView listview)
        {
            int index = -1;
            for ( int i = 0; i < listview.Items.Count; ++i )
            {
                System.Windows.Controls.ListViewItem item = GetListViewItem(i , listview);
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

        System.Windows.Controls.ListViewItem GetListViewItem(int index , System.Windows.Controls.ListView listView)
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
                    if ( currentDeck.canAddCard(cardToAdd , DeckType.Main) )
                    {
                        currentDeck.AddToDeck(cardToAdd , DeckType.Main);
                        DeckList.ItemsSource = currentDeck.MainDeck;
                        DeckList.Items.Refresh();
                    }
                }

                if ( listView.Name == LRIGDeckList.Name )
                {
                    if ( currentDeck.canAddCard(cardToAdd , DeckType.LRIG) )
                    {
                        currentDeck.AddToDeck(cardToAdd , DeckType.LRIG);
                        LRIGDeckList.ItemsSource = currentDeck.LRIGDeck;
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


            if ( e.RightButton == MouseButtonState.Pressed )
            {
                var item = sender as System.Windows.Controls.ListViewItem;
                System.Windows.Controls.ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as System.Windows.Controls.ListView;
                int selectedIndex = handleListViewMouseMove(listView , e);
                if ( selectedIndex != -1 )
                {
                    if ( listView.Name == DeckList.Name )
                    {
                        currentDeck.RemoveAt(selectedIndex , DeckType.Main);
                        DeckList.ItemsSource = currentDeck.MainDeck;
                        DeckList.Items.Refresh();
                    }
                    if ( listView.Name == LRIGDeckList.Name )
                    {
                        currentDeck.RemoveAt(selectedIndex , DeckType.LRIG);
                        LRIGDeckList.ItemsSource = currentDeck.LRIGDeck;
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

        private void loadDeckBtn_Click(object sender , RoutedEventArgs e)
        {

        }

        private void saveDeckBtn_Click(object sender , RoutedEventArgs e)
        {
            String deckName = InputMessageBox.Show("Enter Deck Name" , "Save Deck");

            if ( deckName != null )
            {
                if ( currentDeck.isLegalDeck(true) )
                    WixossDeck.SaveDeck(deckName , currentDeck);
            }
        }
    }
}
