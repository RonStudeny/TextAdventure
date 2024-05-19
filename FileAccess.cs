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
        /// <param name="res">A number coresponding to the chosen option, starting from 0</param>
        public static void Conversation(string message, List<string> options, out int res, bool backOption, string backText = "Back")
        {
            bool loop = true;
            res = -1;
            if (backOption)
                options.Add(backText);
            
            while (loop)
            {
                Console.Clear();
                Console.WriteLine(message + "\n");
                for (int i = 0; i < options.Count; i++)
                    Console.WriteLine($"{i + 1}. {options[i]}");

                if (int.TryParse(Console.ReadLine(), out res) && res > 0 && res < options.Count + 1)
                {
                    if (backOption)
                        res = res == options.Count ? -1 : --res;
                    else --res;
                    loop = false;
                }
                else
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
            //throw new Exception("This function is obsolete, please use Templates class for location, enemy and item instances");
            Random rng = new Random();
            return new Location(Templates.locations[rng.Next(0, Templates.locations.Length)]);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="itemType"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static List<itemType> GetItemsOfType<itemType>(List<Item> items) where itemType : Item
        {
            List<itemType> pickedItems = new List<itemType>();
            foreach (var item in items)
            {
                if (item is itemType itemOfType)
                    pickedItems.Add(itemOfType);
            }
            return pickedItems;
        }

        public static List<string> GetNames<itemType>(List<itemType> items) where itemType : Item
        {
            List<string> res = new List<string>();
            for (int i = 0; i < items.Count; i++)
                res.Add(items[i].ToString());
            return res;
        }

        public static List<string> GetFilePaths()
        {
            return Directory.GetFiles(Path.Combine(TextSource.saveFileDir)).ToList<string>();
        }

        public static List<string> GetFileNames(List<string> paths)
        {
            List<string> res =  new List<string>();
            foreach (var filePath in paths)
                res.Add(Path.GetFileName(filePath));
            return res;
        }


    }
}
