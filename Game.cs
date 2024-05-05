using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.DataStructure;
using TextAdventure.Text;

namespace TextAdventure
{
    public class Game
    {
        public static GameData currentGame = new GameData();
        public static void Main()
        {
            GameInteractor.MainMenu();

        }
    }
}
