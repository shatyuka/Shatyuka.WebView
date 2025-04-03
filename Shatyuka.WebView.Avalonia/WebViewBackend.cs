using Avalonia.Controls;
using Avalonia.Platform;

namespace Shatyuka.WebView.Avalonia;

public class WebViewBackend : NativeControlHost
{
    protected nint ControlHandle;

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        var platformHandle = base.CreateNativeControlCore(parent);
        ControlHandle = platformHandle.Handle;
        return platformHandle;
    }
}
