using Odkrywcy_WorldMap.Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Odkrywcy_WorldMap
{
    public partial class Kontynent_Page : Window
    {
        private Kontynent kontynent;
        private int currentSlideIndex = 0;
        private List<KeyValuePair<string, string>> slides;
        private string Nazwa_k;

        public Kontynent_Page(string nazwa, string nazwaBezPolskich)
        {
            InitializeComponent();
            kontynent = new Kontynent(nazwa, nazwaBezPolskich);
            slides = kontynent.OpisySlajdow.ToList();

            Nazwa_k = nazwaBezPolskich;

            if (slides.Count > 0)
                SetSlide(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Play();
        }

        private void SetSlide(int index)
        {
            if (index >= 0 && index < slides.Count)
            {
                currentSlideIndex = index;
                TytulSlajdu.Text = slides[index].Key;
                OpisSlajdu.Text = slides[index].Value.ToUpper();

                if (index < kontynent.Filmy.Length)
                {
                    BackgroundVideo.Source = new Uri($"Video/{Nazwa_k}/{kontynent.Filmy[index]}", UriKind.Relative);
                    BackgroundVideo.Play();
                }
            }
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero;
            BackgroundVideo.Play();
        }

        private void Lewo_Click(object sender, RoutedEventArgs e)
        {
            if (currentSlideIndex > 0)
                SetSlide(currentSlideIndex - 1);
        }

        private void Prawo_Click(object sender, RoutedEventArgs e)
        {
            if (currentSlideIndex < slides.Count - 1)
                SetSlide(currentSlideIndex + 1);
        }

        private void Quiz_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Quiz jeszcze nie jest zaimplementowany!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            // Utworzenie nowego okna
            WorldMap newWindow = new WorldMap();
            newWindow.Opacity = 0;
            newWindow.Show();

            // Rozjaśnianie nowego okna
            DoubleAnimation fadeInAnim = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(1.0));
            newWindow.BeginAnimation(UIElement.OpacityProperty, fadeInAnim);

            // Zamknięcie starego okna
            this.Close();
        }

    }
}
