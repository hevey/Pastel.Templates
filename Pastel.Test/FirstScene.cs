using System.Numerics;
using Pastel.Core.Models;
using Pastel.Core.Platform.Input;
using Veldrid;

namespace Pastel.Test
{
    public class FirstScene : PastelScene
    {
        PastelGame Game;

        public FirstScene(PastelGame game)
        {
            BackgroundColour = RgbaFloat.CornflowerBlue;
            Game = game;

            var rec = new Rectangle(new Vector2(0.5f, -0.5f), 10);

            SceneObjects.Add(rec);
        }

        public override void SceneUpdate()
        {
            if (InputManager.Buttons.Find(b => b.Name == "Menu").Pressed)
                Game.AddScene(new SecondScene(Game));
        }
    }
}