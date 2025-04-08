namespace Shatyuka.WebView.Avalonia;

public interface IWebViewBackendProvider
{
    public bool Available();
    public WebViewBackend Create();
}
