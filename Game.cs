using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Text;

namespace TextAdventure
{
    public static class Game
    {
        public static Player player;
        public static void Main()
        {
            PlayerInteractor.MainMenu();
            
        }

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
        public string Name { get; set; }
    }



}
