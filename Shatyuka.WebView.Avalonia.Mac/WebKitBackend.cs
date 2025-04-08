using Avalonia.Interactivity;
using Avalonia.Platform;
using WebKit;

namespace Shatyuka.WebView.Avalonia.Mac;

internal class WebKitBackend : WebViewBackend
{
    private WKWebView? _webView;

    protected override void OnLoaded(RoutedEventArgs e)
    {
        SyncBounds();
        _webView?.LoadRequest(new NSUrlRequest(new NSUrl("https://www.google.com/")));
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        _webView?.Dispose();
        _webView = null;
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        try
        {
            NSApplication.Init();
        }
        catch (InvalidOperationException)
        {
            // Ignore already initialized exception
        }

        _webView = new WKWebView(CGRect.Empty, new WKWebViewConfiguration());
        var userAgent = _webView.ValueForKey(new NSString("userAgent")).ToString();
        var webKitVersion = userAgent.Split(" ").FirstOrDefault(s => s.StartsWith("AppleWebKit/"));
        if (webKitVersion != null)
        {
            _webView.CustomUserAgent = string.Concat(userAgent, " Safari/", webKitVersion.AsSpan("AppleWebKit/".Length));
        }

        NativeControlHandle = _webView.Handle;
        return new PlatformHandle(_webView.Handle, "WebKit");
    }

    private void SyncBounds()
    {
        if (_webView != null)
        {
            _webView.Frame = new CGRect(0, 0, Bounds.Width, Bounds.Height);
        }
    }
}
