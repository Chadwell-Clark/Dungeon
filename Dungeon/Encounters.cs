using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class Encounters
    {
       static  Random rand = new Random();
        //Encounter Generic


        //Encounters
        public static void FirstEncounter()
        {
            Console.WriteLine("  You throw open the door, and grab a rusty metal sword, while charging toward your captor");
            Console.WriteLine("  He turns...");
            Console.ReadKey();
            Combat(false, "humanimal Rogue", 1, 4);
        }
        public static void BasicFightEncounter()
        {
            Console.Clear();
            Console.WriteLine("  You turn the corner and see a hulking beast...");
            Console.ReadKey();
            Combat(true, "", 0,0);
        }


        public static void RandomEncounters()
        {
            switch (rand.Next(0, 1))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    WizardEncounter();
                    break;
            }

        }

        public static void WizardEncounter()
        {
            Console.Clear();
            Console.WriteLine("  The door graks open as you peer into a darkened room. You see the silhouett of a");
            Console.WriteLine("  tall man with a shaggy long beard. In his hands he holds an aged tome");
            Console.ReadKey();
            Combat(false, "Zrelius the Dark", 4, 2);



        }
        //Encounter Tools
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;
            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else 
            {
                n = name;
                p = power;
                h = health;
            }
            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine($"  Foe: {n}");
                Console.WriteLine($"  Power: {p}  /  Health: {h}");
                Console.WriteLine("  ==============================");
                Console.WriteLine("  |   (A)ttack     (D)efend    |");
                Console.WriteLine("  |     (R)un      (H)eal      |");
                Console.WriteLine("  ==============================");
                Console.WriteLine($"  Potions: {Program.currentPlayer.potions}  Health: {Program.currentPlayer.health} Coins: {Program.currentPlayer.coins}");
                string input = Console.ReadLine();

                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    //Attack
                    Console.WriteLine($"  With haste and fury you surge forth with your sword swinging!  The {n} strikes you as you pass!" );
                    int damage = p - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(1, Program.currentPlayer.weaponValue) + rand.Next(1, 4);
                    Console.WriteLine($"  You lose {damage} health and deal {attack} damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }

                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    //Defend
                    Console.WriteLine($"  As the {n} prepares to strike, you ready your sword in a defensive stance");
                    int damage = (p/4) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(1, Program.currentPlayer.weaponValue)/2;
                    Console.WriteLine($"  You lose {damage} health and deal {attack} damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;


                }

                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    //Run
                    if (rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine($"  As you sprint away from the {n}, its strike catches you inthe back, sending you sprawling");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine($"  You lose {damage} health and are unable to escape");
                    }
                    else
                    {
                        Console.WriteLine($"  You use your John travolta moves and evade the {n} and escape!");
                        Console.ReadKey();
                        Shop.LoadShop(Program.currentPlayer);
                    }
                }

                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    //Heal
                    if (Program.currentPlayer.potions == 0)
                    {
                        Console.WriteLine("  As you desperately grasp for a potion in your bag, all you find are empty gum wrappers");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine($"  The {n} strikes with you a mighty blow and you lose {damage} health!");
                    }
                    else
                    {
                        Console.WriteLine("  You reach into your bag and pull out a glowing golden flask. You take a long drink.");
                        int potionV = 5;
                        Console.WriteLine($"  You gain {potionV} health");
                        Program.currentPlayer.health += potionV;
                        Program.currentPlayer.potions -= 1;
                        Console.WriteLine($"  As you were occupied, the {n} advanced and struck.");
                        int damage = (p/2) - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine($"  You lose {damage} health");
                    }
                    Console.ReadKey();
                }
                if (Program.currentPlayer.health <= 0)
                {
                    Console.WriteLine($"  As the {n} stands tall and comes down to strike. You have been slain by the mighty {n}");
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int c = Program.currentPlayer.GetCoins();
            Console.WriteLine($"  As you stand victorious over the {n}, its body dissolves into {c} gold coins");
            Program.currentPlayer.coins += c;
            Console.ReadKey();
        
        }

        public static string GetName()
        {
            switch (rand.Next(0, 4))
            {
                case 0:
                    return "Skeleton";
                case 1:
                    return "Zombie";
                case 2:
                    return "Human Cultist";
                case 3:
                    return "Grave Robber";

            }
            return "Human Rogue";
        }


    }
}
