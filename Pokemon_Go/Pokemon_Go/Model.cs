using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pokemon_Go
{
    class Game_Model
    {
        public static List<double> Exist_Position = new List<double>();
        static Boolean Exist = false;
        public Player Player { get; private set; }
        public List<Pokemon_Stop> Pokemon_Stops { get; private set; }
        public Game_Model()
        {
            if (!Exist)
            {
                Player = new Player();
                Exist = true;
                Pokemon_Stops = new List<Pokemon_Stop>();
                for(int i=0; i<3; i++)
                {
                    Pokemon_Stops.Add(new Pokemon_Stop());
                }
            }
        }
    }
    class Pokemon_Stop
    {
        public double Position { get; private set; }
        private DispatcherTimer Timer;
        private int Count;
        private Boolean Explored;
        private Random rnd = new Random();
        public Pokemon_Stop()
        {
            Count = 10;
            Explored = false;
            double temp = (double)rnd.Next(30,470);
            while (Game_Model.Exist_Position.Contains(temp)){
                temp = (double)rnd.Next(30,470);
            }
            Position = temp;
            Game_Model.Exist_Position.Add(Position);
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1.0);
            Timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Count--;
            if(Count == 0)
            {
                Explored = false;
                Timer.Stop();
            }
        }
        public int Explore_Stop()
        {
            int num = rnd.Next(1, 3);
            Count = 10;
            Timer.Start();
            Explored = true;
            return num;
        }
    }
    class Player 
    {
        public Bag Bag { get; private set; }
        public double Position { get; private set; }
        public int Level { get; private set; }
        public int EXP { get; private set; }
        public int EXP_Max { get; private set; }
        public Player()
        {
            Bag = new Bag();
            Position = 0.0;
            Level = 1;
            EXP = 0;
            EXP_Max = 10;
        }
        public void MoveRight(double x, double max)
        {
            if(Position + x <= max)
            {
                Position += x;
            }
        }
        public void MoveLeft(double x, double min)
        {
            if(Position - x >= min)
            {
                Position -= x;
            }
        }
    }
    class Bag 
    {
        private List<Pokemon> MyPokemons;
    }
    class Typing_Game 
    {

    }
    class Pokemon 
    {
        public string name { get; private set; }
        // need a field for the image
        public int Hp_Maximum { get; private set; }
        public int Hp { get; private set; }

        public int Cp { get; private set; }

        public int Damage { get; private set; }

        public Pokemon()
        {

        }

        public void Powerup() { }
        public void Envolved() { }
        public bool Deduct_Health(int deduct_num)
        {
            // Deduct the health, after the operation, return true is Hp <= 0
            Hp -= deduct_num;
            if (Hp <= 0)
            {
                Hp = 0;
                return true;
            }
            return false;
        }
    }

    class Battle_Gym 
    {
        public delegate void Callback(Pokemon winner, Pokemon loser);
        public Pokemon My_Battle_Pokemon { get; private set; }
        public Pokemon Enemy_Pokemon { get; private set; }
        public Battle_Gym(Pokemon me, Pokemon enemy)
        {
            My_Battle_Pokemon = me;
            Enemy_Pokemon = enemy;
        }
        public void Attack(Pokemon src,Pokemon target, Callback callback)
        {
            // transfer each attack into deduction of health, and callback to the presenter if one pokemon "die"
            if (target.Deduct_Health(src.Damage))
            {
                callback(src, target);
            }
        }
    }
}
