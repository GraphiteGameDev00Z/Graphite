using System;

namespace Graphite
{
#if WINDOWS || XBOX
    static class Program
    {

        static void Main(string[] args)
        {
            LaunchScreen Launcher = new LaunchScreen();
            //Show Launcher Menu       
            Launcher.ShowDialog();

            if (Launcher.blnStartGame == true)
            {
                // Start Game
                using (Main game = new Main())
                {
                    game.Run();
                }
            }
        } 
    }
#endif
}

