using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pokemon_Go
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public interface IView {
        Canvas IPlayer { set; get; }
    }
    public partial class MainWindow : Window, IView
    {
        private GUI_Presenter Presenter;
        public MainWindow()
        {
            InitializeComponent();
            //Initialize with mainmap
            Grid_Initialize();
            UserControl_Init();
            Presenter = new GUI_Presenter(this);
            Presenter.Player_Get_Postion();
        }
        
        // Interfave Initialize
        public Canvas IPlayer {
        get { return Player; }
        set { Player = value; }
        }
       

        // Keyboard A/D control moving, only under mainmap useful
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainMap.Visibility == Visibility.Visible)
            {
                if (e.Key == Key.A) {
                    Presenter.Player_Move_Left(5.0, 0.0);
                    e.Handled = true;
                }
                else if (e.Key == Key.D) {
                    Presenter.Player_Move_Right(5.0, MainMap.Width - Player.Width);
                    e.Handled = true;
                }
            }
        }

        //Initializing 
        private void UserControl_Init()
        {
            //Mainmap control
            this.KeyDown += MainWindow_KeyDown;
            Player_Head.MouseLeftButtonUp += Click_On_Player_Head;
            Pokemon_Ball.MouseLeftButtonUp += Click_On_Pokemon_Ball;
            //Personnal information
            Back_PersonalInformation.MouseLeftButtonUp += Click_On_BackButton;
            //Bag
            Back_Bag.MouseLeftButtonUp += Click_On_BackButton;
            //Pokemon information
            Back_PokemonInformation.MouseLeftButtonUp += Click_On_BackToBag;
        }
        private void Grid_Initialize()
        {
            MainMap.Visibility = Visibility.Visible;
            CatchPokemon.Visibility = Visibility.Collapsed;
            PersonalInformation.Visibility = Visibility.Collapsed;
            Bag.Visibility = Visibility.Collapsed;
            PokemonInformation.Visibility = Visibility.Collapsed;
        }

        //Switch between different pages
        private void SwitchPage(Grid Pre, Grid Next)
        {
            Pre.Visibility = Visibility.Collapsed;
            Next.Visibility = Visibility.Visible;
            Next.BringIntoView();
        }
        private void Click_On_Player_Head(object sender, MouseEventArgs e)
        {
            SwitchPage(MainMap, PersonalInformation);
        }
        private void Click_On_Pokemon_Ball(object sender, MouseEventArgs e)
        {
            SwitchPage(MainMap, Bag);
        }
        private void Click_On_BackButton(object sender, MouseEventArgs e)
        {
            Grid_Initialize();
        }
        private void Click_On_BackToBag(object sender, MouseEventArgs e)
        {
            SwitchPage(PokemonInformation, Bag);
        }
        /*
        // below code just for test of page switch
        private void button_Click(object sender, RoutedEventArgs e)
        {
            SwitchPage(MainMap,CatchPokemon);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SwitchPage(CatchPokemon, MainMap);
        }
        */
    }
}
