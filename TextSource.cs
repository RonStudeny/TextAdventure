using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Text
{
    public class TextSource
    {
        public static string mmWelocome = "Welcome to text adventure, please select an action";
        public static string[] mmOptions = { "Start", "Load", "Help", "Quit" };

        public static string newGameText = "You are about to start a new game, are you sure?";
        public static string[] newGameOptions = { "Yes", "Back" };

        public static string helpText = "*very useful info*";
        public static string[] helpOptions = { "Back" };

        public static string loopMainText = "You are in a: ";
        public static string[] loopMainOptions = { "Go", "Search", "Inventory" };
        public static string[] locations = { "Forest", "City", "Whatever" };

    }
}
