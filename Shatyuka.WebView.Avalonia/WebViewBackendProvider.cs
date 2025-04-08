namespace Shatyuka.WebView.Avalonia;

public static class WebViewBackendProvider
{
    public static readonly List<IWebViewBackendProvider> BackendProviders = [];

    public static void Register<TProvider>() where TProvider : IWebViewBackendProvider, new()
    {
        Register(new TProvider());
    }

    public static void Register(IWebViewBackendProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);
        if (BackendProviders.Any(exist => exist.GetType() == provider.GetType()))
        {
            throw new ArgumentException($"Provider {provider.GetType()} is already registered.");
        }
        BackendProviders.Add(provider);
    }
}
