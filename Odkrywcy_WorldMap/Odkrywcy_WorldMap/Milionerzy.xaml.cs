using Odkrywcy_WorldMap.Klasy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Odkrywcy_WorldMap
{
    public sealed partial class Milionerzy : Page
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private bool isQuestionAnswered = false;
        private Stopwatch stopwatch;
        private DispatcherTimer timer;

        private Frame _mainframe;
        private string nazwa, nazwabezposlkich;
        private TimeSpan elapsed;
        private int totalPoints = 0; // Variable to hold the total points

        public Milionerzy(string nazwa, string nazwabezposlkich, Frame mainframe)
        {
            this.InitializeComponent();
            questions = Question.GetQuestions(nazwabezposlkich);
            _mainframe = mainframe;
            this.nazwa = string.IsNullOrEmpty(nazwa) ? "Ogólny" : nazwa;
            this.nazwabezposlkich = nazwabezposlkich;
            StartGame();
        }

        private void StartGame()
        {
            currentQuestionIndex = 0;
            totalPoints = 0; // Reset total points at the start of the game
            StartTimer();
            LoadQuestion();
        }

        private void StartTimer()
        {
            stopwatch = Stopwatch.StartNew();
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            timer.Tick += (s, e) => { elapsed = stopwatch.Elapsed; };
            timer.Start();
        }

        private void StopTimer()
        {
            timer.Stop();
            stopwatch.Stop();
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                var question = questions[currentQuestionIndex];
                QuestionText.Text = question.QuestionText;
                Answer1.Content = question.Answers[0];
                Answer2.Content = question.Answers[1];
                Answer3.Content = question.Answers[2];
                Answer4.Content = question.Answers[3];
                FeedbackText.Text = "";
                isQuestionAnswered = false;

                ResultText.Visibility = Visibility.Collapsed;
                Answer1.Visibility = Visibility.Visible;
                Answer2.Visibility = Visibility.Visible;
                Answer3.Visibility = Visibility.Visible;
                Answer4.Visibility = Visibility.Visible;
            }
            else
            {
                StopTimer();
                EndGame(true);
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            if (isQuestionAnswered) return;

            var button = sender as Button;
            int answerIndex = -1;

            if (button == Answer1) answerIndex = 0;
            if (button == Answer2) answerIndex = 1;
            if (button == Answer3) answerIndex = 2;
            if (button == Answer4) answerIndex = 3;

            if (answerIndex == questions[currentQuestionIndex].CorrectAnswerIndex)
            {
                // Calculate points based on the current question index
                int points = (currentQuestionIndex + 1) * 100; // Points increase with each question
                totalPoints += points;

                FeedbackText.Text = $"Dobrze! Zdobywasz {points} punktów!";
                isQuestionAnswered = true;
                currentQuestionIndex++;
                LoadQuestion();
            }
            else
            {
                StopTimer();
                EndGame(false);
            }
        }

        private void EndGame(bool won)
        {
            string finalTime = $"{elapsed.Minutes:D2}:{elapsed.Seconds:D2}:{elapsed.Milliseconds / 10:D2}";
            string result = won ? "Wygrana" : "Przegrana";
            int score = totalPoints; // Final score based on the total points accumulated

            SaveResult(finalTime, score, result);

            if (won)
            {
                QuestionText.Text = "Gratulacje! Ukończyłeś grę.";
                ResultText.Text = $"Wygrałeś! Zdobyłeś {score} punktów.";
                ResultText.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ResultText.Text = $"Niestety, przegrałeś. Zdobyłeś {score} punktów.";
                ResultText.Foreground = new SolidColorBrush(Colors.Red);
            }

            ResultText.Visibility = Visibility.Visible;
            Answer1.Visibility = Visibility.Collapsed;
            Answer2.Visibility = Visibility.Collapsed;
            Answer3.Visibility = Visibility.Collapsed;
            Answer4.Visibility = Visibility.Collapsed;
        }

        private void SaveResult(string time, int score, string result)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Informacje", $"Quiz_Historia.txt");
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string gameName = "Milionerzy";
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

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
            ResultText.Visibility = Visibility.Collapsed;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new Quiz_Page(nazwa, nazwabezposlkich, _mainframe));
        }
    }
}
