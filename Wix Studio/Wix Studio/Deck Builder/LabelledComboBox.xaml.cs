using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for LabelledComboBox.xaml
    /// </summary>
    public partial class LabelledComboBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty
       .Register("Label" ,
               typeof(string) ,
               typeof(LabelledComboBox) ,
               new FrameworkPropertyMetadata("Unnamed Label"));

        public static readonly DependencyProperty TextProperty = DependencyProperty
            .Register("Text" ,
                    typeof(string) ,
                    typeof(LabelledComboBox) ,
                    new FrameworkPropertyMetadata("" , FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty
            .Register("ItemsSource" , 
            typeof(IEnumerable) ,
           typeof(LabelledComboBox) , 
           new PropertyMetadata(null));


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

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty , value); }
        }


        public LabelledComboBox()
        {
            InitializeComponent();
            Root.DataContext = this;
        }
    }
}
