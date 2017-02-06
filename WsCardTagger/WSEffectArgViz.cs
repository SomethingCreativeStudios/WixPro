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
    public partial class WSEffectArgViz : UserControl
    {
        WSEffectParameter currentParameter;
        public WSEffectArgViz()
        {
            InitializeComponent();
        }

        public WSEffectArgViz(WSEffectParameter effectParameter)
        {
            InitializeComponent();
            currentParameter = effectParameter;

            parameterNameLb.Text = effectParameter.parameterName;
            LoadToolTip(effectParameter.parameterType);

            LoadComboBox(effectParameter.parameterType);

            if ( parameterValueBox.Items.Count != 0 )
            {
                if ( effectParameter.parameterValue != "" )
                    parameterValueBox.SelectedItem = effectParameter.parameterValue;
            } else
            {
                parameterValueBox.Text = effectParameter.parameterValue;
            }
        }

        private void LoadToolTip(String toolTipText)
        {
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(this.parameterNameLb , toolTipText);
        }

        public WSEffectParameter effectParameter
        {
            get
            {
                WSEffectParameter temp = new WSEffectParameter();

                temp.parameterName = currentParameter.parameterName;
                temp.parameterType = currentParameter.parameterType;
                temp.parameterValue = parameterValueBox.Text;

                return temp;
            }
        }


        /*
            String
            Interger
            Boolean
            Var Name
            Game Zone
            Card Type
            Card Trait
            Card Name
            Card State
            Level
            Color
            Power
            Soul
        */
        private void LoadComboBox(String parameterType)
        {
            parameterValueBox.Items.Clear();

            if ( parameterType == "Boolean" )
            {
                parameterValueBox.Items.Add("TRUE");
                parameterValueBox.Items.Add("FALSE");
            }

            if ( parameterType == "Game Zone" )
            {
                foreach ( var location in Enum.GetValues(typeof(WSGameObjects.Location)) )
                {
                    parameterValueBox.Items.Add(location);
                }
            }

            if ( parameterType == "Card Type" )
            {
                parameterValueBox.Items.Add("Character");
                parameterValueBox.Items.Add("Climax");
                parameterValueBox.Items.Add("Event");
            }

            if ( parameterType == "Card Trait" )
            {
                foreach ( string wsTrait in CardCollection.getAllCardTraits() )
                {
                    parameterValueBox.Items.Add(wsTrait);
                }
            }

            if ( parameterType == "Card Name" )
            {
                foreach ( string name in CardCollection.getAllCardNames() )
                {
                    parameterValueBox.Items.Add(name);
                }
            }


            if ( parameterType == "Card State" )
            {
                foreach (var cardState in Enum.GetValues(typeof(WSGameObjects.CardState) ))
                {
                    parameterValueBox.Items.Add(cardState);
                }
            }

            if ( parameterType == "Level" )
            {
                for ( int i = 0; i < 4; i++ )
                {
                    parameterValueBox.Items.Add(i);
                }
            }

            if ( parameterType == "Color" )
            {
                parameterValueBox.Items.Add("RED");
                parameterValueBox.Items.Add("BLUE");
                parameterValueBox.Items.Add("GREEN");
                parameterValueBox.Items.Add("YELLOW");
            }

            if ( parameterType == "Soul" )
            {
                for ( int i = 0; i < 13; i++ )
                {
                    parameterValueBox.Items.Add(i);
                }
            }

            if(parameterValueBox.Items.Count != 0 )
            {
                parameterValueBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
    }
}
