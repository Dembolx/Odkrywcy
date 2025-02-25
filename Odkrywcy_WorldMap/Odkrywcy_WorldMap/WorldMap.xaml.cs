using Odkrywcy_WorldMap.Klasy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Odkrywcy_WorldMap
{
    public partial class WorldMap : Window
    {
        private List<string> kontynenty_nazwy = new List<string>()
        {
            "Ameryka Północna",
            "Ameryka Południowa",
            "Europa",
            "Azja",
            "Antarktyda",
            "Australia",
            "Afryka"
        };

        private List<string> kontynenty_nazwy_bez_polskiech_znakow = new List<string>()
        {
            "AmerykaPolnocna",
            "AmerykaPoludniowa",
            "Europa",
            "Azja",
            "Antarktyda",
            "Australia",
            "Afryka"
        };

        private Dictionary<string, Kontynent> kontynenty = new Dictionary<string, Kontynent>();

        private double zoomFactor = 4.0; // Jak bardzo przybliżyć
        private double duration = 5;   // Czas animacji w sekundach

        public WorldMap()
        {
            InitializeComponent();

            for (int i = 0; i < kontynenty_nazwy.Count; i++)
            {
                kontynenty.Add(kontynenty_nazwy_bez_polskiech_znakow[i], new Kontynent(kontynenty_nazwy[i]));
            }

        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Canvas canvas)
            {
                // Podświetlenie konturów dla wszystkich Path w Canvas
                foreach (var child in canvas.Children)
                {
                    if (child is Path path)
                    {
                        path.Stroke = Brushes.Orange;
                    }
                }

                // Pobranie transformacji (jeśli nie istnieje, dodajemy nową)
                ScaleTransform scaleTransform = canvas.RenderTransform as ScaleTransform;
                if (scaleTransform == null)
                {
                    scaleTransform = new ScaleTransform(1, 1);
                    canvas.RenderTransform = scaleTransform;
                    canvas.RenderTransformOrigin = new Point(0.5, 0.5);
                }

                // Animacja powiększenia
                DoubleAnimation scaleAnimation = new DoubleAnimation(1.2, TimeSpan.FromSeconds(0.3));

                // Zastosowanie animacji
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            }
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Canvas canvas)
            {
                // Przywrócenie domyślnego koloru obramowania dla Path
                foreach (var child in canvas.Children)
                {
                    if (child is Path path)
                    {
                        path.Stroke = Brushes.Black; // Domyślny kolor
                    }
                }

                // Pobranie transformacji
                ScaleTransform scaleTransform = canvas.RenderTransform as ScaleTransform;
                if (scaleTransform == null) return;

                // Animacja powrotu do pierwotnego rozmiaru
                DoubleAnimation scaleAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3));

                // Zastosowanie animacji
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            }
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero; // Resetuj czas filmu
            BackgroundVideo.Play(); // Odtwórz ponownie
        }

        /*private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Canvas canvas)
            {
                // Pobranie wymiarów Canvas
                double canvasWidth = canvas.ActualWidth;
                double canvasHeight = canvas.ActualHeight;

                // Jeśli wymiary są 0, unikamy dzielenia przez 0
                if (canvasWidth == 0 || canvasHeight == 0)
                {
                    MessageBox.Show("Błąd: Wymiary Canvas są niepoprawne.");
                    return;
                }

                // Pobranie pozycji Canvas
                double left = Canvas.GetLeft(canvas);
                double top = Canvas.GetTop(canvas);

                // Jeśli wartości są NaN, ustawiamy je na 0
                if (double.IsNaN(left)) left = 0;
                if (double.IsNaN(top)) top = 0;

                // Środek Canvas względem rodzica
                double centerX = left + (canvasWidth / 2);
                double centerY = top + (canvasHeight / 2);

                // Obliczenie nowego przesunięcia
                double newX = -centerX * (zoomFactor - 1);
                double newY = -centerY * (zoomFactor - 1);

                // Zapobieganie NaN
                if (double.IsNaN(newX) || double.IsNaN(newY))
                {
                    MessageBox.Show("Błąd: Obliczenia zwróciły nieprawidłowe wartości.");
                    return;
                }

                // Animacja powiększenia
                DoubleAnimation scaleAnim = new DoubleAnimation(zoomFactor, TimeSpan.FromSeconds(duration));
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);

                // Animacja przesunięcia
                DoubleAnimation translateXAnim = new DoubleAnimation(newX, TimeSpan.FromSeconds(duration));
                DoubleAnimation translateYAnim = new DoubleAnimation(newY, TimeSpan.FromSeconds(duration));
                translateTransform.BeginAnimation(TranslateTransform.XProperty, translateXAnim);
                translateTransform.BeginAnimation(TranslateTransform.YProperty, translateYAnim);


                // Pobranie nazwy kontynentu z nazwy Canvy
                string kontynentNazwa = canvas.Name;

                // Sprawdzenie czy istnieje w słowniku kontynentów
                *//*if (kontynenty.TryGetValue(kontynentNazwa, out Kontynent? value))
                {
                    MessageBox.Show($"Kontynent: {value.Nazwa}");
                }
                else
                {
                    MessageBox.Show("Nie znaleziono kontynentu.");
                }*//*
            }
        }*/

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Canvas canvas)
            {
                // Pobranie wymiarów Canvas
                double canvasWidth = canvas.ActualWidth;
                double canvasHeight = canvas.ActualHeight;

                // Jeśli wymiary są 0, unikamy dzielenia przez 0
                if (canvasWidth == 0 || canvasHeight == 0)
                {
                    MessageBox.Show("Błąd: Wymiary Canvas są niepoprawne.");
                    return;
                }

                // Pobranie pozycji Canvas
                double left = Canvas.GetLeft(canvas);
                double top = Canvas.GetTop(canvas);

                // Jeśli wartości są NaN, ustawiamy je na 0
                if (double.IsNaN(left)) left = 0;
                if (double.IsNaN(top)) top = 0;

                // Środek Canvas względem rodzica
                double centerX = left + (canvasWidth / 2);
                double centerY = top + (canvasHeight / 2);

                // Obliczenie nowego przesunięcia
                double newX = -centerX * (zoomFactor - 1);
                double newY = -centerY * (zoomFactor - 1);

                // Zapobieganie NaN
                if (double.IsNaN(newX) || double.IsNaN(newY))
                {
                    MessageBox.Show("Błąd: Obliczenia zwróciły nieprawidłowe wartości.");
                    return;
                }

                // Upewnij się, że Canvas ma odpowiedni RenderTransform
                ScaleTransform scaleTransform = canvas.RenderTransform as ScaleTransform;
                if (scaleTransform == null)
                {
                    scaleTransform = new ScaleTransform(1, 1);
                    canvas.RenderTransform = scaleTransform;
                    canvas.RenderTransformOrigin = new Point(0.5, 0.5); // Ustawienie środka transformacji
                }

                TranslateTransform translateTransform = canvas.RenderTransform as TranslateTransform;
                if (translateTransform == null)
                {
                    translateTransform = new TranslateTransform();
                    canvas.RenderTransform = new TransformGroup { Children = { scaleTransform, translateTransform } };
                }

                // Animacja powiększenia mapy (zoom)
                DoubleAnimation scaleAnim = new DoubleAnimation(zoomFactor, TimeSpan.FromSeconds(duration));
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);

                // Animacja przesunięcia mapy, aby środek kontynentu stał się widoczny
                DoubleAnimation translateXAnim = new DoubleAnimation(newX, TimeSpan.FromSeconds(duration));
                DoubleAnimation translateYAnim = new DoubleAnimation(newY, TimeSpan.FromSeconds(duration));
                translateTransform.BeginAnimation(TranslateTransform.XProperty, translateXAnim);
                translateTransform.BeginAnimation(TranslateTransform.YProperty, translateYAnim);
            }
        }




    }
}
