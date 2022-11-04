using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Encounters
    {
        static Random rand = new Random();

        //Encounters
        public static void FirstEncounter()
        {
            Console.WriteLine("You slowly open the door and hear some noise. The room stinks of mold and rotten eggs.");
            Console.WriteLine("The monster turns towards you...");
            Console.ReadKey();
            Combat(false, "Human Rogue", 1, 4);
        }

        public static void BasicFightEncounter()
        {
            Console.Clear();
            Console.WriteLine("You turn the corner and there stands a monster...");
            Console.ReadKey();
            Combat(true, "", 0, 0);
        }

        public static void WizardEncounter()
        {
            Console.Clear();
            Console.WriteLine("The door slowly creaks open and you peer into a dark room. You see a tall shadow...");
            Combat(false, "Dark Wizard", 4, 2);
        }


        //you can make several encounters and add to random
        public static void RandomEncounter()
        {
            switch (rand.Next(0, 2))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    WizardEncounter();
                    break;
            }
        }

        //Tools
        public static void Combat(bool random, string name, int power, int health)
        {

            string dungeonMenu = @"
                --------------------------
                |  Choose your action:   |
                |  A) ATTACK             |
                |  B) BLOCK              |
                |  R) RUN AWAY           |
                |  C) COLLECT COINS      |
                |  P) PLAYER STATS       |
                |  M) MONSTER STATS      |
                |  E) END GAME           |
                --------------------------";

            string _name = "";
            int _power = 0;
            int _health = 0;

            if (random)
            {
                _name = GetName();
                _power = Program.currentPlayer.GetPower();
                _health = Program.currentPlayer.GetHealth();
            }
            else
            {
                _name = name;
                _power = power;
                _health = health;
            }
            while (_health > 0)
            {
                Console.Clear();
                Console.WriteLine("Monster name: " + _name);
                Console.WriteLine("Monster Power: " + _power + " Monster Health: " + _health);
                Console.WriteLine(dungeonMenu);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Health: " + Program.currentPlayer.health);
                string userChoice = Console.ReadLine();

                if (userChoice == "A")
                {
                    //Attack
                    Console.WriteLine(" You surge forth with your weapon in hand!");
                    int damage = _power - Program.currentPlayer.block;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 6) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Warrior) ? 2 : 0);
                    //Warrior gets 2 damage points, if not + 0
                    Console.WriteLine(" You lose " + damage + " health. You cause " + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    _health -= attack;
                }
                else if (userChoice == "B")
                {
                    //Block

                    Console.WriteLine(_name + " is about to strike but you block the hit.");
                    int damage = (_power / 6) - Program.currentPlayer.block;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;
                    Console.WriteLine(" You lose " + damage + " health. You cause " + attack + " damage!");
                    Program.currentPlayer.health -= damage;
                    _health -= attack;
                }
                else if (userChoice == "R")
                {
                    //Run
                    if (Program.currentPlayer.currentClass != Player.PlayerClass.Solider && rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine(" As you run away from " + _name + ", it knocks you in the head and you fall to the ground.");
                        int damage = _power - Program.currentPlayer.block;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine(" You lose " + damage + " health and unable to escape.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine(" You fight back! You get " + _name + " and you escape!");
                        Console.ReadKey();
                    }
                }
                else if (userChoice == "C")
                {
                    //Collect Coins
                    Console.WriteLine("You collect the coins!");
                    //int treasure =+ coins;
                    Console.ReadKey();
                }
                else if (userChoice == "P")
                {
                    //Player Info
                    Console.WriteLine(" You are " + _name + "Your health is: " + _health);
                    Console.ReadKey();
                }
                else if (userChoice == "M")
                {
                    //Player Info
                    Console.WriteLine(" You are " + _name + " ." + " Your health is: " + _health);
                    Console.ReadKey();
                }
                else if (userChoice == "E")
                {
                    //Exit
                    break;

                }
                Console.ReadKey();

                if (Program.currentPlayer.health <= 0)
                {
                    //Player dies code
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have been defeated by " + _name + "!");
                    Console.WriteLine("GAME OVER!!!");
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int _coins = Program.currentPlayer.GetCoins();
            int _xp = Program.currentPlayer.GetXP();
            Console.WriteLine("You have conquered " + _name + " and you have won " + _coins + " gold coins! You have gained " + _xp + "XP!");
            Program.currentPlayer.coins += _coins;
            Program.currentPlayer.xp += _xp;

            if (Program.currentPlayer.CanLevelUp())
                Program.currentPlayer.LevelUp();

            Console.ReadKey();
        }

        public static string GetName()
        {
            switch (rand.Next(0, 5))
            {
                case 0:
                    return "Vaporman";
                //break;
                case 1:
                    return "The Crying Blob";
                //break;
                case 2:
                    return "The Sneaky Alien";
                //break;
                case 3:
                    return "The Wondrous Freak";
                //break;
                case 4:
                    return "The Flaming Dragon";
                    //break;
            }
            return "Gobble Goblin";

        }
    }
}
