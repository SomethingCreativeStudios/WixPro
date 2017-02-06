using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WsCardTagger.WSGameObjects;

namespace WsCardTagger
{
    public partial class WSTagViz : UserControl
    {
        String currentTab = "trigger";
        public WSTagViz()
        {
            InitializeComponent();
        }

        private void tabControl1_Selected(object sender , TabControlEventArgs e)
        {
            TabPage page = (TabPage)sender;
            button1.Visible = true;
            switch ( page.Text )
            {
                case "Effect Trigger":
                    currentTab = "trigger";
                    button1.Visible = false;
                    break;
                case "Cost":
                    currentTab = "cost";
                    button1.Text = "Add New Cost";
                    break;
                case "Card Effect":
                    currentTab = "effect";
                    button1.Text = "Add New Effect";
                    break;
            }
        }

        private void addXBtn_Click(object sender , EventArgs e)
        {
            if ( currentTab == "effect" )
            {
                
            }
            if ( currentTab == "trigger" )
            {

            }
            if ( currentTab == "cost" )
            {

            }
        }
    }
}
