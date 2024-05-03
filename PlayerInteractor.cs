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

                if (int.TryParse(Console.ReadLine(), out res) && res > 0 && res <= options.Length)
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

        //public static void AssesResponse(int res, params Action[] funcs) => funcs[res].Invoke();
        public static Dictionary<int, Action> responseDict;

        #region MainMenu
        public static void MainMenu()
        {
            responseDict = new Dictionary<int, Action>
            {
                {0, () => NewGame()},
                {1, () => LoadGame()},
                {2, () => Help()},
                {3, () => Exit()},
            };
            Conversation(TextSource.mmWelocome, TextSource.mmOptions, out int res);
            responseDict[res].Invoke();

            //AssesResponse(res, NewGame, LoadGame, Help, Exit);
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
                GameLoop();
                // set all the stuff
            }
            
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
        #endregion

        // Main "cycle" in which player chooses actions and the game responds, the loop is done recursivly as to avoid unneceseary conditions
        public static void GameLoop()
        {
            responseDict = new Dictionary<int, Action>
            {
                {0, () => ChangeLocation()},
                {1, () => SearchLocation(Game.location.SearchChances)},
                {2, () => ShowInventory()},
            };

            Conversation($"{TextSource.mainLoopText} {Game.location.Name}", TextSource.mainLoopOptions, out int res);
            responseDict[res].Invoke();
            //AssesResponse(res, ChangeLocation, SearchLocation, ShowInventory);
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
        

        public static void SearchLocation(int chances)
        {
            Random rng = new Random();
            if (Game.location.Searches > 0)
            {
                Game.location.Searches--;
                if (rng.Next(0, chances + 1) == 1)
                    Fight();
                else GrantItem();       
            }
            else
            {
                Console.WriteLine(TextSource.searchNothingLeft);
                Console.ReadLine();
            }
        }

        public static void GrantItem()
        {
            Random rng = new Random();
            //Console.WriteLine("Found ITEM!");
            // Type type = .GetType();
            int itemNum = rng.Next(0, Game.location.ItemPool.Length);
            Item foundItem = Game.location.ItemPool[itemNum];
            switch (foundItem)
            {
                case Weapon:
                    foundItem = new Weapon((Weapon)Game.location.ItemPool[itemNum]);
                    break;
                case Consumable:
                    foundItem = new Consumable((Consumable)Game.location.ItemPool[itemNum]);
                    break;
                case CraftItem:
                    foundItem = new CraftItem((CraftItem)Game.location.ItemPool[itemNum]);
                    break;
            }
            

            Conversation(TextSource.itemFoundText + " " + foundItem.Name, TextSource.itemFoundOptions, out int res);
            if (res == 0)
                Game.player.Items.Add(foundItem);
            else
            {
                Console.WriteLine(TextSource.itemDiscardedText + " " + foundItem.Name);
                Console.ReadLine();
            }

            
        }

        public static void Fight()
        {
            Random rng = new Random();
            Enemy enemy = new Enemy (Game.location.EnemyPool[rng.Next(0, Game.location.EnemyPool.Length)]); // Choose the enemy
            bool escape = false;
            int res;
            do
            {
                bool playerTurn = true;
                do
                {
                    // choose action
                    Conversation($"{TextSource.enemyEncounterText} {enemy.Name} - {enemy.Health} HP", TextSource.enemyEncounterOptions, out res); 
                    switch (res)
                    {
                        case 0: // ATTACK
                            List<Weapon> weapons = GetItemsOfType<Weapon>(Game.player.Items);
                            string[] weaponsNames = GetNames(weapons);

                            Conversation(TextSource.enemyFightText, weaponsNames, out res); // Choose weapon
                            enemy.Health -= weapons[res].Damage;
                            weapons[res].Uses--;

                            Console.WriteLine($"You've attacked the {enemy.Name} with {weapons[res].Name} for {weapons[res].Damage} HP");
                            if (weapons[res].Uses == 0) Game.player.Items.Remove(weapons[res]);
                            if (enemy.Health > 0) Console.WriteLine($"{enemy.Name} health is now {enemy.Health}");
                            else Console.WriteLine($"You've defeated the {enemy.Name}");
                            playerTurn = false;
                            Console.ReadLine();
                            break;
                        case 1: // USE CONSUMABLE
                            if (Game.player.Health < 100)
                            {
                                List<Consumable> consumables = GetItemsOfType<Consumable>(Game.player.Items);                                
                                string[] consumableNames = GetNames(consumables);
                                Conversation(TextSource.chooseConsumableText, consumableNames, out res);
                                consumables[res].Uses--;
                                Game.player.Health = Game.player.Health + consumables[res].HealthRestore > 100 ? 100 : Game.player.Health + consumables[res].HealthRestore;
                                Console.WriteLine($"You've healed yourself for {consumables[res].HealthRestore} health");
                                Console.WriteLine($"Your current health is {Game.player.Health} HP");
                                if (consumables[res].Uses == 0) Game.player.Items.Remove(consumables[res]);
                            }
                            else Console.WriteLine(TextSource.consumableHealthFull);
                            Console.ReadLine();
                            break;
                        case 2: // RUN
                            if (rng.Next(0, 5) == 1)
                            {
                                escape = true;
                                playerTurn = false;
                                Console.WriteLine(TextSource.runSuccessfulText);
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine(TextSource.runUnsuccessfulText);
                                playerTurn = false;
                                Console.ReadLine();
                            }
                            break;
                    }
                } while (playerTurn);
                if (escape == false && enemy.Health > 0)
                {
                    Console.WriteLine($"{enemy.Name} attacked you for {enemy.Damage}HP!");
                    if (Game.player.Health - enemy.Damage > 0)
                    {
                        Game.player.Health -= enemy.Damage;
                        Console.WriteLine($"You're now at {Game.player.Health}HP");
                    }
                        
                    else
                    {
                        Game.player.Health = 0;
                        GameOver();
                    }
                    Console.ReadLine();
                }
            } while (!escape && enemy.Health > 0);

        }


        public static void ShowInventory()
        {
            Console.WriteLine("*inventory dialogue*");
            Console.ReadLine();
        }

        public static void GameOver()
        {
            Console.WriteLine("Bruh you died...");
            Console.ReadLine();
            Exit();
        }

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

        public static string[] GetNames<itemType>(List<itemType> items) where itemType : Item
        {
            string[] res = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
                res[i] = items[i].ToString();
            return res;
        }




    }
}
