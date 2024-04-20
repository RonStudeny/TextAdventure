using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.DataStructure;
using TextAdventure.Text;

//#pragma warning disable CS8604 // Possible null reference argument.

namespace TextAdventure
{
    /// <summary>
    /// The point of this class is to provide functions that help the player interact with the game, while being easy to use and re-use from the developers perspective
    /// </summary>
    public class PlayerInteractor
    {
        public delegate void ResponseBehaviour();

        /// <summary>
        /// Generic function for creating basic conversation loops, prints a message and options with which to respond
        /// </summary>
        /// <param name="message">The message that begins the conversation</param>
        /// <param name="options">An array of options, that will be given to the user to choose from</param>
        /// <param name="res">A number coresponding to the chosen option, starting from 0</param>
        public static void Conversation(string message, string[] options, out int res)
        {
            bool loop = true;
            res = -1;

            while (loop)
            {
                // Write the initial message and print out the options the player has
                Console.Clear();
                Console.WriteLine(message + "\n");
                for (int i = 0; i < options.Length; i++)
                    Console.Write($"| {options[i]} ");
                Console.Write("|\n");

                // Get response from the player, match it to the options and return the index of the chosen option
                string response = Console.ReadLine().ToLower();
                for (int i = 0; i < options.Length; i++)
                    if (response == options[i].ToLower()) res = i;
                // end the conversation loop if a valid answer is given
                loop = res < 0;
            }
        }
        public static void AssesResponse(int res, params Action[] funcs) => funcs[res].Invoke();

        public static void MainMenu()
        {
            Conversation(TextSource.mmWelocome, TextSource.mmOptions, out int res);
            AssesResponse(res, NewGame, LoadGame, Help, Exit);
            MainMenu();

        }

        public static void NewGame()
        {
            Conversation(TextSource.newGameText, TextSource.newGameOptions, out int res);
            if (res == 0)
            {
                Game.player = new Player();
                // set all the stuff
            }
            GameLoop();
        }

        public static void LoadGame()
        {
            Console.WriteLine("*Load game menu*");
            Service.LoadFromFile();
            GameLoop();
        }

        public static void Help()
        {
            Conversation(TextSource.helpText, TextSource.helpOptions, out int res);
        }

        public static void Exit() // exits the game
        {
            Console.WriteLine("*Game exited*");
            Environment.Exit(0);
        }

        public static void GameLoop()
        {
            Conversation($"{TextSource.loopMainText} {TextSource.locations[0]}", TextSource.loopMainOptions, out int res);
            AssesResponse(res, ChangeLocation, SearchLocation, ShowInventory);
            GameLoop();

        }

        public static void ChangeLocation()
        {
            Console.WriteLine("*location change dialogue*");
            Console.ReadLine();
        }

        public static void SearchLocation()
        {
            Console.WriteLine("*search dialogue*");
            Console.ReadLine();
        }

        public static void ShowInventory()
        {
            Console.WriteLine("*inventory dialogue*");
            Console.ReadLine();
        }





    }
}
