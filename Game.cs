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
            InputHandler.AssesResponse(res, InputHandler.NewGame, InputHandler.LoadGame, InputHandler.Help, InputHandler.Exit);
        }
    }
}
