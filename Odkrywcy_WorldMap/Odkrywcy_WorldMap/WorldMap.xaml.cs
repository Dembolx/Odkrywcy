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
    public partial class WorldMap : Page
    {
        private Dictionary<string, string> kontynenty = new Dictionary<string, string>()
        {
            { "AmerykaPolnocna", "Ameryka Północna" },
            { "AmerykaPoludniowa", "Ameryka Południowa" },
            { "Europa", "Europa" },
            { "Azja", "Azja" },
            { "Antarktyda", "Antarktyda" },
            { "Australia", "Australia" },
            { "Afryka", "Afryka" }
        };

        private double zoomFactor = 4.0;  // Współczynnik skalowania
        private double duration = 1.5;    // Czas trwania animacji

        private Canvas obecny_canvas;

        private Frame _mainFrame;
        public WorldMap(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void Quiz_ogolny(object sender, EventArgs e)
        {
            _mainFrame.Navigate(new Quiz_Page("Ogólny", "Ogolny", _mainFrame));
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

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Canvas canvas)
            {
                // Pobranie głównego kontenera
                obecny_canvas = canvas;
                FrameworkElement parent = canvas.Parent as FrameworkElement;
                if (parent == null)
                {
                    MessageBox.Show("Błąd: Canvas nie znajduje się w odpowiednim kontenerze.");
                    return;
                }

                double viewportWidth = parent.ActualWidth;
                double viewportHeight = parent.ActualHeight;

                if (viewportWidth == 0 || viewportHeight == 0)
                {
                    MessageBox.Show("Błąd: Wymiary ekranu są niepoprawne.");
                    return;
                }

                // Pobranie pozycji klikniętej Canvy
                Point canvasPosition = canvas.TranslatePoint(new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2), parent);
                double centerX = canvasPosition.X;
                double centerY = canvasPosition.Y;

                // Pobranie transformacji
                TransformGroup transformGroup = parent.RenderTransform as TransformGroup ?? new TransformGroup();
                parent.RenderTransform = transformGroup;

                ScaleTransform scaleTransform = transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault() ?? new ScaleTransform(1, 1);
                TranslateTransform translateTransform = transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault() ?? new TranslateTransform();

                if (!transformGroup.Children.Contains(scaleTransform)) transformGroup.Children.Add(scaleTransform);
                if (!transformGroup.Children.Contains(translateTransform)) transformGroup.Children.Add(translateTransform);

                // Obliczenie przesunięcia
                double newX = (viewportWidth / 2) - (centerX * zoomFactor);
                double newY = (viewportHeight / 2) - (centerY * zoomFactor);

                // Animacja powiększenia i przesunięcia
                DoubleAnimation scaleAnim = new DoubleAnimation(zoomFactor, TimeSpan.FromSeconds(duration));
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);

                DoubleAnimation translateXAnim = new DoubleAnimation(newX, TimeSpan.FromSeconds(duration));
                DoubleAnimation translateYAnim = new DoubleAnimation(newY, TimeSpan.FromSeconds(duration));
                translateTransform.BeginAnimation(TranslateTransform.XProperty, translateXAnim);
                translateTransform.BeginAnimation(TranslateTransform.YProperty, translateYAnim);

                // Rozjaśnianie ekranu
                StartFadeOut();
            }
        }

        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void StartFadeOut()
        {
            DoubleAnimation fadeOutAnim = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(duration));
            fadeOutAnim.Completed += FadeOut_Completed;
            this.BeginAnimation(UIElement.OpacityProperty, fadeOutAnim);
        }

        private void FadeOut_Completed(object sender, EventArgs e)
        {
            // Sprawdzenie, czy obecny_canvas nie jest nullem
            if (obecny_canvas == null)
            {
                MessageBox.Show("Błąd: Brak wybranego kontynentu.");
                return;
            }

            // Pobranie nazwy kontynentu
            string canvasName = obecny_canvas.Name;

            // Sprawdzenie, czy nazwa istnieje w słowniku
            if (!kontynenty.ContainsKey(canvasName))
            {
                MessageBox.Show($"Błąd: Nie znaleziono kontynentu dla {canvasName}.");
                return;
            }

            // Pobranie pełnej nazwy kontynentu
            string nazwaKontynentu = kontynenty[canvasName];

            // Utworzenie nowego okna
            /*Kontynent_Page newWindow = new Kontynent_Page(nazwaKontynentu, canvasName);
            newWindow.Opacity = 0;
            newWindow.Show();*/

            _mainFrame.Navigate(new Kontynent_Page(nazwaKontynentu, canvasName, _mainFrame));

            // Rozjaśnianie nowego okna
            /*DoubleAnimation fadeInAnim = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(1.0));
            newWindow.BeginAnimation(UIElement.OpacityProperty, fadeInAnim);*/

        }


    }
}
