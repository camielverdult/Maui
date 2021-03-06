using AppKit;
using Foundation;

//namespace Maui.macOS
//{
//    [Register("AppDelegate")]
//    public class AppDelegate : NSApplicationDelegate
//    {
//        public AppDelegate()
//        {
//        }

//        public override void DidFinishLaunching(NSNotification notification)
//        {
//            // Insert code here to initialize your application
//        }

//        public override void WillTerminate(NSNotification notification)
//        {
//            // Insert code here to tear down your application
//        }
//    }
//}

using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace Maui.macOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        NSWindow window;
        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled;

            var rect = new CoreGraphics.CGRect(200, 1000, 1024, 768);
            window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            window.Title = "Maui"; // choose your own Title here
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow
        {
            get { return window; }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            Forms.Init();
            LoadApplication(new App());
            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }

}