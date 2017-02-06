namespace WsCardTagger
{
    partial class WSTagTriggerViz
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cardTriggerGroup = new System.Windows.Forms.GroupBox();
            this.wsConditionPanel = new System.Windows.Forms.Panel();
            this.addConditionBtn = new System.Windows.Forms.Button();
            this.attackTypeBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gamePhaseBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toLocationBox = new System.Windows.Forms.ComboBox();
            this.fromLocationBox = new System.Windows.Forms.ComboBox();
            this.hasTriggerCB = new System.Windows.Forms.CheckBox();
            this.cardTriggerGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardTriggerGroup
            // 
            this.cardTriggerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardTriggerGroup.AutoSize = true;
            this.cardTriggerGroup.Controls.Add(this.wsConditionPanel);
            this.cardTriggerGroup.Controls.Add(this.addConditionBtn);
            this.cardTriggerGroup.Controls.Add(this.attackTypeBox);
            this.cardTriggerGroup.Controls.Add(this.label4);
            this.cardTriggerGroup.Controls.Add(this.gamePhaseBox);
            this.cardTriggerGroup.Controls.Add(this.label3);
            this.cardTriggerGroup.Controls.Add(this.label2);
            this.cardTriggerGroup.Controls.Add(this.label1);
            this.cardTriggerGroup.Controls.Add(this.toLocationBox);
            this.cardTriggerGroup.Controls.Add(this.fromLocationBox);
            this.cardTriggerGroup.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold);
            this.cardTriggerGroup.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cardTriggerGroup.Location = new System.Drawing.Point(4, 30);
            this.cardTriggerGroup.Margin = new System.Windows.Forms.Padding(4);
            this.cardTriggerGroup.Name = "cardTriggerGroup";
            this.cardTriggerGroup.Padding = new System.Windows.Forms.Padding(4);
            this.cardTriggerGroup.Size = new System.Drawing.Size(424, 375);
            this.cardTriggerGroup.TabIndex = 0;
            this.cardTriggerGroup.TabStop = false;
            this.cardTriggerGroup.Text = "Card Trigger";
            // 
            // wsConditionPanel
            // 
            this.wsConditionPanel.AutoScroll = true;
            this.wsConditionPanel.Location = new System.Drawing.Point(32, 192);
            this.wsConditionPanel.Name = "wsConditionPanel";
            this.wsConditionPanel.Size = new System.Drawing.Size(370, 159);
            this.wsConditionPanel.TabIndex = 9;
            // 
            // addConditionBtn
            // 
            this.addConditionBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addConditionBtn.Location = new System.Drawing.Point(47, 149);
            this.addConditionBtn.Name = "addConditionBtn";
            this.addConditionBtn.Size = new System.Drawing.Size(295, 23);
            this.addConditionBtn.TabIndex = 8;
            this.addConditionBtn.Text = "Add WSCondition";
            this.addConditionBtn.UseVisualStyleBackColor = true;
            this.addConditionBtn.Click += new System.EventHandler(this.addConditionBtn_Click);
            // 
            // attackTypeBox
            // 
            this.attackTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackTypeBox.FormattingEnabled = true;
            this.attackTypeBox.Location = new System.Drawing.Point(168, 109);
            this.attackTypeBox.Name = "attackTypeBox";
            this.attackTypeBox.Size = new System.Drawing.Size(162, 24);
            this.attackTypeBox.TabIndex = 7;
            this.attackTypeBox.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Attack Type:";
            this.label4.Visible = false;
            // 
            // gamePhaseBox
            // 
            this.gamePhaseBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gamePhaseBox.FormattingEnabled = true;
            this.gamePhaseBox.Location = new System.Drawing.Point(168, 75);
            this.gamePhaseBox.Name = "gamePhaseBox";
            this.gamePhaseBox.Size = new System.Drawing.Size(162, 24);
            this.gamePhaseBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "During Game Phase:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Location:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "From Location:";
            // 
            // toLocationBox
            // 
            this.toLocationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toLocationBox.FormattingEnabled = true;
            this.toLocationBox.Location = new System.Drawing.Point(179, 42);
            this.toLocationBox.Name = "toLocationBox";
            this.toLocationBox.Size = new System.Drawing.Size(121, 24);
            this.toLocationBox.TabIndex = 1;
            // 
            // fromLocationBox
            // 
            this.fromLocationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromLocationBox.FormattingEnabled = true;
            this.fromLocationBox.Location = new System.Drawing.Point(32, 42);
            this.fromLocationBox.Name = "fromLocationBox";
            this.fromLocationBox.Size = new System.Drawing.Size(132, 24);
            this.fromLocationBox.TabIndex = 0;
            // 
            // hasTriggerCB
            // 
            this.hasTriggerCB.AutoSize = true;
            this.hasTriggerCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hasTriggerCB.Checked = true;
            this.hasTriggerCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hasTriggerCB.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hasTriggerCB.Location = new System.Drawing.Point(136, 3);
            this.hasTriggerCB.Name = "hasTriggerCB";
            this.hasTriggerCB.Size = new System.Drawing.Size(110, 22);
            this.hasTriggerCB.TabIndex = 1;
            this.hasTriggerCB.Text = "Has Trigger";
            this.hasTriggerCB.UseVisualStyleBackColor = true;
            this.hasTriggerCB.CheckedChanged += new System.EventHandler(this.hasTriggerCB_CheckedChanged);
            // 
            // WSTagTriggerViz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.hasTriggerCB);
            this.Controls.Add(this.cardTriggerGroup);
            this.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WSTagTriggerViz";
            this.Size = new System.Drawing.Size(424, 409);
            this.cardTriggerGroup.ResumeLayout(false);
            this.cardTriggerGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox cardTriggerGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox toLocationBox;
        private System.Windows.Forms.ComboBox fromLocationBox;
        private System.Windows.Forms.CheckBox hasTriggerCB;
        private System.Windows.Forms.ComboBox gamePhaseBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox attackTypeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel wsConditionPanel;
        private System.Windows.Forms.Button addConditionBtn;
    }
}
