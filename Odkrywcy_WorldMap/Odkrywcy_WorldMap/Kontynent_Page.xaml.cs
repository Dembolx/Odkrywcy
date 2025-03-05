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
                SetSlide(0, animate: false);
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Play();
        }

        private void SetSlide(int index, bool animate = true)
        {
            if (index >= 0 && index < slides.Count)
            {
                currentSlideIndex = index;

                if (animate)
                {
                    AnimateTextFadeOut(() => {
                        TytulSlajdu.Text = slides[index].Key;
                        OpisSlajdu.Text = slides[index].Value.ToUpper();
                        AnimateTextFadeIn();
                    });
                }
                else
                {
                    TytulSlajdu.Text = slides[index].Key;
                    OpisSlajdu.Text = slides[index].Value.ToUpper();
                }

                if (index < kontynent.Filmy.Length)
                {
                    AnimateVideoTransition(index);
                }
            }
        }

        private void AnimateTextFadeOut(Action onComplete)
        {
            DoubleAnimation fadeOutAnim = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.5));
            fadeOutAnim.Completed += (s, e) => onComplete?.Invoke();
            TytulSlajdu.BeginAnimation(UIElement.OpacityProperty, fadeOutAnim);
            OpisSlajdu.BeginAnimation(UIElement.OpacityProperty, fadeOutAnim);
        }

        private void AnimateTextFadeIn()
        {
            DoubleAnimation fadeInAnim = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.5));
            TytulSlajdu.BeginAnimation(UIElement.OpacityProperty, fadeInAnim);
            OpisSlajdu.BeginAnimation(UIElement.OpacityProperty, fadeInAnim);
        }

        private void AnimateVideoTransition(int index)
        {
            DoubleAnimation fadeOutAnim = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.5));
            fadeOutAnim.Completed += (s, e) =>
            {
                BackgroundVideo.Source = new Uri($"Video/{Nazwa_k}/{kontynent.Filmy[index]}", UriKind.Relative);
                BackgroundVideo.Play();
                DoubleAnimation fadeInAnim = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.5));
                BackgroundVideo.BeginAnimation(UIElement.OpacityProperty, fadeInAnim);
            };
            BackgroundVideo.BeginAnimation(UIElement.OpacityProperty, fadeOutAnim);
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
            Quiz_Page quiz_Page = new Quiz_Page();
            quiz_Page.Show();
            this.Close();
        }

        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            WorldMap newWindow = new WorldMap();
            newWindow.Opacity = 0;
            newWindow.Show();

            DoubleAnimation fadeInAnim = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(1.0));
            newWindow.BeginAnimation(UIElement.OpacityProperty, fadeInAnim);

            DoubleAnimation fadeOutAnim = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(1.0));
            fadeOutAnim.Completed += (s, e) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, fadeOutAnim);
        }
    }
}