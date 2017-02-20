using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wix_Studio.Deck_Builder
{
    /// <summary>
    /// Interaction logic for LabelledTextBox.xaml
    /// </summary>
    public partial class LabelledTextBox
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty
        .Register("Label" ,
                typeof(string) ,
                typeof(LabelledTextBox) ,
                new FrameworkPropertyMetadata("Unnamed Label"));

        public static readonly DependencyProperty TextProperty = DependencyProperty
            .Register("Text" ,
                    typeof(string) ,
                    typeof(LabelledTextBox) ,
                    new FrameworkPropertyMetadata("" , FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty BoxTypeProperty = DependencyProperty
            .Register("BoxType" ,
                    typeof(TextType) ,
                    typeof(LabelledTextBox) ,
                    new FrameworkPropertyMetadata(TextType.Text , FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public LabelledTextBox()
        {
            InitializeComponent();
            Root.DataContext = this;
        }

        private void NumberValidationTextBox(object sender , TextCompositionEventArgs e)
        {
            if ( BoxType == TextType.Number )
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty , value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty , value); }
        }

        public TextType BoxType
        {
            get { return (TextType)GetValue(BoxTypeProperty); }
            set { SetValue(BoxTypeProperty , value); }
        }
    }

    public class LayoutGroup : StackPanel
    {
        public LayoutGroup()
        {
            Grid.SetIsSharedSizeScope(this , true);
        }
    }

    public enum TextType
    {
        Text,
        Number
    }
}
