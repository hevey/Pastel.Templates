using System;
using System.Text;
using AppKit;
using Foundation;
using OpenTK;
using Pastel.Core.Models;
using Pastel.Core.Platform.Graphics;
using Pastel.Core.Platform.Window;
using Veldrid;
using Veldrid.SPIRV;

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

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
