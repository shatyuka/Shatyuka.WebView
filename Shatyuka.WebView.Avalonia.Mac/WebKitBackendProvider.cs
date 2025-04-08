using System;

namespace Shatyuka.WebView.Avalonia.Mac;

internal class WebKitBackendProvider : IWebViewBackendProvider
{
    public bool Available()
    {
        return OperatingSystem.IsMacOSVersionAtLeast(10, 10);
    }

    public WebViewBackend Create()
    {
        return new WebKitBackend();
    }
}
