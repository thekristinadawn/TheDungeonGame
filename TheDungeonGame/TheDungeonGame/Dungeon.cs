using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DungeonGame
{
    public class Dungeon
    {
        public static Character currentPlayer = new Character();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }


            currentPlayer = Load(out bool newP);
            if (newP)
                Rooms.FirstEncounter();

            while (mainLoop)
            {
                Rooms.RandomEncounter();
            }

        }

        //method 
        static Character NewStart(int i)
        {
            Console.Clear();

            //player new
            Character gamePlayer = new Character();
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            //intro
            string titleGame = @"
                
                ██▄     ▄      ▄     ▄▀  ▄███▄   ████▄    ▄       ████▄ ▄████      ██▄   ████▄ ████▄ █▀▄▀█ 
                █  █     █      █  ▄▀    █▀   ▀  █   █     █      █   █ █▀   ▀     █  █  █   █ █   █ █ █ █ 
                █   █ █   █ ██   █ █ ▀▄  ██▄▄    █   █ ██   █     █   █ █▀▀        █   █ █   █ █   █ █ ▄ █ 
                █  █  █   █ █ █  █ █   █ █▄   ▄▀ ▀████ █ █  █     ▀████ █          █  █  ▀████ ▀████ █   █ 
                ███▀  █▄ ▄█ █  █ █  ███  ▀███▀         █  █ █            █         ███▀                 █  
                ▀▀             
                ";


            string gameDescription = @"
            
                Welcome to the Dungeon of Doom. The dungeon is full of rooms and treasures.
                Any treasure that you may find, is yours to keep. The treasures come with great risk.
                Monsters plague the Dungeon and may be in any room. The monsters are fearless and
                will attack. Your journey begins now...";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(titleGame);
            Console.WriteLine(gameDescription);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter your name:");
            string savedName = Console.ReadLine();


            //player picks character
            string playerMenu = @"
            
                --------------------------------
                |  Choose whom you want to be: |
                |  K) KNIGHT                   |
                |  R) RANGER                   |
                |  S) SOLDIER                  |
                |  W) WARRIOR                  |
                --------------------------------";

            bool validPlayerChoice = false;

            do
            {

                Console.WriteLine("Choose your character:");
                Console.WriteLine(playerMenu);
                ConsoleKey characterChoice = Console.ReadKey(intercept: true).Key;

                switch (characterChoice)
                { 
                    //Knight
                    case ConsoleKey.K:
                        gamePlayer.chosenCharacter= Character.PlayerCharacter.Knight;
                        Weapon doubleSword = new Weapon
                        {
                            WeaponName = "Double Edged Sword",
                            WeaponMinDamage = 4,
                            WeaponMaxDamage = 8,
                            IsTwoHanded = true,
                            BonusHitChance = 3,
                        };
                        validPlayerChoice = true;
                        break;
                    //Ranger
                    case ConsoleKey.R:
                        gamePlayer.chosenCharacter = Character.PlayerCharacter.Ranger;
                        Weapon rifle = new Weapon
                        {
                            WeaponName = "Rifle",
                            WeaponMinDamage = 3,
                            WeaponMaxDamage = 8,
                            IsTwoHanded = true,
                            BonusHitChance = 3
                        };
                        validPlayerChoice = true;
                        break;
                    //Solider
                    case ConsoleKey.S:
                        gamePlayer.chosenCharacter = Character.PlayerCharacter.Solider;
                        Weapon pistol = new Weapon
                        {
                            WeaponName = "Pistol",
                            WeaponMinDamage = 5,
                            WeaponMaxDamage = 10,
                            IsTwoHanded = true,
                            BonusHitChance = 2
                        };
                        validPlayerChoice = true;
                        break;
                    //Warrior
                    case ConsoleKey.W:
                        gamePlayer.chosenCharacter = Character.PlayerCharacter.Warrior;
                        Weapon deathAxe = new Weapon
                        {
                            WeaponName = "Death Axe",
                            WeaponMinDamage = 7,
                            WeaponMaxDamage = 13,
                            IsTwoHanded = true,
                            BonusHitChance = 5
                        };
                      
                        validPlayerChoice = true;
                        break;
                    default:
                        Console.WriteLine("Please enter K, R, S or W.");
                        break;
                }
            } while (!validPlayerChoice);
                
            
            gamePlayer.name = Console.ReadLine();
            gamePlayer.playerID = i;
            Console.Clear();
            return gamePlayer;
        }



        //Quit & Save
        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }

        //Save
        public static void Save()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            string path = "saves/" + currentPlayer.playerID.ToString();
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, currentPlayer);
            file.Close();
        }

        //method that establishes new player or pull previous player
        public static Character Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] paths = Directory.GetFiles("saves");
            List<Character> players = new List<Character>();
            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                Character player = (Character)binForm.Deserialize(file);
                file.Close();
                players.Add(player);
            }

            idCount = players.Count;

            while (true)
            {
                Console.Clear();
                Print("Please enter player name or ID (id:# or Name). To create a new player, enter 'create'. ", 20); //the number is how fast it "types"

                foreach (Character p in players)
                {
                    Console.WriteLine(p.playerID + " : " + p.name + " : Block Value:" + p.block + " : Weapon Value:" + p.weaponValue);
                }


                string[] enterInput = Console.ReadLine().Split(':');

                try
                {
                    if (enterInput[0] == "id")
                    {
                        if (int.TryParse(enterInput[1], out int id))
                        {
                            foreach (Character player in players)
                            {
                                if (player.playerID == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("There is no player with that ID. Press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Your ID needs to be a number! Press any key to continue.");
                            Console.ReadKey();
                        }
                    }
                    else if (enterInput[0] == "create")
                    {
                        Character newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;
                    }
                    else
                    {
                        foreach (Character player in players)
                        {
                            if (player.name == enterInput[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("There is no player with that ID. Press any key to continue.");
                        Console.ReadKey();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Your ID needs to be a number! Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        //fun method - words "type" and setting the speed
        public static void Print(string text, int speed = 60)
        {

            // could create a sound SoundPlayer soundPlayer = new SoundPlayer("sounds/type.wav");
            //soundPlayer.PlayLooping();

            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            //soundPlayer.Stop();
            Console.WriteLine();
        }
    }
}
