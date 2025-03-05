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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Odkrywcy_WorldMap.Klasy;

namespace Odkrywcy_WorldMap
{
    /// <summary>
    /// Logika interakcji dla klasy Milionerzy.xaml
    /// </summary>
    public partial class Milionerzy : Page
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;

        public Milionerzy(Frame mainframe)
        {
            this.InitializeComponent();
            InitializeQuestions();
            LoadQuestion();
        }

        private void InitializeQuestions()
        {
            questions = new List<Question>
        {
            new Question(
                "Jaki jest najwięszy park narodowy w Ameryce Północnej?",
                new List<string> { "Yellowstone", "Banff", "Yosemite", "Grand Canyon" },
                0),

            new Question(
                "Które miasto jest nazywane 'Miastem Aniołów'?",
                new List<string> { "New York", "Toronto", "Los Angeles", "Chicago" },
                2),

            new Question(
                "W którym państwie znajduje się największa liczba parków narodowych w Ameryce Północnej?",
                new List<string> { "Kanada", "Meksyk", "USA", "Bermudy" },
                2),

            new Question(
                "Jak nazywa się rdzenna ludność Ameryki Północnej, która zamieszkuje Arktykę?",
                new List<string> { "Indianie", "Inuit", "Meskali", "Navajo" },
                1),

            new Question(
                "Kiedy został założony Nowy Jork?",
                new List<string> { "1624", "1776", "1492", "1850" },
                0),

            new Question(
                "Który kraj ma najwięcej sąsiadów w Ameryce Północnej?",
                new List<string> { "Kanada", "USA", "Meksyk", "Gwatemala" },
                1),
            
            // Można dodać resztę pytań na podstawie podanego tekstu
        };
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
            }
            else
            {
                QuestionText.Text = "Gratulacje! Ukończyłeś grę.";
                Answer1.Visibility = Visibility.Collapsed;
                Answer2.Visibility = Visibility.Collapsed;
                Answer3.Visibility = Visibility.Collapsed;
                Answer4.Visibility = Visibility.Collapsed;
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
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
            }
            else
            {
                FeedbackText.Text = "Błąd! Prawidłowa odpowiedź to: " + questions[currentQuestionIndex].Answers[correctAnswerIndex];
            }

            currentQuestionIndex++;
            LoadQuestion();
        }
    }
}
