namespace WsCardTagger
{
    partial class WSTagConditionViz
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
            this.label1 = new System.Windows.Forms.Label();
            this.cardInZoneBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numberInZoneTxt = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.characterTypeTxt = new System.Windows.Forms.TextBox();
            this.payCostCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numberInZoneTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Card In Zone:";
            // 
            // cardInZoneBox
            // 
            this.cardInZoneBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cardInZoneBox.FormattingEnabled = true;
            this.cardInZoneBox.Location = new System.Drawing.Point(125, 9);
            this.cardInZoneBox.Margin = new System.Windows.Forms.Padding(4);
            this.cardInZoneBox.Name = "cardInZoneBox";
            this.cardInZoneBox.Size = new System.Drawing.Size(196, 24);
            this.cardInZoneBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Num In Zone";
            // 
            // numberInZoneTxt
            // 
            this.numberInZoneTxt.Location = new System.Drawing.Point(126, 44);
            this.numberInZoneTxt.Name = "numberInZoneTxt";
            this.numberInZoneTxt.Size = new System.Drawing.Size(195, 24);
            this.numberInZoneTxt.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Character in Zone:";
            // 
            // characterTypeTxt
            // 
            this.characterTypeTxt.Location = new System.Drawing.Point(168, 85);
            this.characterTypeTxt.Name = "characterTypeTxt";
            this.characterTypeTxt.Size = new System.Drawing.Size(153, 24);
            this.characterTypeTxt.TabIndex = 8;
            // 
            // payCostCB
            // 
            this.payCostCB.AutoSize = true;
            this.payCostCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.payCostCB.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.payCostCB.Location = new System.Drawing.Point(105, 128);
            this.payCostCB.Name = "payCostCB";
            this.payCostCB.Size = new System.Drawing.Size(89, 22);
            this.payCostCB.TabIndex = 9;
            this.payCostCB.Text = "Pay Cost";
            this.payCostCB.UseVisualStyleBackColor = true;
            // 
            // WSTagConditionViz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.payCostCB);
            this.Controls.Add(this.characterTypeTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numberInZoneTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cardInZoneBox);
            this.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(332, 153);
            this.Name = "WSTagConditionViz";
            this.Size = new System.Drawing.Size(332, 153);
            ((System.ComponentModel.ISupportInitialize)(this.numberInZoneTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cardInZoneBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numberInZoneTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox characterTypeTxt;
        private System.Windows.Forms.CheckBox payCostCB;
    }
}
