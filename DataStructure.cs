using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Text;

namespace TextAdventure.DataStructure
{
    /// <summary>
    /// This class provides the "content" of the game, where predfined items, enemies, locations and etc. are created.
    /// When a new instance is required, it is not passed by reference, rather it is sent to the constructor of the given class and a new identical instance is created.
    /// </summary>
    public static class Templates
    {

        #region Items
        public static Consumable Mushroom = new Consumable()
        {
            Name = "Mushroom",
            Description = "Smells weird and tastes even weirder, but it seems to have a positive effect on your health",
            HealthRestore = 15,
            Uses = 1
        };

        public static Consumable Berries = new Consumable()
        {
            Name = "Bush berries",
            Description = "These berries grow everywhere in this forest, but are they edible? time to find out...",
            HealthRestore = 5,
            Uses = 1
        };

        public static Consumable MRE = new Consumable()
        {
            Name = "Military MRE",
            Description = "Meal. Ready. to Eat. it doesn't look appetising, but it'll do",
            HealthRestore = 25,
            Uses = 1
        };

        public static Consumable Medkit = new Consumable()
        {
            Name = "First-aid kit",
            Description = "All the tools neceseary to treat minor wounds and take care of infection",
            HealthRestore = 30,
            Uses = 2
        };

        public static Consumable HandSanitizer = new Consumable()
        {
            Name = "Hand sanitizer",
            Description = "Kills 99.99% of all the germs",
            HealthRestore = 5,
            Uses = 15
        };

        public static CraftItem Stick = new CraftItem()
        {
            Name = "Stick",
            Description = "A wooden stick, suitable for making primitve weaponary like bows, spears and arrows",
        };

        public static Weapon Stone = new Weapon()
        {
            Name = "Stone",
            Description = "A rock, pebble, stone even, suitable for... like bashing your enemies' skull in",
            Damage = 5,
            Uses = 1
        };

        public static Weapon MilitaryKnife = new Weapon()
        {
            Name = "Military-grade knife",
            Description = "Useful for hand to hand combat, sharp as a knife... because it is",
            Damage = 35,
            Uses = 5
        };

        public static Weapon KitchenKnife = new Weapon()
        {
            Name = "Kitchen knife",
            Description = "A little bit dull, but still pointy enough to seriusly hurt something",
            Damage = 15,
            Uses = 3
        };

        public static Weapon Pistol = new Weapon()
        {
            Name = "9mm pistol",
            Description = "A 1911 Colt handgun with a full magazine",
            Damage = 40,
            Uses = 3
        };

        public static Weapon M16 = new Weapon()
        {
            Name = "M16 assault rifle",
            Description = "A fully loaded assault rifle just begging to be used... what could possibly go wrong",
            Damage = 60,
            Uses = 5
        };

        public static Weapon Grenade = new Weapon()
        {
            Name = "Hand grenade",
            Description = "A High explosive hand grenade, definitely single-use",
            Damage = 120,
            Uses = 1
        };

        
        #endregion

        #region Enemies
        public static Enemy Rat = new Enemy()
        {
            Name = "Infected rat",
            Health = 20,
            Damage = 5
        };

        public static Enemy Bear = new Enemy()
        {
            Name = "Wild bear",
            Health = 120,
            Damage = 25
        };

        public static Enemy Gnome = new Enemy()
        {
            Name = "Forest gnome",
            Health = 25,
            Damage = 1
        };

        public static Enemy Zombie = new Enemy()
        {
            Name = "Zombie",
            Health = 85,
            Damage = 15
        };
        public static Enemy ArmoredZombie = new Enemy()
        {
            Name = "Armored zombie",
            Health = 150,
            Damage = 8
        };

        #endregion

        #region Locations
        public static Location Forest = new Location()
        {
            Name = "Forest",
            Narrative = "A dark forest full of mistery opens before your",
            Searches = 10,
            SearchChances = 10,
            ItemPool = new Item[]{ Mushroom, Stick, Stone, Berries },
            EnemyPool = new Enemy[] { Gnome, Bear, Rat}
        };

