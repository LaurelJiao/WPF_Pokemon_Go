using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Go
{
    class Game_Model
    {
        static Boolean exist = false;
        public Player Player { get; private set; }
        public Game_Model()
        {
            if (!exist)
            {
                Player = new Player();
                exist = true;
            }
        }
    }
    class Player 
    {
        private Bag Bag;
        private double Position;
        private int Level;
        private int EXP;
        private int EXP_Max;
        public Player()
        {
            Bag = new Bag();
            Position = 0.0;
            Level = 1;
            EXP = 0;
            EXP_Max = 10;
        }
        public double GetPostion()
        {
            return Position;
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
        public string name
        {
            private set; get;
        }
        // need a field for the image
        private int Hp_Maximum;
        public int Hp
        {
            private set; get;
        }
        public int Cp
        {
            private set; get;
        }
        public int Damage 
        {
            private set; get;
        }
        
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
