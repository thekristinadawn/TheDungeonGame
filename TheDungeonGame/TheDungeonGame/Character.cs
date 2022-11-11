using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    [Serializable]
    public class Character
    {

        Random rand = new Random();

        public string name;
        public int playerID;
        public int coins = 0;
        public int health = 20;
        public int hitChance;
        public int block;
        public int xp;
        public int weaponValue;
        public int playerLevel;
        public Weapon characterWeapon;

        //modification in the calculations
        public int modification = 2;

        //player character selection
        public enum PlayerCharacter { Knight, Solider, Warrior, Ranger };
        public PlayerCharacter chosenCharacter = PlayerCharacter.Warrior;


        //life, power, get coins
        public int MaxLife()
        {
            int upper = (2 * modification + 10);
            int lower = (modification + 2);
            return rand.Next(lower, upper);
        }

        public int GetPower()
        {
            int upper = (2 * modification + 3);
            int lower = (modification + 1);
            return rand.Next(lower, upper);
        }

        public int GetCoins()
        {
            int upper = (15 * modification + 50);
            int lower = (10 * modification + 10);
            return rand.Next(lower, upper);
        }

        public int GetXP()
        {
            int upper = (20 * modification + 50);
            int lower = (15 * modification + 10);
            return rand.Next(lower, upper);
        }

        public int GetLevelUpValue()
        {
            return 100 * playerLevel + 400;
        }

        public bool CanLevelUp()
        {
            if (xp >= GetLevelUpValue())
                return true;
            else
                return false;
        }

        public void LevelUp()
        {
            while (CanLevelUp())
            {
                xp -= GetLevelUpValue();
                playerLevel++;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Dungeon.Print("YOU ARE KILLING IT!!! YOU HAVE ADVANCED TO THE NEXT LEVEL.");
            Dungeon.Print("LEVEL: " + playerLevel);
        }



    }
}
