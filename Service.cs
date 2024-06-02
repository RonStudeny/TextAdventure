using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.DataStructure;
using TextAdventure.Text;

namespace TextAdventure
{
    /// <summary>
    /// Contains static methods for Saving and Loading GameData class instances
    /// </summary>
    public class FileAccess
    {
        /// <summary>
        /// Serializes the GameData instance and tries to save it to the location, if succesful, returns true and the exception variable stays null
        /// </summary>
        /// <param name="fileName">Specified file name, if exists, function overrides existing data</param>
        /// <param name="game">GameData instance to serialize</param>
        /// <param name="e">out argument containing the exception caused by an unsucessful file writing, otherwise null</param>
        /// <returns>true if all text is written successfuly, otherwise false and e argument contains the exception</returns>
        public static bool SaveGameToFile(string fileName, GameData game, out Exception? e)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            fileName = fileName + ".json";
            string path = Path.Combine(TextSource.saveFileDir, fileName);
            string jsonPayload = JsonConvert.SerializeObject(game, settings);
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(path, jsonPayload);
                e = null;
                return true;
            }
            catch (Exception exc)
            {
                e = exc;
                return false;
            }

        }

        /// <summary>
        /// Deserializes data at the specified path into GameData class instance
        /// <param name="filePath">Specified path for the .json file</param>
        /// <param name="game">out arguement that will contain the desrielized object if successful</param>
        /// <param name="e">arguement will contain an exception if unsuccessful, otherwise null</param>
        /// <returns>true if object is read and deserialized successfuly, otherwise false</returns>
        public static bool LoadFromFile(string filePath, out GameData game, out Exception? e)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            try
            {
                game = JsonConvert.DeserializeObject<GameData>(File.ReadAllText(filePath), settings);
                e = null;
                return true;
            }
            catch(Exception exc)
            {
                e = exc;
                game = null;
                return false;
            }
        }

    }

    /// <summary>
    /// Provides static methods that are used often through out the code, 
    /// </summary>

    public static class Helpers
    {
        /// <summary>
        /// Function that provides a conversation loop with the player in the form of giving a prompt and several numbered options and expecting a number as a response 
        /// </summary>
        /// <param name="message">The message that player is prompted to respond to</param>
        /// <param name="options">List of options for the player to choose from</param>
        /// <param name="res">A number coresponding to the chosen option, starting from 0, if "back" was selected, returns -1</param>
        /// <param name="backOption">bool indicating if a "back" option should be added</param>
        /// <param name="backText">Optional parameter overriding the displayed name of the "back" option, is set to "Back" as default</param>
        public static void Conversation(string message, List<string> options, out int res, bool backOption, string backText = "Back")
        {
            bool loop = true;
            res = -1;
            if (backOption) // if desired, add cancel or back as an option
                options.Add(backText);
            
            while (loop)
            {
                Console.Clear();
                Console.WriteLine(message + "\n"); // prompt message
                for (int i = 0; i < options.Count; i++) // print out options numbered 1-n
                    Console.WriteLine($"{i + 1}. {options[i]}");

                if (int.TryParse(Console.ReadLine(), out res) && res > 0 && res < options.Count + 1)
                {
                    if (backOption) // if back was selected, res will be -1, otherwise return selected option (index is 1 lower than the selected option's number)
                        res = res == options.Count ? -1 : --res;
                    else --res;
                    loop = false;
                }
                else // catch invalid input
                {
                    Console.WriteLine("Invalid input, type a corresponding number...");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Chooses a random location from a list of available, premade locations
        /// </summary>
        /// <returns></returns>
        public static Location GetNewLocation()
        {
            Random rng = new Random();
            return new Location(Templates.locations[rng.Next(0, Templates.locations.Length)]);

        }


        /// <summary>
        /// A generic function that returns List of desired item types out of a Item List (typically player's inventory) 
        /// </summary>
        /// <typeparam name="itemType">Desired type of the items</typeparam>
        /// <param name="items">Input list of items, the desired itemType instances will be selected from this list</param>
        /// <returns>List of the desired itemType containing references to all the items of this type in the original List parameter</returns>
        public static List<itemType> GetItemsOfType<itemType>(List<Item> items) where itemType : Item
        {
            List<itemType> pickedItems = new List<itemType>();
            foreach (var item in items)
            {
                if (item is itemType itemOfType) // add corresponding items to the resulting list
                    pickedItems.Add(itemOfType);
            }
            return pickedItems;
        }
        /// <summary>
        /// Generic function that gets all the .ToString() results of given items
        /// </summary>
        /// <typeparam name="itemType">desired item type, is implicit</typeparam>
        /// <param name="items">List of items from which the Name property will be gathered</param>
        /// <returns>List of strings containing the converted items, used for displaying inventory or using the given items</returns>
        public static List<string> GetNames<itemType>(List<itemType> items) where itemType : Item
        {
            List<string> res = new List<string>();
            for (int i = 0; i < items.Count; i++) // cycle through the list
                res.Add(items[i].ToString()); // call .ToString() functions and save the result
            return res;
        }
        /// <summary>
        /// Gets save file names from the provided paths, used for displaying to the player
        /// </summary>
        /// <param name="paths">List of paths to the desired save files<param>
        /// <returns>List of save file names at the end of the provided paths</returns>
        public static List<string> GetFileNames(List<string> paths)
        {
            List<string> res =  new List<string>();
            foreach (var filePath in paths) // cycle through paths
                res.Add(Path.GetFileName(filePath.Split('.')[0])); // remove suffix and save result
            return res;
        }

        /// <summary>
        /// Verifies the validity of a given file name so it doesn't contain invalid characters
        /// </summary>
        /// <param name="fileName">File name to be checked</param>
        /// <returns></returns>
        public static bool IsFileNameValid(string fileName)
        {
            if (fileName == null) return false;
            for (int i = 0; i < fileName.Length; i++)
                if (!Char.IsLetter((char)fileName[i])) return false;
            return true;
        }

    }
}
