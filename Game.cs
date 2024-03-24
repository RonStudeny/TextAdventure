using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Text;

namespace TextAdventure
{
    public class Game
    {
        public static void Run()
        {
            InputHandler.Conversation(TextSource.welcome, TextSource.mainMenuOptions, out int res);
            switch (res)
            {
                case 0:
                    // New game prompt 
                    break;
                case 1:
                    // Load
                    break;
                case 2:
                    // Help menu
                    break;
                case 3:
                    // Exit the game
                    break;

            }
        }
    }
}
