using Core.Models;
using Core.Views;

namespace AdminSetup.Tabs;

public partial class AdminSetup : Form, IAdminSetupView
{
    public event EventHandler? CreateEvent;
    public event EventHandler? ResetPasswordEvent;

    public string Username => AdminUsernameTB.Text;

    public string ValidationError
    {
        set => UsernameErrorProvider.SetError(AdminUsernameTB, value);
    }

    public bool UiBlocked
    {
        get => !CreateBtn.Enabled;
        set => CreateBtn.Enabled = ResetPasswordBtn.Enabled = !value;
    }

    public AdminSetup()
    {
        TopLevel = false;
        InitializeComponent();

        CreateBtn.Click += (sender, args) => CreateEvent!.Invoke(sender, args);
        ResetPasswordBtn.Click += (sender, args) => ResetPasswordEvent!.Invoke(sender, args);
    }

    public void ShowAlert(string text, string? clipboardCopy = null)
    {
        if (clipboardCopy is not null)
        {
            if (MessageBox.Show(text, Constants.AppTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.None) == DialogResult.OK)
            {
                Clipboard.SetText(clipboardCopy);
            }
        }
        else
        {
            MessageBox.Show(text, Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}