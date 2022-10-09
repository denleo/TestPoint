using Core.Views;

namespace Core;

public abstract class PresenterBase<TView> where TView : class, IViewBase
{
    public TView View { get; set; }

    protected PresenterBase(TView view)
    {
        View = view;
        View.Load += ViewOnLoad;
    }

    protected virtual void ViewOnLoad(object? sender, EventArgs e) { }
}