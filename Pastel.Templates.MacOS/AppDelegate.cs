using AppKit;
using Foundation;
using System.Numerics;
using Pastel.Core.Models;

namespace Pastel.Templates.MacOS
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        PastelGame game;
        public AppDelegate()
        {
        }
        

        public override void DidFinishLaunching(NSNotification notification)
        {
            var rectangle1 = new Rectangle(new Vector2(-0.5f, 0.5f), 10);
            rectangle1.CreateResources();

            var rectangle2 = new Rectangle(new Vector2(0.5f, 0.5f), 25);
            rectangle2.CreateResources();

            var rectangle3 = new Rectangle(new Vector2(0.5f, -0.5f), 50);
            rectangle3.CreateResources();

            var rectangle4 = new Rectangle(new Vector2(-0.5f, -0.5f), 45);
            rectangle4.CreateResources();

            //var square = new Square(
            //    new Vector2(-.75f, .75f),
            //    new Vector2(-.25f, .75f),
            //    new Vector2(-.75f, .25f),
            //    new Vector2(-.25f, .25f));
            //square.CreateResources();

            //var square2 = new Square(
            //    new Vector2(.25f, .75f),
            //    new Vector2(.75f, .75f),
            //    new Vector2(.25f, .25f),
            //    new Vector2(.75f, .25f));
            //square2.CreateResources();

            //var square3 = new Square(
            //    new Vector2(-.75f, -.25f),
            //    new Vector2(-.25f, -.25f),
            //    new Vector2(-.75f, -.75f),
            //    new Vector2(-.25f, -.75f));
            //square3.CreateResources();

            //var square4 = new Square(
            //    new Vector2( .25f, -.25f),
            //    new Vector2( .75f, -.25f),
            //    new Vector2( .25f, -.75f),
            //    new Vector2( .75f, -.75f));
            //square4.CreateResources();

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
