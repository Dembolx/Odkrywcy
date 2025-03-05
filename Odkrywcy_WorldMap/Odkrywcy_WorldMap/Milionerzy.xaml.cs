using Odkrywcy_WorldMap.Klasy;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Odkrywcy_WorldMap
{
    public sealed partial class Milionerzy : Page
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private bool isQuestionAnswered = false;

        private Frame _mainframe;

        private string nazwa, nazwabezposlkich;

        public Milionerzy(string nazwa, string nazwabezposlkich, Frame mainframe)
        {
            this.InitializeComponent();
            questions = Question.GetQuestions(nazwabezposlkich); // Ładowanie pytań na podstawie wybranego kontynentu
            LoadQuestion();
            _mainframe = mainframe;
            this.nazwa = nazwa;
            this.nazwabezposlkich = nazwabezposlkich;
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

                // Ukryj komunikat o wyniku i przyciski na początku nowego pytania
                ResultText.Visibility = Visibility.Collapsed;
                Answer1.Visibility = Visibility.Visible;
                Answer2.Visibility = Visibility.Visible;
                Answer3.Visibility = Visibility.Visible;
                Answer4.Visibility = Visibility.Visible;
            }
            else
            {
                // Komunikat o wygranej
                QuestionText.Text = "Gratulacje! Ukończyłeś grę.";
                Answer1.Visibility = Visibility.Collapsed;
                Answer2.Visibility = Visibility.Collapsed;
                Answer3.Visibility = Visibility.Collapsed;
                Answer4.Visibility = Visibility.Collapsed;

                // Komunikat o wygranej
                ResultText.Text = "Wygrałeś! Gratulacje!";
                ResultText.Foreground = new SolidColorBrush(Colors.Green); // Zielony kolor dla wygranej
                ResultText.Visibility = Visibility.Visible;
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            if (isQuestionAnswered) return;

            var button = sender as Button;
            var answerIndex = -1;

            if (button == Answer1) answerIndex = 0;
            if (button == Answer2) answerIndex = 1;
            if (button == Answer3) answerIndex = 2;
            if (button == Answer4) answerIndex = 3;

            var correctAnswerIndex = questions[currentQuestionIndex].CorrectAnswerIndex;

            if (answerIndex == correctAnswerIndex)
            {
                FeedbackText.Text = "Dobrze!";
                isQuestionAnswered = true;
                currentQuestionIndex++;
                LoadQuestion();
            }
            else
            {
                // Komunikat o porażce
                FeedbackText.Text = "Błąd! Prawidłowa odpowiedź to: " + questions[currentQuestionIndex].Answers[correctAnswerIndex];
                FeedbackText.Text += "\nKliknij, aby spróbować ponownie.";

                // Komunikat o porażce
                ResultText.Text = "Niestety, przegrałeś.";
                ResultText.Foreground = new SolidColorBrush(Colors.Red); // Czerwony kolor dla porażki
                ResultText.Visibility = Visibility.Visible;

                // Ukrycie przycisków odpowiedzi po przegranej
                Answer1.Visibility = Visibility.Collapsed;
                Answer2.Visibility = Visibility.Collapsed;
                Answer3.Visibility = Visibility.Collapsed;
                Answer4.Visibility = Visibility.Collapsed;
            }
        }


        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex = 0;
            ResultText.Visibility = Visibility.Collapsed; // Ukrycie komunikatu o wyniku przy resecie
            LoadQuestion();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new Quiz_Page(nazwa, nazwabezposlkich, _mainframe));
        }
    }
}
