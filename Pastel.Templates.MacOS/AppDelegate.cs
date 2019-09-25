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
            var rectangle1 = new Rectangle(new Vector2(-0.5f, 0.5f), 10);
            rectangle1.CreateResources();

            game = new PastelGame();
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