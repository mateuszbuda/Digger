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
            GameForm form = new GameForm();
            form.Show();
            DiggerGame game = new DiggerGame(form);
            game.Run();
        }
    }
#endif
}

