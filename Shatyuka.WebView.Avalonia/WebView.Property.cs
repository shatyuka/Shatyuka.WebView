using Avalonia;

namespace Shatyuka.WebView.Avalonia;

public partial class WebView
{
    public static readonly StyledProperty<Uri?> UrlProperty = AvaloniaProperty.Register<WebView, Uri?>(nameof(Url));

    public Uri? Url
    {
        get => GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }

    private static void RegisterPropertyChangedCallback()
    {
        UrlProperty.Changed.AddClassHandler<WebView, Uri?>((view, args) => view.Navigate(args.NewValue.Value));
    }
}
