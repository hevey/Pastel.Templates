using System.Numerics;
using Pastel.Core.Models;

namespace Pastel.Test
{
    public class TestGame: PastelGame
    {
        public TestGame()
        {
            var rec = new Rectangle(new Vector2(0.5f, -0.5f), 10);
            rec.CreateResources();
            GameObjects.Add(rec);

            AddScene(new FirstScene(this));
        }
    }
}
