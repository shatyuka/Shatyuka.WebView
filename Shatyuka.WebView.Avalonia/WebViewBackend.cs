using Avalonia.Controls;
using Avalonia.Platform;

namespace Shatyuka.WebView.Avalonia;

public class WebViewBackend : NativeControlHost
{
    protected nint NativeControlHandle;

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        var platformHandle = base.CreateNativeControlCore(parent);
        NativeControlHandle = platformHandle.Handle;
        return platformHandle;
    }
}
