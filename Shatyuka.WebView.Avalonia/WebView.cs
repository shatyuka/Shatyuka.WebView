using Avalonia.Controls.Presenters;

namespace Shatyuka.WebView.Avalonia;

public partial class WebView : ContentPresenter
{
    public WebViewBackend Backend { get; }

    public WebView()
    {
        var provider = WebViewBackendProvider.BackendProviders.Find(provider => provider.Available());
        if (provider == null)
            throw new InvalidOperationException("Could not find available provider.");

        Backend = provider.Create();
        Content = Backend;

        RegisterPropertyChangedCallback();
        RegisterEvents();
    }

    public bool Navigate(Uri? uri)
    {
        return Backend.Navigate(uri);
    }
}
