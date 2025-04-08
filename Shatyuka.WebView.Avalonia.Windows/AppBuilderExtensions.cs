using Avalonia;

namespace Shatyuka.WebView.Avalonia.Windows;

public static class AppBuilderExtensions
{
    public static AppBuilder UseWindowsWebView(this AppBuilder builder)
    {
        return builder.AfterPlatformServicesSetup(_ =>
        {
            WebViewBackendProvider.Register<WebView2BackendProvider>();
        });
    }
}
