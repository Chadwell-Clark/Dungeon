using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{

    
    public class Shop
    {
         static int armorMod;
       static int weaponMod;
        static int difMod;
        public static void LoadShop(Player p)
        {

            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionP;
            int armorP;
            int weaponP;
            int difP;

            while (true)
            {
                 potionP = 20 + 10 * p.mods;
                 armorP = 100 * (p.armorValue +1);
                weaponP = 100 * p.weaponValue;
                difP = 300 + 100 * p.mods;
                Console.Clear();
                Console.WriteLine("              Shop              ");
                Console.WriteLine("  ==============================");
                Console.WriteLine($"  |   (W)eapon :         $ {weaponP}");
                Console.WriteLine($"  |   (A)rmor :          $ {armorP}");
                Console.WriteLine($"  |   (P)otions :        $ {potionP}");
                Console.WriteLine($"  |   (D)ifficulty Mod : $ {difP}");
                Console.WriteLine("  ==============================");
                Console.WriteLine("            (E)xit ");
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine($"    {p.name}'s Stats        ");
                Console.WriteLine("  ==============================");
                Console.WriteLine($"  Current Health: {p.health}");
                Console.WriteLine($"  Weapon Strength: {p.weaponValue}");
                Console.WriteLine($"  Armor Protection: {p.armorValue}");
                Console.WriteLine($"  Potions: {p.potions}");
                Console.WriteLine($"  Difficulty Mods: {p.mods}");
                Console.WriteLine($"  Coins:  ${p.coins}          ");
                Console.WriteLine("  ==============================");
                //Wait for input
                string input = Console.ReadLine().ToLower();
                if (input == "p" || input == "potion")
                {

                    TryBuy("potion", potionP, p);
                }
                else if (input == "w" || input == "weapon")
                {
                    TryBuy("weapon", weaponP, p);
                }
                else if (input == "a" || input == "armor")
                {
                    TryBuy("armor", armorP, p);
                }
                else if (input == "d" || input == "difficulty")
                {
                    TryBuy("dif", difP, p);
                }
                else if (input == "e" || input == "exit")
                    break;
                
            }
        }
        static void TryBuy(string item, int cost, Player p)
        {
            if (p.coins >= cost)
            {
                if (item == "potion")
                    p.potions++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "armor")
                    p.armorValue++;
                else if (item == "dif")
                    p.mods++;

                p.coins -= cost;
            }
            else
            { 
            Console.WriteLine("  You need more coinage lad");
                Console.ReadKey();
            }
        }
    }
}
