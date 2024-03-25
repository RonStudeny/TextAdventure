using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Text;

//#pragma warning disable CS8604 // Possible null reference argument.

namespace TextAdventure
{
    public class PlayerInteractor
    {
        public delegate void ResponseDelegate();

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
                Console.WriteLine(message);
                for (int i = 0; i < options.Length; i++)
                    Console.Write($"{options[i]} | ");
                Console.WriteLine();

                // Get response from the player, match it to the options and return the index of the chosen option
                string response = Console.ReadLine().ToLower();
                for (int i = 0; i < options.Length; i++)
                    if (response == options[i].ToLower()) res = i;
                // end the conversation loop if a valid answer is given
                loop = res < 0;
            }
        }

        //  

        public static void AssesResponse(int res, params ResponseDelegate[] funcs) => funcs[res]();

        #region MainMenu

        

        #endregion

    }
}
