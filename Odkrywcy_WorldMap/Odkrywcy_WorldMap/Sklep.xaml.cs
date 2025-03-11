using Odkrywcy_WorldMap.Klasy;
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
    public partial class Sklep : Page
    {

        private Frame _mainframe;
        private Canvas _canvas;
        private Path _path;
        public Sklep(Frame mainwindow, Canvas canvas, Path path)
        {
            InitializeComponent();
            _mainframe = mainwindow;
            _canvas = canvas;
            _path = path;
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new WorldMap(_mainframe));
        }

        private void Kup_Zwierze(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string n_zwierze = button.Name;
            Zwierze zw = new Zwierze(n_zwierze, _canvas, _path);
        }
    }
}
