using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pokemon_Go
{
    class GUI_Presenter
    {
        private readonly IView View;
        private Game_Model Game_Model;
        private Typing_Game Typing_Game;
        private Battle_Gym Battle_Gym;
        public delegate void Callback();
        public GUI_Presenter(IView View)
        {
            this.View = View;
            Game_Model = new Game_Model();
        }

        // Pokemons
        public void Pokemon_Get_Position()
        {
            for (int i = 0; i < View.IPokemons.Count; i++)
            {
                View.IPokemons[i].SetValue(Canvas.LeftProperty, Game_Model.Pokemons[i].Position);
            }
        }

        // Capture Pokemon
        public void Capture(int i, Callback callback)
        {
            if (Game_Model.Player.Position - Game_Model.Pokemons[i].Position <= 10.0 && Game_Model.Player.Position - Game_Model.Pokemons[i].Position >= -10.0)
            {
                Typing_Game = new Typing_Game(Game_Model.Pokemons[i]);
                callback();
                Init_Typing_Game(Typing_Game);
            }
            else
                MessageBox.Show("Too faraway!");
        }

        // Typing Game
        public void Init_Typing_Game(Typing_Game g)
        {
            View.IBlock.Text = g.pokemon.type.ToString();
            View.IType.Text = "";
            View.IType.Focusable = true;
            View.IType.Focus();
        }

        // PokemonStops
        public void Init_PokemonStops(int num)
        {
            for (int i = 0; i < num; i++)
            {
                this.View.IPokemonStops[i].SetValue(Canvas.LeftProperty, Game_Model.Pokemon_Stops[i].Position);
            }
        }
        public void Explore_PokemonStop(int i)
        {
            if (-10.0 <= Game_Model.Player.Position - Game_Model.Pokemon_Stops[i].Position && Game_Model.Player.Position - Game_Model.Pokemon_Stops[i].Position <= 10.0)
            {
                if (!Game_Model.Pokemon_Stops[i].Explored)
                {
                    Add_Ball(Game_Model.Pokemon_Stops[i].Explore_Stop());
                }
                else
                {
                    MessageBox.Show("Sorry, the stop has already been explored, please wait");
                }
            }
            else
            {
                MessageBox.Show("Sorry, the stop is to far away from you");
            }
        }

        //Gym
        public void Init_PokemonGym()
        {
            this.View.IGym.SetValue(Canvas.LeftProperty, Game_Model.Gym.Position);
        }
        public void Enter_Gym(Callback callback)
        {
            if (-10.0 <= Game_Model.Player.Position - Game_Model.Gym.Position && Game_Model.Player.Position - Game_Model.Gym.Position <= 10.0)
            {
                callback();
            }else
            {
                MessageBox.Show("You are far away from the gym");
            }
        }

        //Personal Information
        public void Init_PersonalInformation()
        {
            this.View.ILevel.Text = Game_Model.Player.Level.ToString();
            this.View.ICurrentExp.Text = Game_Model.Player.EXP.ToString();
            this.View.IMaxExp.Text = Game_Model.Player.EXP_Max.ToString();
        }
        public void Add_Exp()
        {
            ///////////////////////////////////
            Init_PersonalInformation();
        }

        //Bag
        public void Init_Bag()
        {
            this.View.IPokeBallNum.Text = Game_Model.Player.Bag.Num_PokemonBalls.ToString();
        }
        public void Add_Ball(int num)
        {
            Game_Model.Player.Bag.Add_PokemonBalls(num);
            Init_Bag();
        }

        // Moving control
        public void Player_Get_Postion()
        {
            View.IPlayer.SetValue(Canvas.LeftProperty, Game_Model.Player.Position);
        }
        public void Player_Move_Right(double x, double m)
        {
            Game_Model.Player.MoveRight(x, m);
            Player_Get_Postion();
        }
        public void Player_Move_Left(double x, double m)
        {
            Game_Model.Player.MoveLeft(x, m);
            Player_Get_Postion();
        }

        
    }
}
