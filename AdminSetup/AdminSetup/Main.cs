using Autofac;
using Core;
using Core.Views;

namespace AdminSetup;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
    }

    private void AdminSetupMenuItem_Click(object sender, EventArgs e) => RunNewTab<IAdminSetupView>();

    private void LoggingMenuItem_Click(object sender, EventArgs e) { }

    private void RunNewTab<TView>()
    where TView : class, IViewBase
    {
        var presenter = Program.Container!.Resolve<PresenterBase<TView>>();

        if (presenter.View is Form form)
        {
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(form);
            form.Show();
        }
    }
}