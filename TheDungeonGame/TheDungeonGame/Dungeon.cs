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
        public static Player currentPlayer = new Player();
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
        static Player NewStart(int i)
        {
            Console.Clear();
            Player p = new Player();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
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
 
            string playerMenu = @"
            
                --------------------------------
                |  Choose whom you want to be: |
                |  K) KNIGHT                   |
                |  R) RANGER                   |
                |  S) SOLDIER                  |
                |  W) WARRIOR                  |
                --------------------------------";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter your name:");

            string savedName = Console.ReadLine();

            Console.WriteLine("Choose your player:");
            Console.WriteLine(playerMenu);
            bool flag = false;

            while (flag == false)
            {
                flag = true;
                string characterChoice = Console.ReadLine();
                if (characterChoice == "K")
                    p.currentClass = Player.PlayerClass.Knight;
                else if (characterChoice == "R")
                    p.currentClass = Player.PlayerClass.Ranger;
                else if (characterChoice == "S")
                    p.currentClass = Player.PlayerClass.Solider;
                else if (characterChoice == "W")
                    p.currentClass = Player.PlayerClass.Warrior;
                else
                {
                    Console.WriteLine("Please enter K, R, S or W.");
                    flag = false;
                }
            }
            p.name = Console.ReadLine();
            p.playerID = i;
            Console.Clear();
            return p;
        }

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }

        public static void Save()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            string path = "saves/" + currentPlayer.playerID.ToString();
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, currentPlayer);
            file.Close();
        }

        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] paths = Directory.GetFiles("saves");
            List<Player> players = new List<Player>();
            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
                players.Add(player);
            }

            idCount = players.Count;

            while (true)
            {
                Console.Clear();
                Print("Please enter player name or ID (id:# or Name). To create a new player, enter 'create'. ", 60); //the number is how fast it "types"

                foreach (Player p in players)
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
                            foreach (Player player in players)
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
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;
                    }
                    else
                    {
                        foreach (Player player in players)
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
        public static void Print(string text, int speed = 40)
        {

            //you could create a sound SoundPlayer soundPlayer = new SoundPlayer("sounds/type.wav");
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
