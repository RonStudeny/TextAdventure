using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Text;

namespace TextAdventure.DataStructure
{
    public static class Templates
    {

    }

    public class Player
    {
        public Player()
        {
            Health = 100.0f;
            Items = new List<Item>();
        }
        public float Health { get; set; }
        public List<Item> Items { get; set; }

    }
    public class Item
    {
        public enum ItemTypes { Weapon, Consumable };
        public ItemTypes ItemType { get; set; }
        public string Name { get; set; }
        public int Uses { get; set; }

    }
    public class Enemy
    {
        public string Name { get; set; }    
        public float Health { get; set; }
        public float Damage { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Narrative { get; set; }
        public int Searches { get; set; }
        public int SearchChances { get; set; }
        public Item[] ItemPool { get; set; }
        public Enemy[] EnemyPool { get; set; }


        public static Location GetNewLocation()
        {
            Location res = new Location();
            Random rng = new Random();
            int index = rng.Next(0, TextSource.locations.Length);
            res.Name = TextSource.locations[index];
            res.Narrative = TextSource.locationNarratives[index];
            res.Searches = rng.Next(1, 5);
            res.SearchChances = rng.Next(2, 8);
            res.ItemPool = null; // implement
            res.EnemyPool = null; // implement

            return res;
        }
    }
}
