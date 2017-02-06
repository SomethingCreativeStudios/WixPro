namespace WsCardTagger
{
    partial class WSEffectViz
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
            this.varNameTxt = new System.Windows.Forms.TextBox();
            this.methodBox = new System.Windows.Forms.Panel();
            this.methodNameLb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Var Name: ";
            // 
            // varNameTxt
            // 
            this.varNameTxt.Location = new System.Drawing.Point(128, 15);
            this.varNameTxt.Name = "varNameTxt";
            this.varNameTxt.Size = new System.Drawing.Size(179, 20);
            this.varNameTxt.TabIndex = 2;
            // 
            // methodBox
            // 
            this.methodBox.AutoScroll = true;
            this.methodBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.methodBox.Location = new System.Drawing.Point(3, 54);
            this.methodBox.Name = "methodBox";
            this.methodBox.Size = new System.Drawing.Size(438, 148);
            this.methodBox.TabIndex = 3;
            // 
            // methodNameLb
            // 
            this.methodNameLb.AutoSize = true;
            this.methodNameLb.Location = new System.Drawing.Point(177, 38);
            this.methodNameLb.Name = "methodNameLb";
            this.methodNameLb.Size = new System.Drawing.Size(74, 13);
            this.methodNameLb.TabIndex = 4;
            this.methodNameLb.Text = "Method Name";
            // 
            // WSEffectViz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.methodNameLb);
            this.Controls.Add(this.methodBox);
            this.Controls.Add(this.varNameTxt);
            this.Controls.Add(this.label1);
            this.Name = "WSEffectViz";
            this.Size = new System.Drawing.Size(445, 209);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox varNameTxt;
        private System.Windows.Forms.Panel methodBox;
        private System.Windows.Forms.Label methodNameLb;
    }
}
