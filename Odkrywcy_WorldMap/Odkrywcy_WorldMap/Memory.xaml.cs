using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Odkrywcy_WorldMap
{
    public partial class Memory : Page
    {
        private Dictionary<Button, string> cardPairs = new Dictionary<Button, string>();
        private Button firstClicked = null;
        private Button secondClicked = null;
        private DispatcherTimer timer = new DispatcherTimer();
        private Frame _mainframe;
        private ContinentData continentData;
        private Stopwatch stopwatch = new Stopwatch();

        private string nazwa, nazwabezpolskich;

        public Memory(Frame mainframe, string nazwa, string nazwabezposlkich)
        {
            InitializeComponent();
            _mainframe = mainframe;
            continentData = new ContinentData();
            this.nazwa = string.IsNullOrEmpty(nazwa) ? "Ogólny" : nazwa;
            this.nazwabezpolskich = nazwabezposlkich;

            InitializeGame(nazwa);
        }

        private void InitializeGame(string continent)
        {
            stopwatch.Restart();
            CreateGameBoard(4, 4);
            SetupCardPairs(continent);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void CreateGameBoard(int rows, int cols)
        {
            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < rows; i++)
                GameGrid.RowDefinitions.Add(new RowDefinition());

            for (int j = 0; j < cols; j++)
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button
                    {
                        Style = (Style)FindResource("MemoryCardStyle"),
                        Content = "?",
                        RenderTransform = new ScaleTransform(1, 1),
                    };

                    button.Click += Card_Click;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    GameGrid.Children.Add(button);
                }
            }
        }

        private void SetupCardPairs(string continent)
        {
            List<string> words = continentData.GetContinentWords(continent);
            int buttonCount = GameGrid.Children.OfType<Button>().Count();

            while (words.Count < buttonCount)
                words.AddRange(words);

            words = words.Take(buttonCount).OrderBy(x => Guid.NewGuid()).ToList();
            int index = 0;

            foreach (var child in GameGrid.Children)
            {
                if (child is Button button)
                {
                    cardPairs[button] = words[index];
                    index++;
                }
            }
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Button clickedButton = sender as Button;

            Storyboard flipAnimation = (Storyboard)FindResource("FlipAnimation");
            flipAnimation.Begin(clickedButton);

            clickedButton.Content = cardPairs[clickedButton];

            if (firstClicked == null)
            {
                firstClicked = clickedButton;
            }
            else if (secondClicked == null && clickedButton != firstClicked)
            {
                secondClicked = clickedButton;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (cardPairs[firstClicked] == cardPairs[secondClicked])
            {
                firstClicked.IsEnabled = false;
                secondClicked.IsEnabled = false;
                firstClicked.Background = Brushes.DarkGreen;
                secondClicked.Background = Brushes.DarkGreen;
            }
            else
            {
                firstClicked.Content = "?";
                secondClicked.Content = "?";
            }

            firstClicked = null;
            secondClicked = null;

            if (GameGrid.Children.OfType<Button>().All(b => !b.IsEnabled))
                EndGame(true);
        }

        private void EndGame(bool completed)
        {
            stopwatch.Stop();
            string finalTime = stopwatch.Elapsed.ToString(@"mm\:ss\:ff");
            string result = completed ? "Ukończono" : "Przerwano";
            int score = completed ? CalculateScore(stopwatch.Elapsed) : 0;

            // Wyświetl wynik i czas gry
            string message = $"Gratulacje! {result}\nCzas gry: {finalTime}\nTwój wynik: {score} punktów.";
            MessageBox.Show(message, "Koniec gry", MessageBoxButton.OK, MessageBoxImage.Information);

            SaveResult(finalTime, score, result);
        }

        private int CalculateScore(TimeSpan elapsedTime)
        {
            double totalSeconds = elapsedTime.TotalSeconds;

            // Można dostosować punktację zależnie od czasu
            double maxScore = 1000;
            double penaltyPerSecond = maxScore / 300; // 5 minut = 0 pkt
            int finalScore = (int)(maxScore - (totalSeconds * penaltyPerSecond));

            return Math.Max(finalScore, 0); // Minimalny wynik to 0
        }

        private void SaveResult(string time, int score, string result)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Informacje", $"Quiz_Historia.txt");
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string gameName = "Memory";
            string entry = $"[{currentDate}] | {gameName} | Kontynent: {nazwa} | Czas: {time} | Punkty: {score} | {result}";

            try
            {
                File.AppendAllText(filePath, entry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd zapisu pliku: {ex.Message}");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            EndGame(false);
            _mainframe.Navigate(new Quiz_Page(nazwa, nazwabezpolskich, _mainframe));
        }
    }
}
