using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Odkrywcy_WorldMap
{
    public partial class Kontynent_Page : Window
    {
        private string Nazwa;
        private string Nazwa_bezposlkich;
        private List<string> Slajd = new List<string>();
        private List<string> Film_do_Slajdu = new List<string>();
        private Dictionary<string, string> OpisySlajdow = new Dictionary<string, string>();


        private int currentSlideIndex = 0;
        private List<KeyValuePair<string, string>> slides = new List<KeyValuePair<string, string>>();

        public Kontynent_Page(string nazwa, string nazwa_bezposlkich)
        {
            InitializeComponent();
            this.Title = nazwa;
            this.Nazwa = nazwa;
            this.Nazwa_bezposlkich = nazwa_bezposlkich;

            // Wczytanie danych do słownika
            OpisySlajdow = LoadData(nazwa_bezposlkich);

            // Konwersja do listy KeyValuePair dla łatwiejszej obsługi indeksów
            slides = OpisySlajdow.ToList();

            // Załaduj pierwszy slajd
            if (slides.Count > 0)
            {
                SetSlide(0);
            }
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
                TytulSlajdu.Text = slides[index].Key;  // Klucz jako tytuł
                OpisSlajdu.Text = slides[index].Value.ToUpper();  // Wartość jako opis (wersalikami)
            }
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero;
            BackgroundVideo.Play();
        }

        private Dictionary<string, string> LoadData(string nazwa)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Informacje", $"{nazwa}.txt");
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Plik {filePath} nie został znaleziony!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return dataDictionary;
            }

            string[] lines = File.ReadAllLines(filePath);
            string currentTitle = "";
            StringBuilder currentText = new StringBuilder();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("🦘") || line.StartsWith("🌞") || line.StartsWith("🏄‍♂️") || line.StartsWith("🏛️"))
                {
                    if (!string.IsNullOrEmpty(currentTitle))
                    {
                        dataDictionary[currentTitle] = currentText.ToString().Trim();
                    }

                    currentTitle = line;
                    currentText.Clear();
                }
                else
                {
                    currentText.AppendLine(line);
                }
            }

            if (!string.IsNullOrEmpty(currentTitle))
            {
                dataDictionary[currentTitle] = currentText.ToString().Trim();
            }

            return dataDictionary;
        }


        private void WyswietlSlajd(int index)
        {
            if (index >= 0 && index < slides.Count)
            {
                currentSlideIndex = index;
                TytulSlajdu.Text = slides[index].Key;
                OpisSlajdu.Text = slides[index].Value;
            }
        }

        private void Lewo_Click(object sender, RoutedEventArgs e)
        {
            if (currentSlideIndex > 0)
            {
                WyswietlSlajd(currentSlideIndex - 1);
            }
        }

        private void Prawo_Click(object sender, RoutedEventArgs e)
        {
            if (currentSlideIndex < slides.Count - 1)
            {
                WyswietlSlajd(currentSlideIndex + 1);
            }
        }

        private void Quiz_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Quiz jeszcze nie jest zaimplementowany!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
