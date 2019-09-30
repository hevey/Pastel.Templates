using System.Numerics;
using AppKit;
using Foundation;
using Pastel.Core.Models;
using Pastel.Test;

namespace Pastel.Templates.MacOS
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        private PastelGame game;

        public override void DidFinishLaunching(NSNotification notification)
        {

            game = new TestGame();
            game.Run();
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }

        public override void WillTerminate(NSNotification notification)
        {
            game.Dispose();
        }
    }
}