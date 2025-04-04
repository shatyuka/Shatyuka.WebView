using Avalonia.Interactivity;
using Avalonia.Platform;
using Microsoft.Web.WebView2.Core;
using System.Drawing;

namespace Shatyuka.WebView.Avalonia.Windows;

public class WebView2Backend : WebViewBackend
{
    private CoreWebView2Environment? _environment;
    private CoreWebView2Controller? _controller;
    private CoreWebView2? _webView2;

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
            _controller = await _environment.CreateCoreWebView2ControllerAsync(ControlHandle);
            _controller.BoundsMode = CoreWebView2BoundsMode.UseRasterizationScale;
            SyncBounds();

            _webView2 = _controller.CoreWebView2;
            _webView2.Navigate("https://www.google.com/");
        }
    }

    private void SyncBounds()
    {
        if (_controller != null)
        {
            _controller.Bounds = new Rectangle((int)Bounds.X, (int)Bounds.Y, (int)Bounds.Width, (int)Bounds.Height);
        }
    }
}
