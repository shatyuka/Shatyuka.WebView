using WebKit;

namespace Shatyuka.WebView.Avalonia.Mac;

public class WebKitNavigationDelegate(WebKitBackend backend) : WKNavigationDelegate
{
    public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
    {
        backend.RaiseWebKitNavigationCompleted();
    }
}
