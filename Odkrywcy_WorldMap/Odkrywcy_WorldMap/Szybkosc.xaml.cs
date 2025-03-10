using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Odkrywcy_WorldMap
{
    public partial class Szybkosc : Page
    {
        private List<(string Question, string[] Answers, int CorrectIndex)> questions;
        private int currentQuestionIndex = 0;
        private int errorCount = 0; // Licznik błędów
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private bool gameStarted = false;

        private Frame _mainframe;
        private string nazwa, nazwabezpolskich;
        public Szybkosc(string nazwa, string nazwaBezPolskich, Frame mainframe)
        {
            InitializeComponent();
            InitializeQuestions();
            ErrorCounter.Text = "Błędy: 0 (+0s)"; // Inicjalizacja licznika błędów
            this.nazwa = string.IsNullOrEmpty(nazwa) ? "Ogólny" : nazwa;
            this.nazwabezpolskich = nazwaBezPolskich;
            this._mainframe = mainframe;
        }

        private void InitializeQuestions()
        {
            questions = new List<(string, string[], int)>
            {
                ("Stolica Francji?", new string[] { "Berlin", "Madryt", "Paryż", "Rzym" }, 2),
                ("Największa rzeka Europy?", new string[] { "Loara", "Wołga", "Dunaj", "Ren" }, 1),
                ("Jaki kraj jest największy pod względem powierzchni?", new string[] { "Niemcy", "Francja", "Hiszpania", "Rosja" }, 3),
                ("Waluta w Niemczech?", new string[] { "Euro", "Złoty", "Funt", "Dolar" }, 0),
                ("Gdzie leży Alhambra?", new string[] { "Francja", "Włochy", "Hiszpania", "Portugalia" }, 2),
                ("Najwyższa góra Europy?", new string[] { "Mont Blanc", "Everest", "Elbrus", "Matterhorn" }, 2),
                ("Który kraj nie graniczy z Polską?", new string[] { "Czechy", "Litwa", "Węgry", "Ukraina" }, 2),
                ("Gdzie znajduje się Akropol?", new string[] { "Ateny", "Rzym", "Paryż", "Berlin" }, 0),
                ("Największa wyspa Europy?", new string[] { "Islandia", "Wielka Brytania", "Sycylia", "Kreta" }, 1),
                ("Gdzie znajduje się Wieża Eiffla?", new string[] { "Rzym", "Paryż", "Berlin", "Londyn" }, 1)
            };
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            gameStarted = true;
            StartGameButton.Visibility = Visibility.Collapsed;
            TimerText.Visibility = Visibility.Visible;
            QuestionText.Visibility = Visibility.Visible;
            QuestionCounter.Visibility = Visibility.Visible;
            ErrorCounter.Visibility = Visibility.Visible;
            AnswerButtons.Visibility = Visibility.Visible;

            currentQuestionIndex = 0;
            errorCount = 0;
            ErrorCounter.Text = "Błędy: 0 (+0s)"; // Reset licznika błędów

            StartTimer();
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                EndGame();
                return;
            }

            var (question, answers, _) = questions[currentQuestionIndex];
            QuestionText.Text = question;
            QuestionCounter.Text = $"Pytanie {currentQuestionIndex + 1} / {questions.Count}";

            AnswerButtons.Children.Clear(); // Wyczyść poprzednie odpowiedzi

            for (int i = 0; i < answers.Length; i++)
            {
                Button answerButton = new Button
                {
                    Content = answers[i],
                    Tag = i,
                    Style = (Style)FindResource("AnswerButtonStyle")
                };
                answerButton.Click += Answer_Click;
                AnswerButtons.Children.Add(answerButton);
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                int selectedIndex = (int)clickedButton.Tag;

                if (selectedIndex == questions[currentQuestionIndex].CorrectIndex)
                {
                    currentQuestionIndex++;
                    LoadQuestion();
                }
                else
                {
                    // Dodanie błędu i aktualizacja licznika
                    errorCount++;
                    ErrorCounter.Text = $"Błędy: {errorCount} (+{errorCount * 3}s)";
                }
            }
        }

        private void StartTimer()
        {
            stopwatch = Stopwatch.StartNew();
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            timer.Tick += (s, e) =>
            {
                TimeSpan elapsed = stopwatch.Elapsed;
                int penaltyTime = errorCount * 3; // Czas karny w sekundach
                TimerText.Text = $"{elapsed.Minutes:D2}:{elapsed.Seconds + penaltyTime:D2}:{elapsed.Milliseconds / 10:D2} (+{penaltyTime}s)";
            };
            timer.Start();
        }

        private void EndGame()
        {
            timer.Stop();
            stopwatch.Stop();

            // Oblicz wynik
            double totalTime = stopwatch.Elapsed.TotalSeconds;
            int penaltyTime = errorCount * 3; // Kara za błędy w sekundach
            double baseScore = Math.Max(0, 1000 - totalTime); // Mniej czasu = lepszy wynik
            double errorPenalty = penaltyTime * 10; // Więcej błędów = większa kara

            // Finalny wynik = podstawa - kara za czas i błędy
            int score = (int)(baseScore - errorPenalty);

            // Zapewniamy, że wynik nie będzie mniejszy niż 0
            score = Math.Max(score, 0);

            string finalTime = TimerText.Text;
            string result = currentQuestionIndex >= questions.Count ? "Wygrana" : "Przegrana";

            // Zapisz wynik do pliku
            SaveResult(finalTime, score, result);

            // Wyświetlenie komunikatu o wyniku
            MessageBox.Show($"Gratulacje! Ukończyłeś quiz w czasie: {finalTime}\nWynik: {score} punktów\nWynik zapisany do pliku.");
        }

        private void SaveResult(string time, int score, string result)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Informacje", "Quiz_Historia.txt");
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string gameName = "Szybkość";
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

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new Quiz_Page(nazwa, nazwabezpolskich, _mainframe));
        }
    }
}
