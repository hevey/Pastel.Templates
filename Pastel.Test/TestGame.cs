using System.Numerics;
using Pastel.Core.Models;

namespace Pastel.Test
{
    public class TestGame: PastelGame
    {
        public TestGame()
        {
            var rectangle1 = new Rectangle(new Vector2(0.0f, 0.0f), 10);
            rectangle1.CreateResources();
        }
    }
}
