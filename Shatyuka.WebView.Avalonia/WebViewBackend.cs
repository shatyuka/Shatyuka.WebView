using Avalonia.Controls;
using Avalonia.Platform;

namespace Shatyuka.WebView.Avalonia;

public class WebViewBackend : NativeControlHost, IWebViewBackend
{
    protected nint NativeControlHandle;

    protected WebViewBackend()
    {
    }

    public event EventHandler<EventArgs>? BackendCreated;
    public event EventHandler<EventArgs>? NavigationCompleted;

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        var platformHandle = base.CreateNativeControlCore(parent);
        NativeControlHandle = platformHandle.Handle;
        return platformHandle;
    }

    protected void RaiseBackendCreated()
    {
        BackendCreated?.Invoke(this, EventArgs.Empty);
    }

    protected void RaiseNavigationCompleted()
    {
        NavigationCompleted?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void SyncBounds()
    {
        throw new NotImplementedException();
    }

    public virtual bool Navigate(Uri? uri)
    {
        throw new NotImplementedException();
    }
}
