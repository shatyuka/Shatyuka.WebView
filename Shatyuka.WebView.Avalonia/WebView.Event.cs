namespace Shatyuka.WebView.Avalonia;

public partial class WebView
{
    public event EventHandler<EventArgs>? BackendCreated;
    public event EventHandler<EventArgs>? NavigationCompleted;

    private void RegisterEvents()
    {
        Backend.BackendCreated += (_, args) =>
        {
            Navigate(Url);
            BackendCreated?.Invoke(this, args);
        };
        Backend.NavigationCompleted += (_, args) => NavigationCompleted?.Invoke(this, args);
    }
}
