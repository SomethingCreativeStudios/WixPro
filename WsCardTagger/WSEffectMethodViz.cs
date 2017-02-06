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
    public partial class WSEffectMethodViz : Form
    {
        public int selectedIndex = -1;

        public WSEffectMethodViz()
        {
            InitializeComponent();
        }

        public WSEffectMethodViz(WSMethodTag effectTag, int selectedIndex)
        {
            InitializeComponent();

            methodTxt.Text = effectTag.methodName;
            returnTypeBox.SelectedItem = effectTag.methodReturnType;

            foreach ( WSEffectParameter paramterItem in effectTag.methodParameters )
            {
                listBox1.Items.Add(paramterItem);
            }

            this.selectedIndex = selectedIndex;
        }

        public WSMethodTag effectTag
        {
            get
            {
                WSMethodTag tag = new WSMethodTag();
                tag.methodName = methodTxt.Text;
                tag.methodReturnType = returnTypeBox.Text;
                tag.methodParameters = new List<WSEffectParameter>();

                foreach ( WSEffectParameter paramterItem in listBox1.Items )
                {
                    tag.methodParameters.Add(paramterItem);
                }

                return tag;
            }
        }

        private void addParaBtn_Click(object sender , EventArgs e)
        {
            WSMethodParameterViz methodParameterViz = new WSMethodParameterViz();
            methodParameterViz.FormClosing += MethodParameterViz_FormClosing;
            methodParameterViz.Show();
        }

        private void MethodParameterViz_FormClosing(object sender , FormClosingEventArgs e)
        {
            WSMethodParameterViz methodParameterViz = (WSMethodParameterViz)sender;
            if ( methodParameterViz.effectParameter.parameterName != "" )
            {
                if ( methodParameterViz.selectedIndex == -1 )
                    listBox1.Items.Add(methodParameterViz.effectParameter);
                else
                    listBox1.Items[methodParameterViz.selectedIndex] = methodParameterViz.effectParameter;
            }
        }

        private void saveMethodBtn_Click(object sender , EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender , EventArgs e)
        {
            if ( listBox1.SelectedIndex != -1 )
            {
                WSEffectParameter temp = (WSEffectParameter)listBox1.SelectedItem;

                WSMethodParameterViz methodParameterViz = new WSMethodParameterViz(temp , listBox1.SelectedIndex);
                methodParameterViz.FormClosing += MethodParameterViz_FormClosing;
                methodParameterViz.Show();
            }
        }

        private void removeToolStripMenuItem_Click(object sender , EventArgs e)
        {
            if ( listBox1.SelectedIndex != -1 )
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
    }
}
