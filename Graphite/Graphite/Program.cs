    using System;

namespace Graphite
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        
        
        static void Main(string[] args)
        {
            //Variables


            using (Game1 game = new Game1())
            {
                  game.Run();
                  while (true) { }
            }  
        }
    }
#endif
}

