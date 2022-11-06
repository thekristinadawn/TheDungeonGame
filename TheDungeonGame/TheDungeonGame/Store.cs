using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Store
    {

        static int blockUpdate;
        static int weaponUpdate;
        static int difUpdate;


        public static void LoadShop(Player p)
        {
            blockUpdate = p.block;
            weaponUpdate = p.weaponValue;
            difUpdate = p.modification;
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int blockPrice;
            int weaponPrice;
            int difficultyPrice;

            while (true)
            {
                blockPrice = 100 * p.modification;
                weaponPrice = 100 * (p.weaponValue + 1);
                difficultyPrice = 300 + 100 * p.modification;

                Console.WriteLine("--------SHOP-------");
                Console.WriteLine(" (W)eapon         $" + weaponPrice);
                Console.WriteLine(" (B)lock          $" + blockPrice);
                Console.WriteLine(" (D)ifficulty   $" + difficultyPrice);
                Console.WriteLine(" (E)xit            ");
                Console.WriteLine(" (Q)uit Game       ");
                Console.WriteLine("===================");
                //input
                string playerInput = Console.ReadLine();
                if (playerInput == "W")
                {
                    TryBuy("Weapon", weaponPrice, p);
                }
                else if (playerInput == "B")
                {
                    TryBuy("Block", blockPrice, p);
                }
                else if (playerInput == "D")
                {
                    TryBuy("Difficulty", difficultyPrice, p);
                }
                else if (playerInput == "E")
                {
                    break;
                }
                else if (playerInput == "Q")
                {
                    Dungeon.Quit();
                }
            }
        }

        static void TryBuy(string item, int cost, Player p)
        {
            if (p.coins >= cost)
            {
                if (item == "weapon")
                    p.weaponValue++;
                else if (item == "block")
                    p.block++;
                else if (item == "difficulty")
                    p.modification++;

                p.coins -= cost;
            }
            else
            {
                Console.WriteLine("Can you count money? You don't have enough coins!");
                Console.ReadKey();
            }
        }
    }
}
