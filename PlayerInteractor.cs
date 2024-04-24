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
;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine(message + "\n");
                for (int i = 0; i < options.Length; i++)
                    Console.WriteLine($"{i + 1}. {options[i]}");

                if (int.TryParse(Console.ReadLine(), out res) && res > 0 && res < options.Length)
                {
                    res--;
                    loop = false;
                }                   
                else
                {
                    Console.WriteLine("Invalid input, type a corresponding number...");
                    Console.ReadLine();
                }

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
                Game.location = Location.GetNewLocation();
                Game.nextLocation = Location.GetNewLocation();
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

        // Main "cycle" in which player chooses actions and the game responds, the loop is done recursivly as to avoid unneceseary conditions
        public static void GameLoop()
        {
            Conversation($"{TextSource.mainLoopText} {TextSource.locations[0]}", TextSource.mainLoopOptions, out int res);
            AssesResponse(res, ChangeLocation, SearchLocation, ShowInventory);
            GameLoop();

        }

        public static void ChangeLocation()
        {
            Conversation(Game.nextLocation.Narrative, new string[] { "Go to the: " + Game.nextLocation.Name, "Back" }, out int res);
            if (res == 0)
            {
                Game.location = Game.nextLocation;
                Game.nextLocation = Location.GetNewLocation();
            }
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
