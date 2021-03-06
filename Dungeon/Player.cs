using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    [Serializable]
    public class Player
    {
        

        public string name;
        public int id;
        public int coins = 3000;
        public int health = 10;
        public int damage = 1 ;
        public int armorValue = 0;
        public int potions = 5;
        public int weaponValue = 1;

        public int mods = 0;

        public int GetHealth()
        {
            int upper = (2 * mods + 5);
            int lower = (mods + 2);
            return Program.rand.Next(lower, upper);
        }

        public int GetPower()
        {
            int upper = (2 * mods + 3);
            int lower = (mods + 1);
            return Program.rand.Next(lower, upper);
        }
        public int GetCoins()
        {
            int upper = (15 * mods + 50);
            int lower = (10 * mods + 10);
            return Program.rand.Next(lower, upper);
        }
    }

}
