using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Rooms

    {
        static Random rand = new Random();

        //Encounters
        public static void FirstEncounter()
        {
            Console.WriteLine("You slowly open the door and hear some noise. The room stinks of mold and rotten eggs.");
            Console.WriteLine("The monster turns towards you...");
            Console.ReadKey();
            Combat(false, GetMonsterName(), 2, 7);//Combat(bool random, string name, int power, int health)
        }

        public static void Room2()
        {
            Console.Clear();
            Console.WriteLine("You turn the corner and there stands a monster...");
            Console.ReadKey();
            Combat(true, GetMonsterName(), 0, 0);//Combat(bool random, string name, int power, int health)
        }

        public static void Room3()
        {
            Console.Clear();
            Console.WriteLine("The door slowly creaks open and you peer into a dark room. You see a tall shadow...");
            Combat(true, GetMonsterName(), 6, 5);//Combat(bool random, string name, int power, int health)
        }

        public static void Room4()
        {
            Console.Clear();
            Console.WriteLine("It's pitch black. You can't see anything in front of you. The air is damp and the room smells of mold...");
            Combat(true, GetMonsterName(), 10, 3);//Combat(bool random, string name, int power, int health)
        }

        public static void Room5()
        {
            Console.Clear();
            Console.WriteLine("YOU REACH THE ROOM OF DOOM. ARE YOU READY???");
            Combat(true, GetMonsterName(), 6, 15);//Combat(bool random, string name, int power, int health)
        }

        public static void Room6()
        {
            Console.Clear();
            Console.WriteLine("The room is full of books and dust. Furniture unused. You hear a large BOOOM!...");
            Combat(true, GetMonsterName(), 3, 8);//Combat(bool random, string name, int power, int health)
        }

        //you can make several encounters and add to random
        public static void RandomEncounter()
        {
            switch (rand.Next(0, 5))
            {
                case 0:
                    Room2();
                    break;
                case 1:
                    Room3();
                    break;
                case 2:
                    Room4();
                    break;
                case 3:
                    Room5();
                    break;
                case 4:
                    Room6();
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
                |  P) PLAYER INFO        |
                |  M) MONSTER INFO       |
                |  E) END GAME           |
                --------------------------";
            string gameOver = @"   
                        @@@@@@@@@  @@@@@@@@  @@@@@@@@@@@  @@@@@@@@     @@@@@@@@  @@@  @@@  @@@@@@@@  @@@@@@@@  
                        !@@        @@!  @@@  @@! @@! @@!  @@!          @@!  @@@  @@!  @@@  @@!       @@!  @@@  
                        !@!        !@!  @!@  !@! !@! !@!  !@!          !@!  @!@  !@!  @!@  !@!       !@!  @!@  
                        !@! @!@!@  @!@!@!@!  @!! !!@ @!@  @!!!:!       @!@  !@!  @!@  !@!  @!!!:!    @!@!!@!   
                        !!! !!@!!  !!!@!!!!  !@!   ! !@!  !!!!!:       !@!  !!!  !@!  !!!  !!!!!:    !!@!@!    
                        :!!   !!:  !!:  !!!  !!:     !!:  !!:          !!:  !!!  :!:  !!:  !!:       !!: :!!   
                        :!:   !::  :!:  !:!  :!:     :!:  :!:          :!:  !:!   ::!!:!   :!:       :!:  !:!  
                         ::: ::::  ::   :::  :::     ::    :: ::::     ::::: ::    ::::     :: ::::  ::   ::: ";

            string _name = "";
            int _power = 0;
            int _health = 0;
            string _roomName = "";

            //assigns monster stats  - could name currentPlayer to something better
            if (random)
            {
                _name = GetMonsterName();
                _power = Dungeon.currentPlayer.GetPower();
                _health = Dungeon.currentPlayer.MaxLife();
                _roomName = RoomName();
            }
            else
            {
                _name = name;
                _power = power;
                _health = health;
                _roomName = RoomName();
            }

            //add a do while loop here
            while (_health > 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Monster name: " + _name);
                Console.WriteLine("Monster Power: " + _power + " Monster Health: " + _health);
                Console.WriteLine("Room: " + _roomName);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(dungeonMenu);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Health: " + Dungeon.currentPlayer.health);
                string userChoice = Console.ReadLine();

                if (userChoice == "A")
                {
                    //Attack
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" You surge forth with your weapon in hand!");
                    int damage = _power - Dungeon.currentPlayer.block;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Dungeon.currentPlayer.weaponValue) + rand.Next(1, 6) + ((Dungeon.currentPlayer.chosenCharacter == Character.PlayerCharacter.Ranger) ? 2 : 0);
                    //Ranger gets 2 damage points, if not + 0
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" You lose " + damage + " health. You cause " + attack + " damage!");
                    Dungeon.currentPlayer.health -= damage;
                    _health -= attack;
                }
                else if (userChoice == "B")
                {
                    //Block

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(_name + " is about to strike but you block the hit.");
                    int damage = (_power / 6) - Dungeon.currentPlayer.block;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Dungeon.currentPlayer.weaponValue) / 2;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" You lose " + damage + " health. You cause " + attack + " damage!");
                    Dungeon.currentPlayer.health -= damage;
                    _health -= attack;
                }
                else if (userChoice == "R")
                {
                    //Run
                    if (Dungeon.currentPlayer.chosenCharacter != Character.PlayerCharacter.Solider && rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine(" As you run away from " + _name + ", it knocks you in the head and you fall to the ground.");
                        int damage = _power - Dungeon.currentPlayer.block;
                        if (damage < 0)
                            damage = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" You lose " + damage + " health and unable to escape.");
                        Dungeon.currentPlayer.health -= damage;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" You fight back! You get " + _name + " and you escape!");
                        Console.ReadKey();
                    }
                }
               
                else if (userChoice == "P")
                {
                    //Player Info
                    Console.WriteLine(" Your stats are:");
                    Console.WriteLine("Current Health:" + Dungeon.currentPlayer.health);


                }
                else if (userChoice == "M")
                {
                    //Player Info
                    Console.WriteLine(" Monster name:  " + _name + " ." + " Health:  " + _health + "!");
                    
                }
                else if (userChoice == "E")
                {
                    //Exit
                    break;

                }
                //Console.ReadKey();

                if (Dungeon.currentPlayer.health <= 0)
                {
                    //Player dies code
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have been defeated by " + _name + "!");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(gameOver);
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int _coins = Dungeon.currentPlayer.GetCoins();
            int _xp = Dungeon.currentPlayer.GetXP();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have conquered " + _name + " and you have won " + _coins + " gold coins! You have gained " + _xp + "XP!");
            Dungeon.currentPlayer.coins += _coins;
            Dungeon.currentPlayer.xp += _xp;

            if (Dungeon.currentPlayer.CanLevelUp())
                Dungeon.currentPlayer.LevelUp();

            Console.ReadKey();
        }

        public static string GetMonsterName()
        {

            string[] monsterNames = { "Vaporman", "Crying Blob", "Smellstink Alien", "Wondrous Freak", "Flaming Dragon", "Gobble Goblin", "Rocko Crud", "Coach Potato" };
            string monsterName = monsterNames[rand.Next(0, monsterNames.Length)];
                return monsterName;
        }

        public static string RoomName()
        {

            string[] roomNames = { "THE LIBRARY", "TREASURE ROOM ", "BARRACKS", "THE KITCHEN", "COLD ROOM", "OBSERVATORY", "SUN ROOM", "PARLOR" };
            string roomName = roomNames[rand.Next(0, roomNames.Length)];
            return roomName;
        }

    }
}
