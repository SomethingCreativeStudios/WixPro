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
    /// Interaction logic for InputMessageBox.xaml
    /// </summary>
    public partial class InputMessageBox : Window
    {
        public InputMessageBox()
        {
            InitializeComponent();
        }

        public InputMessageBox(string question , string title , InputType inputType = InputType.Text)
        {
            InitializeComponent();
            this.Title = title;
            this.TitleText = question;
        }

        public static string Show(string question ,string title, InputType inputType = InputType.Text)
        {
            InputMessageBox inst = new InputMessageBox(question , title , inputType);
            inst.ShowDialog();

            if ( inst.DialogResult == true )
                return inst.InputText;

            return null;
        }

        public string TitleText
        {
            get { return TitleTextBox.Text; }
            set { TitleTextBox.Text = value; }
        }

        public string InputText
        {
            get { return InputTextBox.Text; }
            set { InputTextBox.Text = value; }
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
            DialogResult = true;
            Canceled = false;
            Close();
        }
    }
}
