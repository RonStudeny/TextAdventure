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
    /// The point of this class is to provide functions that interact between the player and the game, while being easy to use and re-use from the developers perspective
    /// </summary>
    public class GameInteractor
    {
        // Dictionary used for handling player response, takes an int as a key and runs the desired function, corresponding to the player's choice
        public static Dictionary<int, Action> responseDict;

        #region Menu functions
        
        // First function ran from the entry point, provides a couple of main menu options
        public static void MainMenu()
        {
            responseDict = new Dictionary<int, Action>
            {
                {0, () => NewGame()},
                {1, () => LoadGame()},
                {2, () => Help()},
                {3, () => Exit()},
            };

            Helpers.Conversation(TextSource.mmWelocome, TextSource.mmOptions.ToList<string>(), out int res, false);
            responseDict[res].Invoke(); // call the correct func. based on the conversation outcome
            MainMenu(); // recursively call MainMenu if player cancels any of the actions
        }

        // sets up a new game without creating or overriding any existing files
        public static void NewGame()
        {
            Helpers.Conversation(TextSource.newGameText, TextSource.newGameOptions.ToList<string>(), out int res, true);
            if (res == 0) // player decides to proceed
            {
                // setup a new game
                Game.currentGame.player = new Player();
                Game.currentGame.location = Helpers.GetNewLocation();
                Game.currentGame.nextLocation = Helpers.GetNewLocation();
                // start the main game loop
                GameLoop();
            }
            
        }

        // creates a new .json file or writes to an already existing one based on file name, serializes all the neceseary class instances to save the player's progress
        public static void SaveGame()
        {
            Console.Clear();
            Console.WriteLine(TextSource.saveGameText);

            string fileName = Console.ReadLine().ToLower(); // get the file name from the player
            if (fileName != null && fileName != "cancel") // if the entered file name isn't empty or 'cancel' try to save it
            {
                if (FileAccess.SaveGameToFile(fileName, Game.currentGame, out Exception? e)) // try to save the file using a custom function
                    Console.WriteLine($"{TextSource.saveGameSuccess} as '{fileName}'"); // if successful, notify the player
                else Console.WriteLine($"{TextSource.saveGameFailure} reason: {e.Message}.."); // else print out the reason
            }
            else Console.WriteLine(TextSource.saveGameCancel);
            Console.ReadLine();
        }

        // loads .json file names from a specified dictionary and let's the player choose which game file to load
        public static void LoadGame()
        {
            List<string> files = Helpers.GetFilePaths(); // get paths to all the save files
            List<string> fileNames = Helpers.GetFileNames(files); // get names of the files
            Helpers.Conversation(TextSource.loadGameText, fileNames, out int res, true); // player selects the desired save file
            if (res >= 0)
            {
                if (FileAccess.LoadFromFile(files[res], out Game.currentGame, out Exception? e)) // try to load the selected file
                {
                    Console.WriteLine(TextSource.loadGameText); // loading text
                    GameLoop(); // start the main game loop with the retrieved data
                }
                else Console.WriteLine($"{TextSource.loadGameFailed} reason: {e.Message}.."); // if unsuccessful, notify the player with error message
                Console.ReadLine();
            }
            
        }
        // simple game rule overview
        public static void Help()
        {
            Helpers.Conversation(TextSource.helpText, TextSource.helpOptions.ToList<string>(), out int res, true);
        }

        // exit function, parameter is used to ask the player to confirm that they want to quit the game
        public static void Exit(bool showWarning = false) // exits the game
        {
            if (showWarning) // check if the player should be asked to confirm 
            {
               Helpers.Conversation(TextSource.exitText, TextSource.exitOptions.ToList<string>(), out int res, false);
               if (res == 0) // if confirem, exit the game
                {
                    Console.WriteLine("Game exited...");
                    Environment.Exit(0);
                }
            }
            else // or just exit the game
            {
                Console.WriteLine("Game exited...");
                Environment.Exit(0);
            }

        }
        #endregion

        // Main "cycle" in which player chooses actions and the game responds, the loop is done recursivly as to avoid unneceseary conditions and loops 
        public static void GameLoop()
        {
            responseDict = new Dictionary<int, Action>
            {
                {0, () => ChangeLocation()},
                {1, () => SearchLocation(Game.currentGame.location.SearchChances)},
                {2, () => ShowInventory()},
                {3, () => SaveGame() },
                {4, () => Exit(true)},
            };

            Helpers.Conversation($"{TextSource.mainLoopText} {Game.currentGame.location.Name}", TextSource.mainLoopOptions.ToList<string>(), out int res, false);
            responseDict[res].Invoke();
            //AssesResponse(res, ChangeLocation, SearchLocation, ShowInventory);
            GameLoop();

        }

        // creates new location instances
        public static void ChangeLocation()
        {
            List<string> options = new List<string> { $"{TextSource.changeLocationText} {Game.currentGame.nextLocation.Name }" };
            Helpers.Conversation(Game.currentGame.nextLocation.Narrative, options , out int res, true);
            if (res == 0)
            {
                Game.currentGame.location = Game.currentGame.nextLocation;
                Game.currentGame.nextLocation = Helpers.GetNewLocation();
            }
        }
        
        // the player searches the location, based on the chances parameter is awarded either an item or encounters an enemy (chances == 1 - 100 % enemy; 2 - 50% enemy...)
        public static void SearchLocation(int chances)
        {
            Random rng = new Random();
            if (Game.currentGame.location.Searches > 0)
            {
                Game.currentGame.location.Searches--;
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

        // if an item is found during the SearchLocation() function call, it is generated here based on the location's item pool and added (or discarded) to the players inventory
        public static void GrantItem()
        {
            Random rng = new Random();
            //Console.WriteLine("Found ITEM!");
            // Type type = .GetType();
            int itemNum = rng.Next(0, Game.currentGame.location.ItemPool.Length);
            Item foundItem = Game.currentGame.location.ItemPool[itemNum];
            switch (foundItem)
            {
                case Weapon:
                    foundItem = new Weapon((Weapon)Game.currentGame.location.ItemPool[itemNum]);
                    break;
                case Consumable:
                    foundItem = new Consumable((Consumable)Game.currentGame.location.ItemPool[itemNum]);
                    break;
                case CraftItem:
                    foundItem = new CraftItem((CraftItem)Game.currentGame.location.ItemPool[itemNum]);
                    break;
            }
            

            Helpers.Conversation(TextSource.itemFoundText + " " + foundItem.Name + "\n" + foundItem.Description, TextSource.itemFoundOptions.ToList<string>(), out int res, false);
            if (res == 0)
                Game.currentGame.player.Items.Add(foundItem);
            else
            {
                Console.WriteLine(TextSource.itemDiscardedText + " " + foundItem.Name);
                Console.ReadLine();
            }

            
        }

        // if an enemy is encountered during SearchLocation() function call, this function is called where an enemy is chosen based on the location
        // the player has the options to Fight, Heal or Run

        public static void Fight()
        {
            Random rng = new Random();
            Enemy enemy = new Enemy (Game.currentGame.location.EnemyPool[rng.Next(0, Game.currentGame.location.EnemyPool.Length)]); // Choose the enemy out of location enemy pool
            bool escape = false;
            int res;
            do
            {
                bool playerTurn = true;
                do
                {
                    // choose action
                    Helpers.Conversation($"{TextSource.enemyEncounterText} {enemy.Name} - {enemy.Health} HP", TextSource.enemyEncounterOptions.ToList<string>(), out res, false); 
                    switch (res)
                    {
                        case 0: // ATTACK
                            List<Weapon> weapons = Helpers.GetItemsOfType<Weapon>(Game.currentGame.player.Items); // get weapons from inventory
                            Helpers.Conversation(TextSource.enemyFightText, Helpers.GetNames(weapons), out res, true, "Cancel"); // Choose weapon
                            if (res == -1) break; // cancel attack without losing the players turn
                            // use the weapon and reduce its uses 
                            enemy.Health -= weapons[res].Damage;
                            weapons[res].Uses--; 
                            Console.WriteLine($"You've attacked the {enemy.Name} with {weapons[res].Name} for {weapons[res].Damage} HP");
                            if (weapons[res].Uses == 0) Game.currentGame.player.Items.Remove(weapons[res]); // remove the weapon from inventory if last use
                            //print out the attack result
                            if (enemy.Health > 0) Console.WriteLine($"{enemy.Name} health is now {enemy.Health}");
                            else Console.WriteLine($"You've defeated the {enemy.Name}");

                            playerTurn = false; //end player turn
                            Console.ReadLine();
                            break;
                        case 1: // USE CONSUMABLE
                            if (Game.currentGame.player.Health < 100) // allow to use only if less then 100 hp
                            {
                                List<Consumable> consumables = Helpers.GetItemsOfType<Consumable>(Game.currentGame.player.Items); // get consumables from inventory                                    
                                Helpers.Conversation(TextSource.chooseConsumableText, Helpers.GetNames(consumables), out res, true, "Cancel"); //choose consumable
                                if (res == -1) break; // cancel item use
                                // using the item
                                consumables[res].Uses--;
                                // restoring up to the 100 max hp
                                Game.currentGame.player.Health = Game.currentGame.player.Health + consumables[res].HealthRestore > 100 ? 100 : Game.currentGame.player.Health + consumables[res].HealthRestore;
                                // print out the result
                                Console.WriteLine($"You've healed yourself for {consumables[res].HealthRestore} health");
                                Console.WriteLine($"Your current health is {Game.currentGame.player.Health} HP");
                                if (consumables[res].Uses == 0) Game.currentGame.player.Items.Remove(consumables[res]); //remove consumable from inventory if used up
                            }
                            else Console.WriteLine(TextSource.consumableHealthFull);
                            Console.ReadLine();
                            break;
                        case 2: // RUN
                            if (rng.Next(0, 3) == 0) // successful escape 
                            {
                                escape = true;
                                playerTurn = false;
                                Console.WriteLine(TextSource.runSuccessfulText);
                                Console.ReadLine();
                            }
                            else // unsuccessful escape
                            {
                                Console.WriteLine(TextSource.runUnsuccessfulText);
                                playerTurn = false;
                                Console.ReadLine();
                            }
                            // in both cases end the players turn
                            break;
                    }
                } while (playerTurn);
                // after player turn, enemy attack, unless player escaped or defeated the enemy
                if (escape == false && enemy.Health > 0)
                {
                    Console.WriteLine($"{enemy.Name} attacked you for {enemy.Damage}HP!");
                    if (Game.currentGame.player.Health - enemy.Damage > 0) // check if damage isn't fatal
                    {
                        Game.currentGame.player.Health -= enemy.Damage; // player damage
                        Console.WriteLine($"You're now at {Game.currentGame.player.Health}HP");
                    }
                        
                    else // player death
                    {
                        Game.currentGame.player.Health = 0;
                        GameOver();
                    }
                    Console.ReadLine();
                }
            } while (!escape && enemy.Health > 0); // loop while player is alive, hasn't escaped and the enemy is alive

        }

        // Prints out the player's whole inventory divided by different item types
        public static void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("Weapons:");
            foreach (var item in Helpers.GetItemsOfType<Weapon>(Game.currentGame.player.Items))
                Console.WriteLine(item.ToString());
            Console.WriteLine();

            Console.WriteLine("Consumables:");
            foreach (var item in Helpers.GetItemsOfType<Consumable>(Game.currentGame.player.Items))
                Console.WriteLine(item.ToString());
            Console.WriteLine();

            Console.WriteLine("Crafting items:");
            foreach (var item in Helpers.GetItemsOfType<CraftItem>(Game.currentGame.player.Items))
                Console.WriteLine(item.ToString());
            Console.WriteLine();
            Console.ReadLine();
        }

        // this function is called if the player looses a fight (his Health reaches 0)
        public static void GameOver()
        {
            Console.WriteLine("Bruh you died...");
            Console.ReadLine();
            Exit();
        }







    }
}
