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
    public partial class EffectsForm : Form
    {
        public EffectsForm()
        {
            InitializeComponent();
            foreach ( var effectTag in CardCollection.LoadEffects() )
            {
                effectBox.Items.Add(effectTag);
            }
        }

        private void editToolStripMenuItem_Click(object sender , EventArgs e)
        {
            if ( effectBox.SelectedIndex != -1 )
            {
                WSMethodTag temp = (WSMethodTag)effectBox.SelectedItem;

                WSEffectMethodViz methodParameterViz = new WSEffectMethodViz(temp , effectBox.SelectedIndex);
                methodParameterViz.FormClosing += EffectMethod_FormClosing;
                methodParameterViz.Show();
            }
        }

        private void removeToolStripMenuItem_Click(object sender , EventArgs e)
        {
            if ( effectBox.SelectedIndex != -1 )
                effectBox.Items.RemoveAt(effectBox.SelectedIndex);
        }

        private void EffectMethod_FormClosing(object sender , FormClosingEventArgs e)
        {
            WSEffectMethodViz effectMethod = (WSEffectMethodViz)sender;
            if ( effectMethod.selectedIndex == -1 )
                effectBox.Items.Add(effectMethod.effectTag);
            else
                effectBox.Items[effectMethod.selectedIndex] = effectMethod.effectTag;
        }

        private void addEffectToolStripMenuItem_Click(object sender , EventArgs e)
        {
            WSEffectMethodViz effectMethod = new WSEffectMethodViz();
            effectMethod.FormClosing += EffectMethod_FormClosing;
            effectMethod.Show();
        }

        private void saveEffectsToolStripMenuItem_Click(object sender , EventArgs e)
        {
            List<WSEffectTag> effects = new List<WSEffectTag>();

            foreach ( WSMethodTag effectTag in effectBox.Items )
            {
                effects.Add(new WSEffectTag(effectTag));
            }

            CardCollection.SaveEffects(effects);
        }
    }
}
