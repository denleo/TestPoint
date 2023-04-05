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
            MainMenuStrip = new MenuStrip();
            SetupMenuItem = new ToolStripMenuItem();
            AdminSetupMenuItem = new ToolStripMenuItem();
            LoggingMenuItem = new ToolStripMenuItem();
            MainPanel = new Panel();
            MainMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainMenuStrip
            // 
            MainMenuStrip.BackColor = Color.Transparent;
            MainMenuStrip.Font = new Font("Arial Unicode MS", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            MainMenuStrip.ImageScalingSize = new Size(20, 20);
            MainMenuStrip.Items.AddRange(new ToolStripItem[] { SetupMenuItem, LoggingMenuItem });
            MainMenuStrip.Location = new Point(0, 0);
            MainMenuStrip.Name = "MainMenuStrip";
            MainMenuStrip.Size = new Size(462, 31);
            MainMenuStrip.TabIndex = 0;
            MainMenuStrip.Text = "MainMenuStrip";
            // 
            // SetupMenuItem
            // 
            SetupMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AdminSetupMenuItem });
            SetupMenuItem.Name = "SetupMenuItem";
            SetupMenuItem.Size = new Size(67, 27);
            SetupMenuItem.Text = "Setup";
            // 
            // AdminSetupMenuItem
            // 
            AdminSetupMenuItem.Name = "AdminSetupMenuItem";
            AdminSetupMenuItem.Size = new Size(186, 28);
            AdminSetupMenuItem.Text = "Admin setup";
            AdminSetupMenuItem.Click += AdminSetupMenuItem_Click;
            // 
            // LoggingMenuItem
            // 
            LoggingMenuItem.Name = "LoggingMenuItem";
            LoggingMenuItem.Size = new Size(81, 27);
            LoggingMenuItem.Text = "Logging";
            LoggingMenuItem.Click += LoggingMenuItem_Click;
            // 
            // MainPanel
            // 
            MainPanel.BorderStyle = BorderStyle.FixedSingle;
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 31);
            MainPanel.Margin = new Padding(10);
            MainPanel.Name = "MainPanel";
            MainPanel.Padding = new Padding(10);
            MainPanel.Size = new Size(462, 236);
            MainPanel.TabIndex = 1;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(462, 267);
            Controls.Add(MainPanel);
            Controls.Add(MainMenuStrip);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Main";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Setup Tool";
            MainMenuStrip.ResumeLayout(false);
            MainMenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainMenuStrip;
        private ToolStripMenuItem SetupMenuItem;
        private ToolStripMenuItem AdminSetupMenuItem;
        private ToolStripMenuItem LoggingMenuItem;
        private Panel MainPanel;
    }
}