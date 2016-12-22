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
        public Gym Gym { get; private set; }
        public List<Pokemon> Pokemons { get; private set; }
        public Game_Model()
        {
            if (!Exist)
            {
                Player = new Player();
                Exist = true;
                // Initialize the pokemonstops
                Pokemon_Stops = new List<Pokemon_Stop>();
                Pokemon_Stops.Add(new Pokemon_Stop(100.0));
                Pokemon_Stops.Add(new Pokemon_Stop(250.0));
                Pokemon_Stops.Add(new Pokemon_Stop(400.0));
                // Initialize the gym
                Gym = new Gym(150.0);
                //Initialize the pokemons
                Pokemons = new List<Pokemon>();
                Pokemon Pichu = new Pokemon("Pichu", 84);
                Pokemon Pikachu = new Pokemon("Pikachu", 448);
                Pokemon Charmander = new Pokemon("Charmander", 333);
                Pokemon Squirtle = new Pokemon("Squirtle", 374);
                Pokemon Wartortle = new Pokemon("Wartortle", 124);
                Pokemon Charmeleon = new Pokemon("Charmeleon", 23);
                Pokemons.Add(Pichu);
                Pokemons.Add(Pikachu);
                Pokemons.Add(Charmander);
                Pokemons.Add(Squirtle);
                Pokemons.Add(Wartortle);
                Pokemons.Add(Charmeleon);

            }
        }
    }
    class Pokemon_Stop
    {
        public double Position { get; private set; }
        private DispatcherTimer Timer;
        private int Count;
        public bool Explored { get; private set; }
        public Pokemon_Stop(double position)
        {
            Count = 10;
            Explored = false;
            Position = position;
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
        private Random rnd = new Random();
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
        public List<Pokemon> MyPokemons { get; private set; }
        public int Num_PokemonBalls { get; private set; }
        public Bag()
        {
            Num_PokemonBalls = 0;
            MyPokemons = new List<Pokemon>();
        }
        public void Add_PokemonBalls(int num)
        {
            Num_PokemonBalls += num;
        }
    }
    
    class Pokemon 
    {
        public double Position { get; private set; }
        public string name { get; private set; }
        public string type { get; private set; }
        // need a field for the image
        public int Hp_Maximum { get; private set; }
        public int Hp { get; private set; }
        public int Cp { get; private set; }
        public int Damage { get; private set; }
        public string skill;
        private Random rnd = new Random();
        public Pokemon(string type, double Position)
        {
            this.name = type;
            this.type = type;
            this.Position = Position;
            Hp = rnd.Next(80);
            Cp = rnd.Next(70);
        }
        public Pokemon(string type)
        {
            this.name = type;
            this.type = type;
            Hp = rnd.Next(80);
            Cp = rnd.Next(70);
        }
        public void Powerup()
        {
            int add = rnd.Next(40);
            if (Hp != Hp_Maximum)
            {
                Hp = Hp + add;
            }
            add = rnd.Next(70);
            Cp = Cp + add;
        }
        public void Envolved(string newtype, string newskill)
        {
            type = newtype;
            int newadd = rnd.Next(80);
            Hp = Hp + newadd;
            newadd = rnd.Next(90);
            Cp = Cp + newadd;
            skill = newskill;

        }
        public void Setname(string uname)
        {
            name = uname;
        }
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

    class Gym
    {
        public double Position { get; private set; }
        public Gym(double positon)
        {
            Position = positon;
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

    class Typing_Game
    {
        public Pokemon pokemon { get; private set; }
        private DispatcherTimer Timer;
        public int count {get; private set;}
        public Typing_Game(Pokemon pokemon)
        {
            if(Timer != null)
            {
                Timer = null;
                Timer = new DispatcherTimer();
                Timer.Interval = TimeSpan.FromSeconds(1);
                Timer.Tick += Tick;
                Timer.Start();
                return;
            }
            this.pokemon = pokemon;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Tick;
            count = 5;
            Timer.Start();
        }
        private void Tick(object sender,EventArgs e)
        {
            count--;
            if(count == 0)
            {
                count = 5;
                Timer.Stop();
                Fail_Catch();
            }
        }
        public void Success_Catch()
        {
            
        }
        public void Fail_Catch()
        {

        }
    }
}
