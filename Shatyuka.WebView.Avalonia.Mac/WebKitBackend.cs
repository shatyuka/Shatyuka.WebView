using AppKit;
using Avalonia.Interactivity;
using Avalonia.Platform;
using CoreGraphics;
using Foundation;
using System;
using System.Linq;
using WebKit;

namespace Shatyuka.WebView.Avalonia.Mac;

public class WebKitBackend : WebViewBackend
{
    public WKWebView? WebView => _webView;

    private WKWebView? _webView;

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

        // Pretend to be Safari
        var userAgent = _webView.ValueForKey(new NSString("userAgent")).ToString();
        var webKitVersion = userAgent.Split(" ").FirstOrDefault(s => s.StartsWith("AppleWebKit/"));
        if (webKitVersion != null)
        {
            _webView.CustomUserAgent = string.Concat(userAgent, " Safari/", webKitVersion.AsSpan("AppleWebKit/".Length));
        }

        _webView.NavigationDelegate = new WebKitNavigationDelegate(this);

        RaiseBackendCreated();

        NativeControlHandle = _webView.Handle;
        return new PlatformHandle(_webView.Handle, "WebKit");
    }

    public override bool Navigate(Uri? uri)
    {
        if (_webView == null)
            return false;
        if (uri == null)
            return false;
        return _webView.LoadRequest(new NSUrlRequest(new NSUrl(uri.AbsoluteUri))) != null;
    }

    internal void RaiseWebKitNavigationCompleted()
    {
        RaiseNavigationCompleted();
    }
}
