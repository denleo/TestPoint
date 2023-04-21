namespace AdminSetup.Tabs
{
    partial class AdminSetup
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
            components = new System.ComponentModel.Container();
            AdminUsernameTB = new TextBox();
            CreateBtn = new Button();
            ResetPasswordBtn = new Button();
            UsernameErrorProvider = new ErrorProvider(components);
            panel1 = new Panel();
            AdminSetupLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)UsernameErrorProvider).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // AdminUsernameTB
            // 
            AdminUsernameTB.BorderStyle = BorderStyle.FixedSingle;
            AdminUsernameTB.Location = new Point(21, 12);
            AdminUsernameTB.MaxLength = 16;
            AdminUsernameTB.Name = "AdminUsernameTB";
            AdminUsernameTB.Size = new Size(208, 27);
            AdminUsernameTB.TabIndex = 0;
            AdminUsernameTB.TextAlign = HorizontalAlignment.Center;
            // 
            // CreateBtn
            // 
            CreateBtn.Location = new Point(21, 59);
            CreateBtn.Name = "CreateBtn";
            CreateBtn.Size = new Size(94, 29);
            CreateBtn.TabIndex = 1;
            CreateBtn.Text = "Create";
            CreateBtn.UseVisualStyleBackColor = true;
            // 
            // ResetPasswordBtn
            // 
            ResetPasswordBtn.Location = new Point(135, 59);
            ResetPasswordBtn.Name = "ResetPasswordBtn";
            ResetPasswordBtn.Size = new Size(94, 29);
            ResetPasswordBtn.TabIndex = 2;
            ResetPasswordBtn.Text = "Reset";
            ResetPasswordBtn.UseVisualStyleBackColor = true;
            // 
            // UsernameErrorProvider
            // 
            UsernameErrorProvider.ContainerControl = this;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(AdminUsernameTB);
            panel1.Controls.Add(ResetPasswordBtn);
            panel1.Controls.Add(CreateBtn);
            panel1.Location = new Point(104, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(248, 102);
            panel1.TabIndex = 3;
            // 
            // AdminSetupLabel
            // 
            AdminSetupLabel.AutoSize = true;
            AdminSetupLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            AdminSetupLabel.Location = new Point(166, 51);
            AdminSetupLabel.Name = "AdminSetupLabel";
            AdminSetupLabel.Size = new Size(126, 28);
            AdminSetupLabel.TabIndex = 4;
            AdminSetupLabel.Text = "Admin Setup";
            // 
            // AdminSetup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(460, 230);
            Controls.Add(AdminSetupLabel);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AdminSetup";
            ((System.ComponentModel.ISupportInitialize)UsernameErrorProvider).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox AdminUsernameTB;
        private Button CreateBtn;
        private Button ResetPasswordBtn;
        private ErrorProvider UsernameErrorProvider;
        private Panel panel1;
        private Label AdminSetupLabel;
    }
}