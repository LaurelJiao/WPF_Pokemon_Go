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
        List<Canvas> IPokemons { set; get; }
        List<Canvas> IPokemonStops { set; get; }
        Canvas IGym { set; get; }
        TextBlock IPokeBallNum { set; get; }
        TextBlock ILevel { set; get; }
        TextBlock ICurrentExp { set; get; }
        TextBlock IMaxExp { set; get; }

        //typing game
        TextBox IType { set; get; }
        TextBlock IBlock { set; get; }
    }
    public partial class MainWindow : Window, IView
    {
        private GUI_Presenter Presenter;
        private List<Canvas> PokemonStops = new List<Canvas>();
        private List<Canvas> Pokemons = new List<Canvas>();
        public MainWindow()
        {
            InitializeComponent();
            //Initialize with mainmap
            Grid_Initialize();
            Presenter = new GUI_Presenter(this);
            Presenter.Player_Get_Postion();
            UserControl_Init();
        }
        
        // Interfave Initialize
        public Canvas IPlayer {
            get { return Player; }
            set { Player = value; }
        }
        public List<Canvas> IPokemons
        {
            get { return Pokemons; }
            set { Pokemons = value; }
        }
        public List<Canvas> IPokemonStops
        {
            get { return PokemonStops; }
            set { PokemonStops = value; }
        }
        public Canvas IGym
        {
            get { return Gym; }
            set { Gym = value; }
        }
        public TextBlock IPokeBallNum
        {
            get { return Ball_Num; }
            set { Ball_Num = value; }
        }
        public TextBlock ILevel
        {
            get { return Level_Num; }
            set { Level_Num = value; }
        }
        public TextBlock ICurrentExp
        {
            get { return Exp_Current; }
            set { Exp_Current = value; }
        }
        public TextBlock IMaxExp
        {
            get { return Exp_Maximum; }
            set { Exp_Maximum = value; }
        }
        public TextBlock IBlock
        {
            get { return Type_Block; }
            set { Type_Block = value; }
        }
        public TextBox IType
        {
            get { return Typing_Area; }
            set { Typing_Area = value; }
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
        // Mouse Click on the Pokemon
        private void Click_On_Pokemon(object sender, MouseEventArgs e)
        {
            Canvas c = sender as Canvas;
            int i = Pokemons.IndexOf(c);
            Presenter.Capture(i, Switch_to_Capture);

        }
        public void Switch_to_Capture()
        {
            SwitchPage(MainMap, CatchPokemon);

        }
        // Mouse Click on the PokemonStops
        private void PokemonStop_MouseClick(object sender, MouseEventArgs e)
        {
            Canvas c = sender as Canvas;
            Presenter.Explore_PokemonStop(PokemonStops.IndexOf(c));
        }

        // Mouse Click on the Gym
        private void Gym_MouseClick(object sender, MouseEventArgs e)
        {
            Presenter.Enter_Gym(GUI_EnterGymGrid);
        }
        public void GUI_EnterGymGrid()
        {
            SwitchPage(MainMap, Gym_Battle);
        }

        //Initializing 
        private void UserControl_Init()
        {
            //Mainmap control
                //Movement
            this.KeyDown += MainWindow_KeyDown;
            Player_Head.MouseLeftButtonUp += Click_On_Player_Head;
            Pokemon_Ball.MouseLeftButtonUp += Click_On_Pokemon_Ball;
                //Pokemons
            Pokemons.Add(Pichu);
            Pokemons.Add(Pikachu);
            Pokemons.Add(Charmander);
            Pokemons.Add(Squirtle);
            Pokemons.Add(Wartortle);
            Pokemons.Add(Charmeleon);
            Presenter.Pokemon_Get_Position();
            foreach (Canvas c in Pokemons)
            { c.MouseLeftButtonUp += Click_On_Pokemon; }
            //PokemonStops
            PokemonStops.Add(Stop1); PokemonStops.Add(Stop2); PokemonStops.Add(Stop3);
            Presenter.Init_PokemonStops(PokemonStops.Count);
            foreach(Canvas c in PokemonStops)
            {
                c.MouseLeftButtonUp += PokemonStop_MouseClick;
            }
            //Personnal information
            Presenter.Init_PersonalInformation();
            Back_PersonalInformation.MouseLeftButtonUp += Click_On_BackButton;
            //Bag
            Presenter.Init_Bag();
            Back_Bag.MouseLeftButtonUp += Click_On_BackButton;
            //Pokemon information
            Back_PokemonInformation.MouseLeftButtonUp += Click_On_BackToBag;
            //Gym
            Presenter.Init_PokemonGym();
            Gym.MouseLeftButtonUp += Gym_MouseClick;
            Back_Gym.MouseLeftButtonUp += Click_On_BackButton;
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
