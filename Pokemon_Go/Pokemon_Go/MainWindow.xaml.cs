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
    public interface IView { }
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            InitializeComponent();
            //Initialize with mainmap
            MainMap.Visibility = Visibility.Visible;
            CatchPokemon.Visibility = Visibility.Collapsed;
            PersonalInformation.Visibility = Visibility.Collapsed;
            Bag.Visibility = Visibility.Collapsed;
            PokemonInformation.Visibility = Visibility.Collapsed;
        }
        private void SwitchPage(Grid Pre, Grid Next)
        {
            Pre.Visibility = Visibility.Collapsed;
            Next.Visibility = Visibility.Visible;
            Next.BringIntoView();
        }







        // below code just for test of page switch
        private void button_Click(object sender, RoutedEventArgs e)
        {
            SwitchPage(MainMap,CatchPokemon);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SwitchPage(CatchPokemon, MainMap);
        }
    }
}
