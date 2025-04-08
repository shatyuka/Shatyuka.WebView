using Avalonia.Controls;

namespace Shatyuka.WebView.Avalonia;

public class WebView : Border
{
    public WebView()
    {
        var provider = WebViewBackendProvider.BackendProviders.Find(provider => provider.Available());
        if (provider == null)
        {
            throw new InvalidOperationException("Could not find available provider.");
        }

        var backend = provider.Create();
        Child = backend;
    }
}
