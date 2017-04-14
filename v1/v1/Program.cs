using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v1
{
    public static class Utility
    {
        public static void Dialog(string message, string color)
        {
            switch (color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    break;
            }

            Console.Write(message + "\n");
            Console.ResetColor();
        }

        public static void Dialog(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message + "\n");
            Console.ResetColor();
        }
    }

    public class Map
    {
        
        
    }

    public class Location
    {
        public string name { get; set; }
        public string desc { get; set; }
        //public List<Item>[] loc_items { get; set; }
        //public bool[] loc_accItems { get; set; }
        public bool loc_active { get; set; }

        public static void Description()
        {
            Utility.Dialog("Description of the area specified");
        }

        public static void AddLocation()
        {

        }
    }

    public class Item
    {
        public string item { get; set; }
        public string item_desc { get; set; }
        //public string item_help { get; set; }
        //public int quantity { get; set; }

        public static void AddItem()
        {

        }
    }

    public class Character
    {
        public List<Item> playerItems = new List<Item>();

        public string name { get; set; }
        public int hpMax { get; set; }
        public int hp { get; set; }
        public int energyMax { get; set; }
        public int energy { get; set; }
        public int attMin { get; set; }
        public int attMax { get; set; }
    }

    public class Enemy
    {
        public string name { get; set; }
        public int hpMax { get; set; }
        public int hp { get; set; }
        public int attMin { get; set; }
        public int attMax { get; set; }

        public static Enemy SpawnEnemy()
        {
            Random rand = new Random();
            Enemy enemy = new Enemy();

            enemy.name = "Clone #" + rand.Next(200);
            enemy.hpMax = rand.Next(10, 20);
            enemy.hp = enemy.hpMax;
            enemy.attMin = rand.Next(1, 5);
            enemy.attMax = enemy.attMin + 5;

            return enemy;
        }
    }

    public static class Game
    {
        public static bool run = true;
        public static bool map = false;
        public static Character player = new Character();

        public static void Start()
        /*
        * Write game title and give context for player to start
        */
        {
            Console.WriteLine(@"  ___     _                 _   ____            _           _   
 |_ _|___| | __ _ _ __   __| | |  _ \ _ __ ___ (_) ___  ___| |_ 
  | |/ __| |/ _` | '_ \ / _` | | |_) | '__/ _ \| |/ _ \/ __| __|
  | |\__ \ | (_| | | | | (_| | |  __/| | | (_) | |  __/ (__| |_ 
 |___|___/_|\__,_|_| |_|\__,_| |_|   |_|  \___// |\___|\___|\__|
                                             |__/               ");

            Utility.Dialog("What's your name?\n");  // ADD: 7 or 8 character limit
            player.name = Console.ReadLine();

            Utility.Dialog("Your name is " + player.name + ". Press any key to continue.\n");
            Console.ReadKey();

            Console.Clear();
            Utility.Dialog("You open your eyes and the blue sky slowly comes into focus. Rays of sun stream down from the trees.\n");

            Game.Status();

            Random rand = new Random();

            player.hpMax = 60;
            player.hp = player.hpMax;
            player.energyMax = 10;
            player.energy = player.energyMax;
            player.attMin = rand.Next(3, 8);
            player.attMax = player.attMin + 5;

            //test code start
            Console.WriteLine(player.attMin);
            Console.WriteLine(player.attMax);
            //test code end

            player.playerItems.Add(new Item() { item = "Key", item_desc = "A dull gray key. Like most keys, there's probably a lock that it can open.\n" });
            player.playerItems.Add(new Item() { item = "Stick", item_desc = "What's brown and sticky?\n" });
            player.playerItems.Add(new Item() { item = "Clothes", item_desc = "Worn, tattered, and kind of smelly.\n" });
            // playerItems.Add(new Item() { item = "Map", item_desc = "Yellowed and wrinkly, it has space to draw out where you've been and where you will go." });

            Game.Idle();
        }


        public static void Inspect(string query)
        {
            bool found = false;

            if (query == "AREA")
            {
                Location.Description();
            }
            else
            {
                foreach (Item item in player.playerItems)
                {
                    if (item.item.ToUpper() == query)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n" + item.item);
                        Console.ResetColor();
                        Console.Write(": ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(item.item_desc + "\n");
                        Console.ResetColor();
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
                Utility.Dialog("Could not find specified item.", "red");

            Game.Idle();
        }


        public static void Inventory()
        {
            Utility.Dialog("\nInventory: ");
            Utility.Dialog("=============================================================");

            if (player.playerItems.Count == 0)
                Utility.Dialog("You have nothing in your inventory!");
            else
            {
                foreach (Item item in player.playerItems)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(item.item + "\n");
                }
            }

            Console.ResetColor();
            Utility.Dialog("=============================================================\n");
            Game.Idle();
        }


        public static void Status()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n=============================================================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("    " + player.name);
            Console.ResetColor();
            Console.Write(" :: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(/*location.name*/"Temp Location");
            Console.ResetColor();
            Console.Write(" :: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("HP: " + player.hp + "/" + player.hpMax);
            Console.ResetColor(); Console.Write(" :: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Energy: " + player.energy + "/" + player.energyMax + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=============================================================\n");
            Console.ResetColor();
        }


        public static void Help()
        /*
         * Explains simple commands to player
         */
        {
            Utility.Dialog("Common commands: inventory, inspect, status, explore. Type \"exit\" to exit. \nIf you need to know what something does, type \"help: <command>.\n");
        }


        public static void Help(string context)
        {
            switch (context)
            {
                case "HELP":
                    Utility.Dialog("Okay, now you're just being stupid.\n");
                    break;
                case "MAP":
                    Utility.Dialog("The map shows your current location and places you've visited.\n");
                    break;
                case "INVENTORY":
                case "INV":
                    Utility.Dialog("Shows the items you currently have.\n");
                    break;
                case "INSPECT":
                case "INS":
                    Utility.Dialog("Use the command \"Inspect: <item>\" to inspect an item. Use \"Inspect: Area\" to inspect your surroundings.\n");
                    break;
                case "EXPLORE":
                    Utility.Dialog("You go out exploring.");
                    break;
            }
        }


        public static bool MultiWords(string input)
        {
            string[] split = input.Split(new Char[] { ' ' });

            if (split.Length == 1)
                return false;
            else
                return true;
        }


        public static string[] ParseInput(string input)
        {
            Regex regex = new Regex(": ");
            string[] split = regex.Split(input);

            return split;
        }


        public static void Sleep()
        {

        }


        public static void Combat()
        {
            Random rand = new Random();
            Enemy enemy = Enemy.SpawnEnemy();

            Utility.Dialog("You've encountered " + enemy.name + "! Press any key to start fight >>\n");
            Console.ReadKey();

            while (player.hp > 0 && enemy.hp > 0)
            {
                if (rand.NextDouble() <= 0.5)
                {
                    //player attacks
                    enemy.hp -= rand.Next(player.attMin, player.attMax);

                    Utility.Dialog(player.name + " attacks!"); //for ___ damage
                    Utility.Dialog(enemy.name + " HP: " + enemy.hp + "/" + enemy.hpMax);
                }
                else
                {
                    //enemy attacks
                    player.hp -= rand.Next(enemy.attMin, enemy.attMax);

                    Utility.Dialog(enemy.name + " attacks!"); //for __ damage
                    Utility.Dialog(player.name + " HP: " + player.hp + "/" + player.hpMax);
                }
            }

            if (player.hp <= 0)
            {
                Utility.Dialog(enemy.name + " overpowered you and took all your stuff. Press any key to respawn. >>");
                // remove all of players items, run Idle()

                Console.ReadKey();
                Utility.Dialog("You're back at camp.\n");
                Game.Idle();
            }
            else if (enemy.hp <= 0)
            {
                Utility.Dialog("You defeated " + enemy.name + "!\n");
            }
        }


        public static void Trek(int hours)
        {
            Random gen = new Random();

            int ticks = hours * 4;
            int enemyCnt = 0;
            int itemCnt = 0;
            int locCnt = 0;

            double fightPerc = 0.50;
            double itemPerc = 0.20;
            double locPerc = 0.10;

            //need a loop that changes percentages based on character's items equipped

            for (int i = 0; i < ticks; i++)
            {
                //display the time, add in 15 min increments for every loop


                //determine what encounters have a chance of happening
                if (gen.NextDouble() <= fightPerc)
                {
                    Game.Combat();
                    enemyCnt++;
                }
                else if (gen.NextDouble() <= itemPerc)
                {
                    Utility.Dialog("add item >>\n");
                    Item.AddItem();
                    itemCnt++;
                }
                else if (gen.NextDouble() <= locPerc)
                {
                    Utility.Dialog("add location >>\n");
                    Location.AddLocation();
                    locCnt++;
                }
                else
                    Utility.Dialog("nothing... >>\n");

                Console.ReadKey();
                Console.WriteLine();
            }

            Utility.Dialog("Trek has ended, you're back at camp. >>\n");
            Utility.Dialog("Enemies defeated: " + enemyCnt);
            Utility.Dialog("Items found: " + itemCnt);
            Utility.Dialog("Locations discovered: " + locCnt);
            Game.Status();
        }


        public static void Explore()
        {
            Game.Status();
            Utility.Dialog("You head out into the Wild Unknown.\n");
            Game.ExploreIdle();
        }


        public static void ExploreIdle()
        {
            string input;
            int hoursInt;

            input = Console.ReadLine();
            input = input.ToUpper();

            if (!MultiWords(input))
            {
                switch (input)
                {
                    case "HELP":
                        Game.Help();
                        Utility.Dialog("Explore commands: sleep, trek, return\n");
                        break;

                    case "RETURN":
                        Utility.Dialog("You're back at camp.\n");
                        Game.Idle();
                        break;

                    case "MAP":
                        if (map)
                        {
                            Utility.Dialog("You look at the map. I haven't coded this far yet, rip\n");
                        }
                        else
                        {
                            Utility.Dialog("You don't have a map!\n");
                        }
                        break;

                    case "INVENTORY":
                    case "INV":
                        Game.Inventory();
                        break;

                    case "INSPECT":
                    case "INS":
                        Utility.Dialog("Use the command \"Inspect: <item>\" to inspect an item. Use \"Inspect: Area\" to inspect your surroundings.\n");
                        break;

                    case "STATUS":
                        Game.Status();
                        break;

                    case "EXPLORE":
                        Game.Explore();
                        break;

                    case "TREK":
                        Utility.Dialog("How many hours do you want to explore?");
                        string hoursString = Console.ReadLine();
                        Int32.TryParse(hoursString, out hoursInt);

                        Console.WriteLine(hoursInt);

                        if (hoursInt != 0)
                            Game.Trek(hoursInt);
                        else
                            Utility.Dialog("INVALID NUMBER", "red");
                        break;

                    default:
                        Utility.Dialog("Invalid Command\n", "red");
                        break;
                }
            }
            else
            {
                string[] inputArr = ParseInput(input);

                switch (inputArr[0])
                {
                    case "HELP":
                        Game.Help(inputArr[1]);
                        break;

                    case "INSPECT":
                    case "INS":
                        Game.Inspect(inputArr[1]);
                        break;

                    default:
                        Utility.Dialog("Invalid Command\n", "red");
                        break;
                }
            }
            
            Game.ExploreIdle();
        }


        public static void Idle()
        /*
        * Every action loops back to this method. This prevents the console window from closing automatically.
        * Reads input and determines if it is one or multiple words; keywords will run specific methods
        */
        {
            // Console.WriteLine();

            string input;
            input = Console.ReadLine();
            input = input.ToUpper();

            if (!MultiWords(input))
            {
                switch (input)
                {
                    case "HELP":
                        Game.Help();
                        break;

                    case "EXIT":
                        run = false;
                        Utility.Dialog("\nPress any key to exit the game.");
                        Console.ReadKey();
                        break;

                    case "MAP":
                        if (map)
                        {
                            Utility.Dialog("You look at the map. I haven't coded this far yet, rip\n");
                        }
                        else
                        {
                            Utility.Dialog("You don't have a map!\n");
                        }
                        break;

                    case "INVENTORY":
                    case "INV":
                        Game.Inventory();
                        break;

                    case "INSPECT":
                    case "INS":
                        Utility.Dialog("Use the command \"Inspect: <item>\" to inspect an item. Use \"Inspect: Area\" to inspect your surroundings.\n");
                        break;

                    case "STATUS":
                        Game.Status();
                        break;

                    case "EXPLORE":
                        Game.Explore();
                        break;

                    default:
                        Utility.Dialog("Invalid Command\n", "red");
                        break;
                }
            }
            else
            {
                /*
                 * Previously had used a space for a character delimiter (" "), but in the case that there are 
                 * items or non-keyword items that are more than one word, the expression delimiter (": ") is instead being used.
                 */

                //Regex regex = new Regex(": ");
                //string[] split = regex.Split(input);

                //string[] split = input.Split(new Char[] { ": " });
                //Console.WriteLine("\nParsed words: ");
                //foreach (string keyword in split)
                //    Console.Write(keyword + "\n");

                string[] inputArr = ParseInput(input);

                switch (inputArr[0])
                {
                    case "HELP":
                        Game.Help(inputArr[1]);
                        break;

                    case "INSPECT":
                    case "INS":
                        Game.Inspect(inputArr[1]);
                        break;

                    default:
                        Utility.Dialog("Invalid Command\n", "red");
                        break;
                }
            }

            if (run)
                Game.Idle();
            else
                return;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game.Start();
        }
    }
}
