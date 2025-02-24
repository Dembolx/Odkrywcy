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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Odkrywcy_WorldMap
{
    /// <summary>
    /// Logika interakcji dla klasy WorldMap.xaml
    /// </summary>
    public partial class WorldMap : Window
    {
        public WorldMap()
        {
            InitializeComponent();
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
    }
}
