using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WsCardTagger.WSGameObjects;

namespace WsCardTagger
{
    public partial class WSMethodParameterViz : Form
    {
        public int selectedIndex = -1;
        public WSMethodParameterViz()
        {
            InitializeComponent();
        }

        public WSMethodParameterViz(WSEffectParameter effectParameter, int selectIndex)
        {
            InitializeComponent();

            textBox1.Text = effectParameter.parameterName;
            comboBox1.SelectedItem = effectParameter.parameterType;
            this.selectedIndex = selectIndex;
        }

        public WSEffectParameter effectParameter
        {
            get
            {
                WSEffectParameter effectParameter = new WSEffectParameter();

                effectParameter.parameterName = textBox1.Text;
                effectParameter.parameterType = comboBox1.Text;

                return effectParameter;
            }
        }

        private void saveParaBtn_Click(object sender , EventArgs e)
        {
            this.Close();
        }
    }
}
