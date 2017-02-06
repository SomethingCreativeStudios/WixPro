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
    public partial class WSCardTagViz : UserControl
    {
        WSCardTag wsCardTag;
        public WSCardTagViz()
        {
            InitializeComponent();
            wsCardTag = new WSCardTag();
        }
    }
}
