using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Wix_Studio
{
    /// <summary>
    /// Interaction logic for DeckViewer.xaml
    /// </summary>
    public partial class DeckViewer : Window
    {
        CardCollection cardCollection;

        List<WixossCard> currentSetCards = new List<WixossCard>();
        private Point LastMousePos = new Point(-1 , -1);

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

            selectCard(ImageList.SelectedIndex);
        }

        private void selectCard(int selectedIndex)
        {
            WixossCard selectedCard = (WixossCard)ImageList.Items[selectedIndex];
            try
            {
                Uri uriImage = new Uri(CardCollection.baseSetPath + "\\setimages\\" + selectedCard.Id + ".jpg");
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
            int selectedIdex = this.GetCurrentIndex(e.GetPosition);
            if ( selectedIdex == -1 )
                return;

            selectCard(selectedIdex);

        }

        private int GetCurrentIndex(GetPositionDelegate getPosition)
        {
            int index = -1;
            for ( int i = 0; i < ImageList.Items.Count; ++i )
            {
                System.Windows.Controls.ListViewItem item = GetListViewItem(i);
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
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = getPosition((IInputElement)target);
            return bounds.Contains(mousePos);
        }

        delegate Point GetPositionDelegate(IInputElement element);

        System.Windows.Controls.ListViewItem GetListViewItem(int index)
        {
            if ( ImageList.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated )
                return null;

            return ImageList.ItemContainerGenerator.ContainerFromIndex(index) as System.Windows.Controls.ListViewItem;
        }
    }
}
