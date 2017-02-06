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
    public partial class WSTagTriggerViz : UserControl
    {
        public WSTagTriggerViz()
        {
            InitializeComponent();
            foreach ( Control control in Controls )
            {
                control.ForeColor = Color.Black;
            }
            foreach ( var locationVal in Enum.GetValues(typeof(Location)) )
            {
                fromLocationBox.Items.Add(locationVal);
                toLocationBox.Items.Add(locationVal);
            }

            foreach ( var gamePhaseVal in Enum.GetValues(typeof(GamePhase)) )
            {
                gamePhaseBox.Items.Add(gamePhaseVal);
            }

            foreach ( var attackVal in Enum.GetValues(typeof(AttackType)) )
            {
                attackTypeBox.Items.Add(attackVal);
            }

            fromLocationBox.SelectedIndex = 0;
            toLocationBox.SelectedIndex = 0;
            gamePhaseBox.SelectedIndex = 0;
            attackTypeBox.SelectedIndex = 0;
        }

        private void hasTriggerCB_CheckedChanged(object sender , EventArgs e)
        {
              getTagTrigger();
            cardTriggerGroup.Enabled = hasTriggerCB.Checked;
           
        }

        public WSTagTrigger getTagTrigger()
        {
            WSTagTrigger wsTagTrigger = new WSTagTrigger();

            wsTagTrigger.cardHasTrigger = hasTriggerCB.Checked;

            if ( hasTriggerCB.Checked )
            {
                wsTagTrigger.fromlocation = (Location)fromLocationBox.Items[fromLocationBox.SelectedIndex];
                wsTagTrigger.toLocation = (Location)toLocationBox.Items[toLocationBox.SelectedIndex];
                wsTagTrigger.gamePhase = (GamePhase)gamePhaseBox.Items[gamePhaseBox.SelectedIndex];
                wsTagTrigger.onAttack = (AttackType)attackTypeBox.Items[attackTypeBox.SelectedIndex];
                wsTagTrigger.onMove = wsTagTrigger.toLocation != WSGameObjects.Location.NoLocation;
                wsTagTrigger.conditionsToBeMent = new List<WSCondition>();
            } 

            if(wsConditionPanel.Controls.Count != 0 )
            {
                foreach ( WSTagConditionViz wsConditions in wsConditionPanel.Controls )
                {
                    wsTagTrigger.conditionsToBeMent.Add(wsConditions.getWSCondition);
                }
            }

            return wsTagTrigger;
        }

        private void addConditionBtn_Click(object sender , EventArgs e)
        {
            WSTagConditionViz condition = new WSTagConditionViz();
            condition.MouseClick += Condition_MouseClick;

            if(wsConditionPanel.Controls.Count != 0 )
            {
                condition.Location =  new Point(0, wsConditionPanel.Controls[wsConditionPanel.Controls.Count - 1].Location.Y + condition.Height);
            }

            wsConditionPanel.Controls.Add(condition);
        }

        private void Condition_MouseClick(object sender , MouseEventArgs e)
        {
            wsConditionPanel.Controls.Remove((Control)sender);
            int y = 0;
            for ( int i = 0; i < wsConditionPanel.Controls.Count; i++ )
            {
                if(i != 0 )
                {
                    wsConditionPanel.Controls[i].Location = new Point(0 , y);
                    y += wsConditionPanel.Controls[i].Height;
                }
            }
        }
    }
}
