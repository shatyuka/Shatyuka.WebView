namespace Shatyuka.WebView.Avalonia;

public interface IWebViewBackend
{
    public bool Navigate(Uri? uri);
}
