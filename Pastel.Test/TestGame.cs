using System.Numerics;
using Pastel.Core.Models;

namespace Pastel.Test
{
    public class TestGame: PastelGame
    {
        public TestGame()
        {
            AddScene(new FirstScene(this));
        }
    }
}
