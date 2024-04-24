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

        public static string mainLoopText = "You are in a:";
        public static string[] mainLoopOptions = { "Go", "Search", "Inventory" };

        public static string[] locations = { "Forest", "Abondened city", "Military base" };
        public static string[] locationNarratives = { 
            "A dark forest full of mistery opens before your",
            "You see the edge of a once blooming city, now abondened to it's fate",
            "You've sumbled upon a military base"
        };

        public static string changeLocationText = "Go to the: ";

    }
}
