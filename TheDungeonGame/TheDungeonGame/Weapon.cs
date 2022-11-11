using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Weapon
    {
        Random rand = new Random();
        //fields
        public string weaponName;
        public int weaponMinDamage;
        public int weaponMaxDamage;
        public bool isTwoHanded;
        public int bonusHitChance;

        //properties
        //Ability to create a weapon object to be used by the character

        public string WeaponName
        {
            get { return weaponName; }
            set { weaponName = value; }
        }

        public int WeaponMinDamage
        {
            get { return weaponMinDamage; }
            set
            {
                if (value > 0 && value <= weaponMaxDamage)
                {
                    weaponMinDamage = value; //rolling for damage twice
                }
                else
                {
                    weaponMinDamage = 1;
                }
            }
        }
        public int WeaponMaxDamage
        {
            get { return weaponMaxDamage; }
            set { weaponMaxDamage = value; }
        }

        public bool IsTwoHanded
        {
            get { return isTwoHanded; }
            set { isTwoHanded = value; }
        }
        public int BonusHitChance
        {
            get { return bonusHitChance; }
            set { bonusHitChance = value; }
        }


        //Constructors

        public Weapon() { }

        public Weapon(string weaponName, int weaponMinDamage, int weaponMaxDamage, bool isTwoHanded, int bonusHitChance)
        {
            WeaponName = weaponName;
            WeaponMinDamage = weaponMinDamage;
            WeaponMaxDamage = weaponMaxDamage;
            IsTwoHanded = isTwoHanded;
            BonusHitChance = bonusHitChance;
        }

    }
}