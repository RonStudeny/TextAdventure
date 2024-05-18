using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Text
{
    public class TextSource
    {
        public static string saveFileDir = "saves";
        public static string mmWelocome = "Welcome to text adventure, please select an action";
        public static string[] mmOptions = { "Start", "Load", "Help", "Quit" };

        public static string newGameText = "You are about to start a new game, are you sure?";
        public static string[] newGameOptions = { "Yes" };

        public static string helpText = "*very useful info*";
        public static string[] helpOptions = { "help1", "help2" };

        public static string loadGameText = "Please select a save file:";
        public static string loadGameLoading = "Loading the game...";
        public static string loadGameFailed = "Couldn't load the save file, it may be corrupted. Reason:";

        public static string saveGameText = "Type file name to save your progress\nTip: Type an existing file name to override the given file or type 'cancel' to cancel saving";
        public static string saveGameSuccess = "Game saved successfuly as";
        public static string saveGameFailure = "Error while saving the game";
        public static string saveGameCancel = "Saving canceled...";

        public static string mainLoopText = "You are in a";
        public static string[] mainLoopOptions = { "Go", "Search", "Inventory", "Save game", "Quit" };

        public static string changeLocationText = "Go to the: ";


        public static string searchNothingLeft = "There's nothing left to search in this location...";
        public static string itemFoundText = "You've found a";
        public static string itemDiscardedText = "You've discarded a";
        public static string[] itemFoundOptions = { "Keep", "Discard" };

        public static string enemyEncounterText = "You've encountred a";
        public static string[] enemyEncounterOptions = { "Fight ", "Item", "Run" };

        public static string enemyFightText = "Choose weapon to fight with";

        public static string consumableHealthFull = "Your health is already full";
        public static string chooseConsumableText = "Choose wich item to use";

        public static string runSuccessfulText = "You've decided that fighting isn't an option and ran away";
        public static string runUnsuccessfulText = "You tried to run away but you triped and fell and the enemy has caught up";

        public static string exitText = "Any unsaved progress will be lost, are you sure?";
        public static string[] exitOptions = { "YES - exit", "NO - cancel" };


    }
}
