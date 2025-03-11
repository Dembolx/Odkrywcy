using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Odkrywcy_WorldMap.Klasy
{
    public class Zwierze
    {
        private string nazwa;
        private Ellipse zwierzeUI;
        private Canvas mapa;
        private Random random;
        private DispatcherTimer timer;
        private double x, y;
        private double krok = 10;

        public Zwierze(string nazwa, Canvas mapa, Path path)
        {
            MessageBox.Show($"Kupiono {nazwa}");
            this.nazwa = nazwa;
            this.mapa = mapa;
            this.random = new Random();
            DodajDoMapy();
        }

        private void DodajDoMapy()
        {
            // Ustalamy losową pozycję początkową zwierzęcia na Canvas
            x = random.Next(300, 1000); // 100 to szerokość zwierzęcia
            y = random.Next(200, 500); // 100 to wysokość zwierzęcia

            zwierzeUI = new Ellipse
            {
                Width = 300,
                Height = 300,
                Fill = Brushes.Black
            };

            // Ustawiamy początkową pozycję zwierzęcia
            Canvas.SetLeft(zwierzeUI, 600);
            Canvas.SetTop(zwierzeUI, 250);

            // Ustawiamy najwyższy ZIndex, aby element był zawsze na wierzchu
            Canvas.SetZIndex(zwierzeUI, 1);

            // Dodajemy Ellipse do Canvas
            mapa.Children.Add(zwierzeUI);

            // Sprawdzamy, czy Ellipse został dodany do Canvas
            if (mapa.Children.Contains(zwierzeUI))
            {
                MessageBox.Show("Ellipse został dodany do Canvas.");
            }
            else
            {
                MessageBox.Show("Ellipse nie został dodany do Canvas.");
            }
        }

        /*private void DodajDoMapy()
        {
            // Ustalamy losową pozycję początkową zwierzęcia na Canvas
            x = random.Next(300, 1000); // 100 to szerokość zwierzęcia
            y = random.Next(200, 500); // 100 to wysokość zwierzęcia

            zwierzeUI = new Ellipse
            {
                Width = 300,
                Height = 300,
                Fill = Brushes.Black
            };

            // Ustawiamy początkową pozycję zwierzęcia
            Canvas.SetLeft(zwierzeUI, 600);
            Canvas.SetTop(zwierzeUI, 250);

            // Ustawiamy najwyższy ZIndex, aby element był zawsze na wierzchu
            Canvas.SetZIndex(zwierzeUI, 1);

            mapa.Children.Add(zwierzeUI);

            // Inicjalizujemy i uruchamiamy timer (jeśli chcesz, możesz odkomentować kod z timerem)
            *//*
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // Ustawiamy co sekundę
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            *//*
        }*/

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Generujemy losowy kierunek (góra, dół, lewo, prawo)
            int kierunek = random.Next(0, 4); // 0 - prawo, 1 - lewo, 2 - góra, 3 - dół

            switch (kierunek)
            {
                case 0: // Przemieszczanie w prawo
                    x += krok;
                    break;
                case 1: // Przemieszczanie w lewo
                    x -= krok;
                    break;
                case 2: // Przemieszczanie w górę
                    y -= krok;
                    break;
                case 3: // Przemieszczanie w dół
                    y += krok;
                    break;
            }

            // Zapobiegamy wychodzeniu poza granice
            x = Math.Max(0, Math.Min(x, mapa.ActualWidth - 100)); // 100 to szerokość zwierzęcia
            y = Math.Max(0, Math.Min(y, mapa.ActualHeight - 100)); // 100 to wysokość zwierzęcia

            // Aktualizujemy pozycję zwierzęcia
            Canvas.SetLeft(zwierzeUI, x);
            Canvas.SetTop(zwierzeUI, y);
        }
    }
}
