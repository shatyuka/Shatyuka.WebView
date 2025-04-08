using Microsoft.Web.WebView2.Core;

namespace Shatyuka.WebView.Avalonia.Windows;

internal class WebView2BackendProvider : IWebViewBackendProvider
{
    public bool Available()
    {
        try
        {
            return CoreWebView2Environment.GetAvailableBrowserVersionString().Length > 0;
        }
        catch (WebView2RuntimeNotFoundException)
        {
            return false;
        }
    }

    public WebViewBackend Create()
    {
        return new WebView2Backend();
    }
}
