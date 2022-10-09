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
            this.components = new System.ComponentModel.Container();
            this.AdminUsernameTB = new System.Windows.Forms.TextBox();
            this.CreateBtn = new System.Windows.Forms.Button();
            this.ResetPasswordBtn = new System.Windows.Forms.Button();
            this.UsernameErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.AdminSetupLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameErrorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AdminUsernameTB
            // 
            this.AdminUsernameTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdminUsernameTB.Location = new System.Drawing.Point(21, 12);
            this.AdminUsernameTB.MaxLength = 16;
            this.AdminUsernameTB.Name = "AdminUsernameTB";
            this.AdminUsernameTB.Size = new System.Drawing.Size(208, 27);
            this.AdminUsernameTB.TabIndex = 0;
            this.AdminUsernameTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CreateBtn
            // 
            this.CreateBtn.Location = new System.Drawing.Point(21, 59);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(94, 29);
            this.CreateBtn.TabIndex = 1;
            this.CreateBtn.Text = "Create";
            this.CreateBtn.UseVisualStyleBackColor = true;
            // 
            // ResetPasswordBtn
            // 
            this.ResetPasswordBtn.Location = new System.Drawing.Point(135, 59);
            this.ResetPasswordBtn.Name = "ResetPasswordBtn";
            this.ResetPasswordBtn.Size = new System.Drawing.Size(94, 29);
            this.ResetPasswordBtn.TabIndex = 2;
            this.ResetPasswordBtn.Text = "Reset";
            this.ResetPasswordBtn.UseVisualStyleBackColor = true;
            // 
            // UsernameErrorProvider
            // 
            this.UsernameErrorProvider.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.AdminUsernameTB);
            this.panel1.Controls.Add(this.ResetPasswordBtn);
            this.panel1.Controls.Add(this.CreateBtn);
            this.panel1.Location = new System.Drawing.Point(242, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 101);
            this.panel1.TabIndex = 3;
            // 
            // AdminSetupLabel
            // 
            this.AdminSetupLabel.AutoSize = true;
            this.AdminSetupLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AdminSetupLabel.Location = new System.Drawing.Point(303, 126);
            this.AdminSetupLabel.Name = "AdminSetupLabel";
            this.AdminSetupLabel.Size = new System.Drawing.Size(126, 28);
            this.AdminSetupLabel.TabIndex = 4;
            this.AdminSetupLabel.Text = "Admin Setup";
            // 
            // AdminSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(732, 422);
            this.Controls.Add(this.AdminSetupLabel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminSetup";
            ((System.ComponentModel.ISupportInitialize)(this.UsernameErrorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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