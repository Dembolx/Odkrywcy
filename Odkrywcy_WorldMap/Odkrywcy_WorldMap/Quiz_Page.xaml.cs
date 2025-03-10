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

namespace Odkrywcy_WorldMap
{
    public partial class Quiz_Page : Page
    {
        private string nazwa;
        private string nazwaBezPolskich;

        private Frame _mainframe;
        public Quiz_Page(string nazwa, string nazwaBezPolskich, Frame mainframe)
        {
            InitializeComponent();
            this.nazwa = nazwa;
            this.nazwaBezPolskich = nazwaBezPolskich;
            Title = nazwa;
            tytul_quizu.Text = $"QUIZ - {nazwa}";
            _mainframe = mainframe;
        }

        private void Memory_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new Memory(_mainframe, nazwa, nazwaBezPolskich));
        }

        private void Milionerzy_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new Milionerzy(nazwa, nazwaBezPolskich, _mainframe));
        }

        private void Szybkosc_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new Szybkosc(nazwa, nazwaBezPolskich, _mainframe));
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero;
            BackgroundVideo.Play();
        }

        // Obsługa kliknięcia przycisku Wyjście
        private void Wyjscie_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie do strony WorldMap_Page
            NavigationService.Navigate(new WorldMap(_mainframe));
        }

    }
}
