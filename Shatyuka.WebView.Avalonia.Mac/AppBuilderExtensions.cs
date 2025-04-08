using Avalonia;

namespace Shatyuka.WebView.Avalonia.Mac;

public static class AppBuilderExtensions
{
    public static AppBuilder UseMacWebView(this AppBuilder builder)
    {
        return builder.AfterPlatformServicesSetup(_ =>
        {
            WebViewBackendProvider.Register<WebKitBackendProvider>();
        });
    }
}
