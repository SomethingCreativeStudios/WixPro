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
    public partial class WSTagConditionViz : UserControl
    {
        public WSCondition getWSCondition
        {
            get
            {
                WSCondition wsCondition = new WSCondition();

                wsCondition.characterType = characterTypeTxt.Text;
                wsCondition.zoneToCheck = (Location)cardInZoneBox.Items[cardInZoneBox.SelectedIndex];
                wsCondition.cardInZone = (int)numberInZoneTxt.Value;
                wsCondition.mustPayCost = payCostCB.Checked;

                return wsCondition;
            }
        }
        public WSTagConditionViz()
        {
            InitializeComponent();
            foreach ( Control control in Controls )
            {
                control.ForeColor = Color.Black;
            }

            foreach ( var locationVal in Enum.GetValues(typeof(Location)) )
            {
                cardInZoneBox.Items.Add(locationVal);
            }
        }
    }
}
