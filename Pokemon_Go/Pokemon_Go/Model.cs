using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Go
{
    public interface IModel { }

    class Game_Model : IModel
    {
        public Player Player { private set; get; }
        public Bag Bag { private set; get; }
        public List<Pokemon> PokemonsOnMap { private set; get; }
        public List<PokemonStop> PokemonStops { private set; get; }
        public Game_Model(int map_long, int map_width, int pokemon_num, int stop_num)
        {
            // Randomly generate some pokemons on the map
            PokemonsOnMap = new List<Pokemon>();
            PokemonStops = new List<PokemonStop>();
            Random rnd = new Random();

            if(Player == null)
            {
                Player = new Player();
            }
            if(Bag == null)
            {
                Bag = new Bag();
            }
        }
    }
    class PokemonStop
    {

    }
    class Player : IModel
    {
        public string name {private set; get;}
        public int Level { private set; get; }
        public int Exp_Max { private set;  get; }
        public int Exp { private set; get; }
        public int[,] Position { private set; get; }
        private Player(string name){
            this.name = name;
        }
        static private Player instance;
        static public Player Instance{
            get{
                if(instance == null)
                instance = new Player();
                return instance;
            }
        }
        public int[,] Move(int[,] Position){
            int[,] NewPosition;
            //...
            return NewPosition;
        }
    }
    class Bag : IModel
    {
        private List<Pokemon> MyPokemons;
        public int PokemonBall { private set; get; }
        public Bag()
        {
            if (MyPokemons == null)
            {
                MyPokemons = new List<Pokemon>();
            }
            else
            {
                MyPokemons.Clear();
            }
        }
    }
    class Typing_Game : IModel
    {
        public Typing_Game()
        {
            
        }
    }
    class Pokemon : IModel
    {
        public string Name {private set; get; }
        // need a field for the image
        private int Hp_Maximum;
        public int Hp { private set; get; }
        public int Cp { private set; get; }
        public int Damage { private set; get; }
        public int[,] Position { private set; get; }
        public Pokemon(string name)
        {
            this.Name = name;
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


    class Battle_Gym : IModel
    {
        public delegate void Callback(Pokemon winner, Pokemon loser);
        private Pokemon My_Battle_Pokemon;
        private Pokemon Enemy_Pokemon;
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
