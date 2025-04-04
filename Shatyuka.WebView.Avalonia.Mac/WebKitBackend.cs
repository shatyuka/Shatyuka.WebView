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
        NSApplication.Init();
        _webView = new WKWebView(CGRect.Empty, new WKWebViewConfiguration());
        var userAgent = _webView.ValueForKey(new NSString("userAgent")).ToString();
        var webKitVersion = userAgent.Split(" ").FirstOrDefault(s => s.StartsWith("AppleWebKit/"));
        if (webKitVersion != null)
        {
            _webView.CustomUserAgent = string.Concat(userAgent, " Safari/", webKitVersion.AsSpan("AppleWebKit/".Length));
        }

        ControlHandle = _webView.Handle;
        return new PlatformHandle(ControlHandle, "WebKit");
    }

    private void SyncBounds()
    {
        if (_webView != null)
        {
            _webView.Frame = new CGRect(0, 0, Bounds.Width, Bounds.Height);
        }
    }
}
