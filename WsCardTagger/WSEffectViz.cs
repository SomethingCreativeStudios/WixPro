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
    public partial class WSEffectViz : UserControl
    {

        WSEffectTag currentEffectTag;
        public WSEffectViz()
        {
            InitializeComponent();
        }

        public WSEffectViz(WSEffectTag effectTag)
        {
            InitializeComponent();
            LoadEffect(effectTag);
        }

        public void LoadEffect(WSEffectTag effectTag)
        {
            currentEffectTag = effectTag;
            methodNameLb.Text = effectTag.methodName;

            int currentX = 0;
            int currentY = 50;
            foreach ( var effectParameter in effectTag.methodParameters )
            {
                WSEffectArgViz argument = new WSEffectArgViz(effectParameter);
                argument.Location = new Point(currentX , currentY);

                methodBox.Controls.Add(argument);
                currentX += argument.Width + 15;
                if((currentX + 10) >= methodBox.Width )
                {
                    currentY += argument.Height + 20;
                    currentX = 0;
                }
            }
        }

        public WSEffectTag effectTag
        {
            get
            {
                WSEffectTag tempTag = new WSEffectTag();
                tempTag.methodParameters = new List<WSEffectParameter>();

                tempTag.methodName = methodBox.Text;
                tempTag.methodReturnType = currentEffectTag.methodReturnType;
                tempTag.varName = varNameTxt.Text;

                foreach ( WSEffectArgViz control in methodBox.Controls )
                {
                    tempTag.methodParameters.Add(control.effectParameter);
                }
                return tempTag;
            }
        }
    }
}
