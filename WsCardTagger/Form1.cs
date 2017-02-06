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
    public partial class Form1 : Form
    {
        CardCollection cardCollection;

        List<WSCard> currentSetCards = new List<WSCard>();

        Boolean editingTrigger = true;

        public Form1()
        {
            cardCollection = new CardCollection();
            InitializeComponent();
            LoadBasePath();
            //this.Size = new Size(1200 , 720); //needed for whatever reason, may just be my pc
            cardCollection.MigrateCards(); //Convert txt to xml
            LoadSets();

           /* if ( CardCollection.LoadEffects().Count != 0 )
                wsEffectViz1.LoadEffect(CardCollection.LoadEffects()[0]);*/
        }

        private void LoadSets()
        {
            AutoCompleteStringCollection autoStringCollection = new AutoCompleteStringCollection();
            foreach ( String set in cardCollection.GetAllSets() )
            {
                autoStringCollection.Add(set.Remove(0 , 1));
                setComboBox.Items.Add(set.Remove(0 , 1));
            }

            setComboBox.AutoCompleteCustomSource = autoStringCollection;
        }

        private void LoadBasePath()
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            String basePath = WsCardTagger.Properties.Settings.Default.basePath;
            if ( basePath == "" )
            {
                if ( fbd.ShowDialog() == DialogResult.OK )
                {
                    CardCollection.basePath = fbd.SelectedPath;
                    WsCardTagger.Properties.Settings.Default.basePath = CardCollection.basePath;
                }
            } else
            {
                CardCollection.basePath = basePath;
            }

            WsCardTagger.Properties.Settings.Default.Save();
        }

        private void setComboBox_SelectedIndexChanged(object sender , EventArgs e)
        {
            String setName = "\\" + setComboBox.SelectedItem;
            cardGroupBox.Visible = true;
            currentSetCards = new List<WSCard>(cardCollection.GetSet(setName));
            cardCombBox.Items.Clear();

            AutoCompleteStringCollection autoStringCollection = new AutoCompleteStringCollection();
            foreach ( WSCard wsCard in currentSetCards )
            {
                autoStringCollection.Add(wsCard.Name);
                cardCombBox.Items.Add(wsCard.Name);
            }

            cardCombBox.AutoCompleteCustomSource = autoStringCollection;
        }

        private void cardCombBox_SelectedIndexChanged(object sender , EventArgs e)
        {
            cardImageBox.ImageLocation = CardCollection.setImages + currentSetCards[cardCombBox.SelectedIndex].Set + "\\" +currentSetCards[cardCombBox.SelectedIndex].ImageFile + ".jpg";
            cardEffectLbl.Text = currentSetCards[cardCombBox.SelectedIndex].TotalEffect;
        }

        private void globalMethodsToolStripMenuItem_Click(object sender , EventArgs e)
        {
            EffectsForm effectForm = new EffectsForm();
            effectForm.Show();
        }
    }
}
