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
        public GUI_Presenter(IView View)
        {
            this.View = View;
            Game_Model = new Game_Model();
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
                    Game_Model.Player.Bag.Add_PokemonBalls(Game_Model.Pokemon_Stops[i].Explore_Stop());
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
