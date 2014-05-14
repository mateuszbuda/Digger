using System;

namespace Digger
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            gameForm form = new gameForm();
            form.Show();
            DiggerGame game = new DiggerGame(form.getDrawSurface());
            game.Run();
        }
    }
#endif
}

