using System;

namespace DungeonGame
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (Game1 game1 = new Game1())
                game1.Run();
        }
    }
}
