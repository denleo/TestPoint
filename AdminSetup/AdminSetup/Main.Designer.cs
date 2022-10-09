namespace AdminSetup
{
    partial class Main
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SetupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdminSetupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoggingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.MainMenuStrip.Font = new System.Drawing.Font("Arial Unicode MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetupMenuItem,
            this.LoggingMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(732, 31);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "MainMenuStrip";
            // 
            // SetupMenuItem
            // 
            this.SetupMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AdminSetupMenuItem});
            this.SetupMenuItem.Name = "SetupMenuItem";
            this.SetupMenuItem.Size = new System.Drawing.Size(67, 27);
            this.SetupMenuItem.Text = "Setup";
            // 
            // AdminSetupMenuItem
            // 
            this.AdminSetupMenuItem.Name = "AdminSetupMenuItem";
            this.AdminSetupMenuItem.Size = new System.Drawing.Size(186, 28);
            this.AdminSetupMenuItem.Text = "Admin setup";
            this.AdminSetupMenuItem.Click += new System.EventHandler(this.AdminSetupMenuItem_Click);
            // 
            // LoggingMenuItem
            // 
            this.LoggingMenuItem.Name = "LoggingMenuItem";
            this.LoggingMenuItem.Size = new System.Drawing.Size(81, 27);
            this.LoggingMenuItem.Text = "Logging";
            this.LoggingMenuItem.Click += new System.EventHandler(this.LoggingMenuItem_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 31);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(10);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(10);
            this.MainPanel.Size = new System.Drawing.Size(732, 422);
            this.MainPanel.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(732, 453);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MainMenuStrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Setup Tool";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip MainMenuStrip;
        private ToolStripMenuItem SetupMenuItem;
        private ToolStripMenuItem AdminSetupMenuItem;
        private ToolStripMenuItem LoggingMenuItem;
        private Panel MainPanel;
    }
}