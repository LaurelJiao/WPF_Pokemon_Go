using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        
        // Moving control
        public void Player_Get_Postion()
        {
            View.IPlayer.SetValue(Canvas.LeftProperty, Game_Model.Player.GetPostion());
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
