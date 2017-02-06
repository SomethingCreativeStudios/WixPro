namespace WsCardTagger
{
    partial class WSTagViz
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.triggerTab = new System.Windows.Forms.TabPage();
            this.wsTagTriggerViz1 = new WsCardTagger.WSTagTriggerViz();
            this.costTab = new System.Windows.Forms.TabPage();
            this.effectTab = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.triggerTab.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.triggerTab);
            this.tabControl1.Controls.Add(this.costTab);
            this.tabControl1.Controls.Add(this.effectTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(690, 466);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // triggerTab
            // 
            this.triggerTab.AutoScroll = true;
            this.triggerTab.Controls.Add(this.wsTagTriggerViz1);
            this.triggerTab.Location = new System.Drawing.Point(4, 22);
            this.triggerTab.Name = "triggerTab";
            this.triggerTab.Padding = new System.Windows.Forms.Padding(3);
            this.triggerTab.Size = new System.Drawing.Size(682, 440);
            this.triggerTab.TabIndex = 0;
            this.triggerTab.Text = "Effect Trigger";
            this.triggerTab.UseVisualStyleBackColor = true;
            // 
            // wsTagTriggerViz1
            // 
            this.wsTagTriggerViz1.BackColor = System.Drawing.Color.Transparent;
            this.wsTagTriggerViz1.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Bold);
            this.wsTagTriggerViz1.Location = new System.Drawing.Point(130, 7);
            this.wsTagTriggerViz1.Margin = new System.Windows.Forms.Padding(4);
            this.wsTagTriggerViz1.MinimumSize = new System.Drawing.Size(359, 343);
            this.wsTagTriggerViz1.Name = "wsTagTriggerViz1";
            this.wsTagTriggerViz1.Size = new System.Drawing.Size(406, 412);
            this.wsTagTriggerViz1.TabIndex = 4;
            // 
            // costTab
            // 
            this.costTab.Location = new System.Drawing.Point(4, 22);
            this.costTab.Name = "costTab";
            this.costTab.Padding = new System.Windows.Forms.Padding(3);
            this.costTab.Size = new System.Drawing.Size(682, 440);
            this.costTab.TabIndex = 2;
            this.costTab.Text = "Cost";
            this.costTab.UseVisualStyleBackColor = true;
            // 
            // effectTab
            // 
            this.effectTab.Location = new System.Drawing.Point(4, 22);
            this.effectTab.Name = "effectTab";
            this.effectTab.Padding = new System.Windows.Forms.Padding(3);
            this.effectTab.Size = new System.Drawing.Size(682, 440);
            this.effectTab.TabIndex = 1;
            this.effectTab.Text = "Card Effect";
            this.effectTab.UseVisualStyleBackColor = true;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(433, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Add New Trigger";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.addXBtn_Click);
            // 
            // WSTagViz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "WSTagViz";
            this.Size = new System.Drawing.Size(690, 507);
            this.tabControl1.ResumeLayout(false);
            this.triggerTab.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage triggerTab;
        private WSTagTriggerViz wsTagTriggerViz1;
        private System.Windows.Forms.TabPage costTab;
        private System.Windows.Forms.TabPage effectTab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}
