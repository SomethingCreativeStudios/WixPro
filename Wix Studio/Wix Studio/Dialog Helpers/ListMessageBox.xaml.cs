using System;
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
using System.Windows.Shapes;

namespace Wix_Studio.Dialog_Helpers
{
    /// <summary>
    /// Interaction logic for ListMessageBox.xaml
    /// </summary>
    public partial class ListMessageBox : Window
    {
        public ListMessageBox()
        {
            InitializeComponent();
        }

        public ListMessageBox(List<string> items , string title , InputType inputType = InputType.Text)
        {
            InitializeComponent();
            this.Title = title;
            foreach ( var item in items )
            {
                this.DeckBox.Items.Add(item);
            }
        }

        public static string Show(List<String> items , string title , InputType inputType = InputType.Text)
        {
            ListMessageBox inst = new ListMessageBox(items , title , inputType);
            inst.ShowDialog();

            if ( inst.DialogResult == true )
                return inst.InputText;

            return null;
        }

        public string InputText
        {
            get { return (String)DeckBox.SelectedItem; }
        }

        public bool Canceled { get; set; }

        private void BtnCancel_Click(object sender , System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
            Canceled = true;
            Close();
        }

        private void BtnOk_Click(object sender , System.Windows.RoutedEventArgs e)
        {
            if ( DeckBox.SelectedIndex != -1 )
            {
                DialogResult = true;
                Canceled = false;
                Close();
            } else
            {
                System.Windows.Forms.MessageBox.Show("Please Select A Deck");
            }
        }
    }
}
