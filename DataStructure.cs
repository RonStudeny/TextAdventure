using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.DataStructure
{
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
        public Item[] ItemPool { get; set; }
        public Enemy[] EnemyPool { get; set; }


    }
}
