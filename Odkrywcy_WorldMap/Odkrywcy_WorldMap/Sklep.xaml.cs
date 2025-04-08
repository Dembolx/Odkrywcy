using Odkrywcy_WorldMap.Klasy;

using System;

using System.Windows;

using System.Windows.Controls;

using System.Windows.Shapes;

namespace Odkrywcy_WorldMap

{

    public partial class Sklep : Page

    {

        private Frame _mainframe;

        private Canvas _canvas;

        private Path _path;

        private WorldMap _worldMap; // Dodaj referencję do WorldMap

        public Sklep(Frame mainwindow, Canvas canvas, Path path, WorldMap worldMap)

        {

            InitializeComponent();

            _mainframe = mainwindow;

            _canvas = canvas;

            _path = path;

            _worldMap = worldMap; // Przypisz referencję

        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)

        {

            // Przejście do strony WorldMap

            _mainframe.Navigate(_worldMap);

        }

        private void Kup_Zwierze(object sender, RoutedEventArgs e)

        {

            Button button = (Button)sender;

            string n_zwierze = button.Name;

            // Tworzenie nowego zwierzęcia

            Zwierze zw = new Zwierze(n_zwierze, _canvas, _path);

            // Przekazanie zwierzęcia do WorldMap

            _worldMap.DodajZwierze(zw); // Dodaj zwierzę do listy w WorldMap

        }

    }

}
