namespace WsCardTagger
{
    partial class WSEffectArgViz
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
            this.parameterNameLb = new System.Windows.Forms.Label();
            this.parameterValueBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // parameterNameLb
            // 
            this.parameterNameLb.Location = new System.Drawing.Point(4, 0);
            this.parameterNameLb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.parameterNameLb.Name = "parameterNameLb";
            this.parameterNameLb.Size = new System.Drawing.Size(186, 34);
            this.parameterNameLb.TabIndex = 0;
            this.parameterNameLb.Text = "Parameter Name";
            this.parameterNameLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // parameterValueBox
            // 
            this.parameterValueBox.FormattingEnabled = true;
            this.parameterValueBox.Location = new System.Drawing.Point(7, 37);
            this.parameterValueBox.Name = "parameterValueBox";
            this.parameterValueBox.Size = new System.Drawing.Size(183, 24);
            this.parameterValueBox.TabIndex = 1;
            // 
            // WSEffectArgViz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.parameterValueBox);
            this.Controls.Add(this.parameterNameLb);
            this.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WSEffectArgViz";
            this.Size = new System.Drawing.Size(201, 83);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label parameterNameLb;
        private System.Windows.Forms.ComboBox parameterValueBox;
    }
}
