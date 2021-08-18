using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dungeon
{
    public class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        public static Random rand = new Random();
        static void Main(string[] args)
        {
            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }

            currentPlayer = Load(out bool newP);
            if (newP)
                Encounters.FirstEncounter();
            while (mainLoop)
            {

                Encounters.RandomEncounters();
            }
        }

        static Player NewStart(int i)
        {
            Console.Clear();
            Player p = new Player();
            Console.WriteLine("  Grunk's Dungeon");
            Console.WriteLine("  Name? ");
            p.name = Console.ReadLine();
            p.id = i;
            Console.Clear();
            Console.WriteLine("  You awake in a cold, stone, dark room. You feel dazed and are having trouble remembering");
            Console.WriteLine("  anything about your past.");
            if (p.name == "")
                Console.WriteLine("  You can't even remember your nama...");
            else
                Console.WriteLine("  You know your name is " + p.name);
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("  You grope around in the darkness until you find a door handle. You feel some resistance as");
            Console.WriteLine("  you turn the handle, but the rusty lock breaks with little effort. You see the captor");
            Console.WriteLine("  standing with his back to you outside the door.");
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
            string path = $"saves/{currentPlayer.id.ToString()}.level";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            binForm.Serialize(file, currentPlayer);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            file.Close();
        
        }

        public static Player Load(out bool newP)
        {
            newP = false;
            string[] paths = Directory.GetFiles("saves");
            List<Player> players = new List<Player>();

            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                Player player = (Player)binForm.Deserialize(file);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                file.Close();
                players.Add(player);
            }

            idCount = players.Count;

            while (true)
            {
            Console.Clear();
               Print("Choose your player:", 60);

                foreach (Player p in players)
                {
                    Console.WriteLine($"{p.id} : {p.name}");
                }
                Print("Please input player name or id (id:# or playername) or type 'create' to make a new player");
                string[] data = Console.ReadLine().Split(':');

                try
                {

                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player player in players)
                            {
                                if (player.id == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("There is no player with that id");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Your id needs to be a number! Press any key to continue!");
                            Console.ReadKey();

                        }
                    }

                    else if(data[0] == "create")
                        {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;
                        
                        
                    }
                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == data[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("There is no player with that name");
                        Console.ReadKey();
                    }
                }
                catch (IndexOutOfRangeException)
                {

                    Console.WriteLine("Your id needs to be a number! Press any key to continue!");
                    Console.ReadKey();
                }
            }

        }
        public static void Print(string text, int speed = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
        
        }

    }
}
