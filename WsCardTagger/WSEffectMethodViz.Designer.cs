namespace WsCardTagger
{
    partial class WSEffectMethodViz
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.methodTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.returnTypeBox = new System.Windows.Forms.ComboBox();
            this.saveMethodBtn = new System.Windows.Forms.Button();
            this.addParaBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Method Name:";
            // 
            // methodTxt
            // 
            this.methodTxt.Location = new System.Drawing.Point(12, 31);
            this.methodTxt.Name = "methodTxt";
            this.methodTxt.Size = new System.Drawing.Size(540, 24);
            this.methodTxt.TabIndex = 1;
            this.methodTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Method Parameters:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 227);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Method Return Type: ";
            // 
            // returnTypeBox
            // 
            this.returnTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.returnTypeBox.FormattingEnabled = true;
            this.returnTypeBox.Items.AddRange(new object[] {
            "Void(Nothing)",
            "String",
            "Boolean(Yes, No)",
            "WSCard"});
            this.returnTypeBox.Location = new System.Drawing.Point(12, 259);
            this.returnTypeBox.Name = "returnTypeBox";
            this.returnTypeBox.Size = new System.Drawing.Size(540, 24);
            this.returnTypeBox.TabIndex = 5;
            // 
            // saveMethodBtn
            // 
            this.saveMethodBtn.Location = new System.Drawing.Point(422, 289);
            this.saveMethodBtn.Name = "saveMethodBtn";
            this.saveMethodBtn.Size = new System.Drawing.Size(130, 27);
            this.saveMethodBtn.TabIndex = 6;
            this.saveMethodBtn.Text = "Save";
            this.saveMethodBtn.UseVisualStyleBackColor = true;
            this.saveMethodBtn.Click += new System.EventHandler(this.saveMethodBtn_Click);
            // 
            // addParaBtn
            // 
            this.addParaBtn.Location = new System.Drawing.Point(422, 76);
            this.addParaBtn.Name = "addParaBtn";
            this.addParaBtn.Size = new System.Drawing.Size(130, 27);
            this.addParaBtn.TabIndex = 7;
            this.addParaBtn.Text = "Add Parameter";
            this.addParaBtn.UseVisualStyleBackColor = true;
            this.addParaBtn.Click += new System.EventHandler(this.addParaBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 109);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(540, 116);
            this.listBox1.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // WSEffectMethodViz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 325);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.addParaBtn);
            this.Controls.Add(this.saveMethodBtn);
            this.Controls.Add(this.returnTypeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.methodTxt);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WSEffectMethodViz";
            this.Text = "Add New Method";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox methodTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox returnTypeBox;
        private System.Windows.Forms.Button saveMethodBtn;
        private System.Windows.Forms.Button addParaBtn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}