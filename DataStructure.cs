﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Text;

namespace TextAdventure.DataStructure
{
    public static class Templates
    {

        #region Items
        public static Consumable Mushroom = new Consumable()
        {
            Name = "Mushroom",
            Description = "A mushroom you found in the forest, should have a positive impact on your health... surely",
            HealthRestore = 15,
            Uses = 1
        };

        public static CraftItem Stick = new CraftItem()
        {
            Name = "Stick",
            Description = "It's a wooden stick, suitable for making primitve weaponary like bows, spears and arrows",
        };

        public static Weapon Stone = new Weapon()
        {
            Name = "Stone",
            Description = "A rock you've found on the ground, suitable for... like bashing your enemies' skull in",
            Damage = 5,
            Uses = 10
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

        #endregion

        #region Locations
        public static Location Forest = new Location()
        {
            Name = "Forest",
            Narrative = TextSource.locationNarratives[0],
            Searches = 3,
            SearchChances = 5,
            ItemPool = new Item[]{ Mushroom, Stick, Stone },
            EnemyPool = new Enemy[] { Gnome, Bear, Rat}
        };

        public static Location City = new Location()
        {
            Name = "Abandoned city",
            Narrative = TextSource.locationNarratives[1],
            Searches = 6,
            SearchChances = 8,
            ItemPool = new Item[] { Mushroom, Stick, Stone },
            EnemyPool = new Enemy[] { Gnome, Bear, Rat }
        };

        public static Location Base = new Location()
        {
            Name = "Military base",
            Narrative = TextSource.locationNarratives[2],
            Searches = 5,
            SearchChances = 4,
            ItemPool = new Item[] { Mushroom, Stick, Stone },
            EnemyPool = new Enemy[] { Gnome, Bear, Rat }
        };

        #endregion

        public static Location[] locations = { Forest, City, Base };
    }

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
            return $"{Name} - {Damage}DMG - {Uses} uses left";
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
            return $"{Name} - restores {HealthRestore}HP - {Uses} uses left";
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
        public int Searches { get; set; }
        public int SearchChances { get; set; }
        public Item[] ItemPool { get; set; }
        public Enemy[] EnemyPool { get; set; }


        public static Location GetNewLocation()
        {
            //throw new Exception("This function is obsolete, please use Templates class for location, enemy and item instances");
            Random rng = new Random();
            return new Location(Templates.locations[rng.Next(0, Templates.locations.Length)]);
            
        }
    }
}
