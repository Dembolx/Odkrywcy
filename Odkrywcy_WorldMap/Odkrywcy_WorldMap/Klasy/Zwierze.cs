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

        private Path kontynentPath; // Ścieżka reprezentująca kontynent

        private Random random;

        private DispatcherTimer timer;

        private double x, y;

        private double krok = 10; // Krok przesunięcia

        // Właściwości publiczne do zarządzania pozycją i UI

        public double X

        {

            get { return x; }

            private set { x = value; }

        }

        public double Y

        {

            get { return y; }

            private set { y = value; }

        }

        public Ellipse ZwierzeUI

        {

            get { return zwierzeUI; }

        }

        public Zwierze(string nazwa, Canvas mapa, Path kontynentPath)

        {

            MessageBox.Show($"Kupiono {nazwa}");

            this.nazwa = nazwa;

            this.mapa = mapa;

            this.kontynentPath = kontynentPath;

            this.random = new Random();

            DodajDoMapy();

            // Inicjalizacja timera

            /*timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1); // Interwał 1 sekunda

            timer.Tick += Timer_Tick; // Podpięcie metody do zdarzenia Tick

            timer.Start(); // Uruchomienie timera*/

        }

        private void DodajDoMapy()

        {

            // Ustalamy początkową pozycję zwierzęcia wewnątrz kontynentu

            Point pozycja = LosujPunktWewnatrzKontynentu();

            x = pozycja.X;

            y = pozycja.Y;

            zwierzeUI = new Ellipse

            {

                Width = 10,

                Height = 10,

                Fill = Brushes.Black

            };

            // Ustawiamy początkową pozycję zwierzęcia

            Canvas.SetLeft(zwierzeUI, x);

            Canvas.SetTop(zwierzeUI, y);

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

        // Metoda losująca punkt wewnątrz kontynentu

        private Point LosujPunktWewnatrzKontynentu()

        {

            Point punkt;

            do

            {

                // Losujemy punkt w obrębie całego Canvas

                double losowyX = random.Next(0, (int)mapa.ActualWidth);

                double losowyY = random.Next(0, (int)mapa.ActualHeight);

                punkt = new Point(losowyX, losowyY);

            }

            while (!CzyPunktWewnatrzKontynentu(punkt)); // Powtarzaj, aż znajdziesz punkt wewnątrz kontynentu

            return punkt;

        }

        // Metoda wywoływana co sekundę przez timer

        private void Timer_Tick(object sender, EventArgs e)

        {

            double noweX = x;

            double noweY = y;

            // Generujemy losowy kierunek (góra, dół, lewo, prawo)

            int kierunek = random.Next(0, 4); // 0 - prawo, 1 - lewo, 2 - góra, 3 - dół

            switch (kierunek)

            {

                case 0: // Przemieszczanie w prawo

                    noweX += krok;

                    break;

                case 1: // Przemieszczanie w lewo

                    noweX -= krok;

                    break;

                case 2: // Przemieszczanie w górę

                    noweY -= krok;

                    break;

                case 3: // Przemieszczanie w dół

                    noweY += krok;

                    break;

            }

            // Sprawdzamy, czy nowa pozycja znajduje się wewnątrz kontynentu

            if (CzyPunktWewnatrzKontynentu(new Point(noweX, noweY)))

            {

                x = noweX;

                y = noweY;

                // Aktualizujemy pozycję zwierzęcia na Canvas

                Canvas.SetLeft(zwierzeUI, x);

                Canvas.SetTop(zwierzeUI, y);

            }

        }

        // Metoda sprawdzająca, czy punkt znajduje się wewnątrz kontynentu

        private bool CzyPunktWewnatrzKontynentu(Point punkt)

        {

            // Pobieramy geometrię z Path

            Geometry geometria = kontynentPath.Data;

            // Sprawdzamy, czy punkt znajduje się wewnątrz geometrii

            return geometria.FillContains(punkt);

        }

    }

}
