﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.DataStructure;
using TextAdventure.Text;

namespace TextAdventure
{
    public static class Game
    {
        public static Player player;
        public static Location location, nextLocation;
        

        public static void Main()
        {
            PlayerInteractor.MainMenu();
            
        }

    }



}
