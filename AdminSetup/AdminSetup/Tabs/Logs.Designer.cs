namespace AdminSetup.Tabs
{
    partial class Logs
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
            LogsCenterLabel = new Label();
            LogFilesComboBox = new ComboBox();
            panel1 = new Panel();
            DownloadBtn = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // LogsCenterLabel
            // 
            LogsCenterLabel.AutoSize = true;
            LogsCenterLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LogsCenterLabel.Location = new Point(127, 57);
            LogsCenterLabel.Name = "LogsCenterLabel";
            LogsCenterLabel.Size = new Size(210, 28);
            LogsCenterLabel.TabIndex = 5;
            LogsCenterLabel.Text = "Logs Download Center";
            // 
            // LogFilesComboBox
            // 
            LogFilesComboBox.BackColor = Color.White;
            LogFilesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LogFilesComboBox.FlatStyle = FlatStyle.System;
            LogFilesComboBox.FormattingEnabled = true;
            LogFilesComboBox.Location = new Point(12, 14);
            LogFilesComboBox.Name = "LogFilesComboBox";
            LogFilesComboBox.Size = new Size(263, 28);
            LogFilesComboBox.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(DownloadBtn);
            panel1.Controls.Add(LogFilesComboBox);
            panel1.Location = new Point(32, 98);
            panel1.Name = "panel1";
            panel1.Size = new Size(401, 75);
            panel1.TabIndex = 7;
            // 
            // DownloadBtn
            // 
            DownloadBtn.Location = new Point(294, 14);
            DownloadBtn.Name = "DownloadBtn";
            DownloadBtn.Size = new Size(94, 28);
            DownloadBtn.TabIndex = 7;
            DownloadBtn.Text = "Download";
            DownloadBtn.UseVisualStyleBackColor = true;
            // 
            // Logs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(462, 236);
            Controls.Add(panel1);
            Controls.Add(LogsCenterLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Logs";
            Text = "Logs";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LogsCenterLabel;
        private ComboBox LogFilesComboBox;
        private Panel panel1;
        private Button DownloadBtn;
    }
}