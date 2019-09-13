using AppKit;
using Foundation;
using Pastel.Core.Models;

namespace Pastel.Templates.MacOS
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {

        public AppDelegate()
        {
        }
        

        public override void DidFinishLaunching(NSNotification notification)
        {
            var game = new PastelGame();

            game.Run();
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }
    }
}
