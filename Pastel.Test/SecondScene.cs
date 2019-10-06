using System;
using System.Numerics;
using Pastel.Core.Models;
using Pastel.Core.Platform.Input;
using Veldrid;

namespace Pastel.Test
{
    public class SecondScene : PastelScene
    {
        PastelGame Game;
        public SecondScene(PastelGame game)
        {
            BackgroundColour = RgbaFloat.DarkRed;
            Game = game;

            var rectangle = new Rectangle(new Vector2(-0.4f, 0.3f), 20);

            SceneObjects.Add(rectangle);
        }

        public override void SceneUpdate()
        {
            if (InputManager.Buttons.Find(b => b.Name == "Menu").Pressed)
                Game.RemoveScene();

        }
    }
}
