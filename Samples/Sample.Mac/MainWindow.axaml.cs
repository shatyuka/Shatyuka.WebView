using Avalonia.Controls;
using Shatyuka.WebView.Avalonia;
using System;

namespace Sample.Mac;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void WebView_OnNavigationCompleted(object? sender, EventArgs e)
    {
        var webView = (WebView)sender!;
        Console.WriteLine($"WebView_OnNavigationCompleted: {webView.Url}");
    }
}
