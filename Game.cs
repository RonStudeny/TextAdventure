using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Text;

namespace TextAdventure
{
    public class Game
    {
        public PlayerInteractor interactor { get; set; }
        public static void Run()
        {

            PlayerInteractor.MainMenu();
        }

        public Game()
        {

        }

        public static ResponseDelegate MainMenu = () =>
        {
            Conversation(TextSource.welcome, TextSource.mainMenuOptions, out int res);
            AssesResponse(res, NewGame, LoadGame, Help, Exit);
        };

        public static ResponseDelegate NewGame = () =>
        {
            Console.WriteLine("*New Game Started*");
        };

        public static ResponseDelegate LoadGame = () =>
        {
            Console.WriteLine("*Load game menu*");
        };

        public static ResponseDelegate Help = () =>
        {
            Console.WriteLine("*Help Interaction");
        };

        public static ResponseDelegate Exit = () =>
        {
            Console.WriteLine("*Game exited*");
        };


    }
}
