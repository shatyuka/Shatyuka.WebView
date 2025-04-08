using Avalonia;
using Avalonia.Interactivity;
using Microsoft.Web.WebView2.Core;
using System.Drawing;

namespace Shatyuka.WebView.Avalonia.Windows;

public class WebView2Backend : WebViewBackend
{
    private CoreWebView2Environment? _environment;
    private CoreWebView2Controller? _controller;
    public CoreWebView2? WebView2 => _webView2;
    private CoreWebView2? _webView2;

    public WebView2Backend()
    {
        BoundsProperty.Changed.AddClassHandler<Visual, Rect>((_, _) => SyncBounds());
    }

    protected override async void OnLoaded(RoutedEventArgs e)
    {
        await EnsureCoreWebView2Async();
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        _controller?.Close();
        _controller = null;
        _webView2 = null;
    }

    private async Task EnsureCoreWebView2Async()
    {
        _environment ??= await CoreWebView2Environment.CreateAsync();
        if (_controller == null)
        {
            _controller = await _environment.CreateCoreWebView2ControllerAsync(NativeControlHandle);
            _controller.BoundsMode = CoreWebView2BoundsMode.UseRasterizationScale;
            SyncBounds();

            _webView2 = _controller.CoreWebView2;

            _webView2.NavigationCompleted += (_, _) => RaiseNavigationCompleted();

            RaiseBackendCreated();
        }
    }

    protected override void SyncBounds()
    {
        if (_controller != null)
            _controller.Bounds = new Rectangle((int)Bounds.X, (int)Bounds.Y, (int)Bounds.Width, (int)Bounds.Height);
    }

    public override bool Navigate(Uri? uri)
    {
        if (_webView2 == null)
            return false;
        if (uri == null)
            return false;
        _webView2.Navigate(uri.ToString());
        return true;
    }
}
