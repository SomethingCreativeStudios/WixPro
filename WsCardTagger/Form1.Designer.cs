namespace WsCardTagger
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.methodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalMethodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cardGroupBox = new System.Windows.Forms.GroupBox();
            this.cardEffectLbl = new System.Windows.Forms.RichTextBox();
            this.addXBtn = new System.Windows.Forms.Button();
            this.cardImageBox = new System.Windows.Forms.PictureBox();
            this.cardCombBox = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wsTagViz1 = new WsCardTagger.WSTagViz();
            this.menuStrip1.SuspendLayout();
            this.cardGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardImageBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.methodsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1436, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // methodsToolStripMenuItem
            // 
            this.methodsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalMethodsToolStripMenuItem});
            this.methodsToolStripMenuItem.Name = "methodsToolStripMenuItem";
            this.methodsToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.methodsToolStripMenuItem.Text = "Methods";
            // 
            // globalMethodsToolStripMenuItem
            // 
            this.globalMethodsToolStripMenuItem.Name = "globalMethodsToolStripMenuItem";
            this.globalMethodsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.globalMethodsToolStripMenuItem.Text = "Global Methods";
            this.globalMethodsToolStripMenuItem.Click += new System.EventHandler(this.globalMethodsToolStripMenuItem_Click);
            // 
            // setComboBox
            // 
            this.setComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.setComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.setComboBox.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold);
            this.setComboBox.FormattingEnabled = true;
            this.setComboBox.Location = new System.Drawing.Point(389, 48);
            this.setComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.setComboBox.Name = "setComboBox";
            this.setComboBox.Size = new System.Drawing.Size(189, 24);
            this.setComboBox.TabIndex = 1;
            this.setComboBox.SelectedIndexChanged += new System.EventHandler(this.setComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(444, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pick Set:";
            // 
            // cardGroupBox
            // 
            this.cardGroupBox.Controls.Add(this.wsTagViz1);
            this.cardGroupBox.Controls.Add(this.cardEffectLbl);
            this.cardGroupBox.Controls.Add(this.addXBtn);
            this.cardGroupBox.Controls.Add(this.cardImageBox);
            this.cardGroupBox.Controls.Add(this.cardCombBox);
            this.cardGroupBox.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardGroupBox.Location = new System.Drawing.Point(30, 69);
            this.cardGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.cardGroupBox.Name = "cardGroupBox";
            this.cardGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.cardGroupBox.Size = new System.Drawing.Size(1395, 583);
            this.cardGroupBox.TabIndex = 3;
            this.cardGroupBox.TabStop = false;
            this.cardGroupBox.Text = "The Card";
            this.cardGroupBox.Visible = false;
            // 
            // cardEffectLbl
            // 
            this.cardEffectLbl.BackColor = System.Drawing.SystemColors.Control;
            this.cardEffectLbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardEffectLbl.Location = new System.Drawing.Point(5, 370);
            this.cardEffectLbl.Name = "cardEffectLbl";
            this.cardEffectLbl.ReadOnly = true;
            this.cardEffectLbl.Size = new System.Drawing.Size(429, 208);
            this.cardEffectLbl.TabIndex = 7;
            this.cardEffectLbl.Text = "";
            // 
            // addXBtn
            // 
            this.addXBtn.Location = new System.Drawing.Point(322, 186);
            this.addXBtn.Name = "addXBtn";
            this.addXBtn.Size = new System.Drawing.Size(102, 52);
            this.addXBtn.TabIndex = 6;
            this.addXBtn.Text = "Add New Trigger";
            this.addXBtn.UseVisualStyleBackColor = true;
            // 
            // cardImageBox
            // 
            this.cardImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cardImageBox.Image = global::WsCardTagger.Properties.Resources.cardback;
            this.cardImageBox.Location = new System.Drawing.Point(90, 83);
            this.cardImageBox.Margin = new System.Windows.Forms.Padding(2);
            this.cardImageBox.Name = "cardImageBox";
            this.cardImageBox.Size = new System.Drawing.Size(211, 270);
            this.cardImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardImageBox.TabIndex = 1;
            this.cardImageBox.TabStop = false;
            // 
            // cardCombBox
            // 
            this.cardCombBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cardCombBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cardCombBox.FormattingEnabled = true;
            this.cardCombBox.Location = new System.Drawing.Point(16, 30);
            this.cardCombBox.Margin = new System.Windows.Forms.Padding(2);
            this.cardCombBox.Name = "cardCombBox";
            this.cardCombBox.Size = new System.Drawing.Size(216, 24);
            this.cardCombBox.TabIndex = 0;
            this.cardCombBox.SelectedIndexChanged += new System.EventHandler(this.cardCombBox_SelectedIndexChanged);
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
            this.editToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // wsTagViz1
            // 
            this.wsTagViz1.Location = new System.Drawing.Point(441, 30);
            this.wsTagViz1.Margin = new System.Windows.Forms.Padding(4);
            this.wsTagViz1.Name = "wsTagViz1";
            this.wsTagViz1.Size = new System.Drawing.Size(934, 547);
            this.wsTagViz1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1436, 663);
            this.Controls.Add(this.cardGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.setComboBox);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cardGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cardImageBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ComboBox setComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox cardGroupBox;
        private System.Windows.Forms.ComboBox cardCombBox;
        private System.Windows.Forms.PictureBox cardImageBox;
        private System.Windows.Forms.Button addXBtn;
        private System.Windows.Forms.RichTextBox cardEffectLbl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem methodsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalMethodsToolStripMenuItem;
        private WSTagViz wsTagViz1;
    }
}