        public static Location City = new Location()
        {
            Name = "Abandoned city",
            Narrative = "You see the edge of a once blooming city, now abandoned to it's fate",
            Searches = 6,
            SearchChances = 4,
            ItemPool = new Item[] { HandSanitizer, KitchenKnife, Medkit, Pistol, Mushroom, Stick, Stone },
            EnemyPool = new Enemy[] {Zombie, Rat }
        };

        public static Location Base = new Location()
        {
            Name = "Military base",
            Narrative = "You've stumbled upon a military base",
            Searches = 8,
            SearchChances = 3,
            ItemPool = new Item[] { MilitaryKnife, MRE, M16, Grenade, Stick, Stone, Mushroom },
            EnemyPool = new Enemy[] { ArmoredZombie, Zombie, Rat }
        };

        #endregion

        public static Location[] locations = { Forest, City, Base };
    }
    
    /// <summary>
    /// A class that holds all the neceseary data about the game, this object is (de)serialized when reading or writing a file
    /// </summary>
    public class GameData
    {
        public Player player;
        public Location location, nextLocation;
        //public GameData game;
    }


    /// <summary>
    /// Player class holding all the neceseary values, being his health and a list of items representing his inventory
    /// </summary>
    public class Player
    {

        public Player()
        {
            Health = 100;
            Items = new List<Item>();
        }
        public int Health { get; set; }
        public List<Item> Items { get; set; }

    }

    /// <summary>
    /// Item class from which all the other item classes are derived, inventory is kept in Item type list for scalebality and ease of access
    /// </summary>
    public class Item
    {
        public Item() { }
        public Item(Item clone)
        {
            this.Name = clone.Name;
            this.Description = clone.Description;
        }
        public string Name { get; set; }
        public string Description { get; set; }
       
    }

    public class Weapon : Item
    {
        public Weapon() { }
        public Weapon(Weapon clone)
        {
            this.Name = clone.Name;
            this.Description= clone.Description;
            this.Damage = clone.Damage;
            this.Uses = clone.Uses;
        }
        public int Damage { get; set; }
        public int Uses { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Damage} DMG - {Uses} uses left";
        }
    }

    public class Consumable : Item
    {
        public Consumable() { }
        public Consumable(Consumable clone)
        {
            this.Name = clone.Name;
            this.Description = clone.Description;
            this.HealthRestore = clone.HealthRestore;
            this.Uses = clone.Uses;
        }
        public int HealthRestore { get; set; }
        public int Uses { get; set; }

        public override string ToString()
        {
            return $"{Name} - restores {HealthRestore} HP - {Uses} uses left";
        }

    }

    public class CraftItem : Item
    {
        public CraftItem() { }
        public CraftItem(CraftItem clone) : base(clone) { }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    /// <summary>
    /// Simply for holding all the temporary values of an enemy for the duration of the fight
    /// </summary>
    public class Enemy
    {
        public Enemy() { }
        public Enemy(Enemy clone)
        {
            this.Name = clone.Name;
            this.Health = clone.Health;
            this.Damage = clone.Damage;
        }
        public string Name { get; set; }    
        public float Health { get; set; }
        public int Damage { get; set; }
    }
    /// <summary>
    /// Contains all the information about the location the player is currently in, most importantly the different items and enemies he can find in the location
    /// </summary>
    public class Location
    {
        public Location() { }
        public Location(Location clone)
        {
            this.Name = clone.Name;
            this.Narrative = clone.Narrative;
            this.Searches = clone.Searches;
            this.SearchChances = clone.SearchChances;
            this.ItemPool = clone.ItemPool;
            this.EnemyPool = clone.EnemyPool;
        }
        public string Name { get; set; }
        public string Narrative { get; set; }
        public int Searches { get; set; } // how many times can player search the place
        public int SearchChances
        {
            get { return _searchChances; }
            set
            {
                if (value <= 0) throw new ArgumentException("Value must be bigger than 0");
                else _searchChances = value;
            }
        }// the chance of player finding an enemy (1 == 100%, 2 == 50% ...)
        public Item[] ItemPool { get; set; }
        public Enemy[] EnemyPool { get; set; }

        private int _searchChances;

    }
}
