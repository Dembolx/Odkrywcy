using System;
using System.Collections.Generic;
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

        private string nazwa, nazwabezpolskich;

        public Memory(Frame mainframe, string nazwa, string nazwabezposlkich)
        {
            InitializeComponent();
            _mainframe = mainframe;

            continentData = new ContinentData();
            InitializeGame(nazwa);
            this.nazwa = nazwa;
            this.nazwabezpolskich = nazwabezposlkich;
        }

        private void InitializeGame(string continent)
        {
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

            // Upewniamy się, że liczba przycisków w GameGrid jest zgodna z liczbą słów w words.
            int buttonCount = GameGrid.Children.OfType<Button>().Count();

            if (words.Count != buttonCount)
            {
                // Jeśli liczba słów jest mniejsza niż liczba przycisków, powielamy słowa.
                while (words.Count < buttonCount)
                {
                    words.AddRange(words); // Powielamy listę, aż osiągniemy wymaganą liczbę słów
                }

                // Jeśli lista ma teraz więcej elementów niż liczba przycisków, przycinamy ją
                words = words.Take(buttonCount).ToList();
            }

            // Losowanie i przypisanie słów do przycisków
            words = words.OrderBy(x => Guid.NewGuid()).ToList(); // Mieszamy listę
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
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Przejdź do strony Quizy_Page
            _mainframe.Navigate(new Quiz_Page(nazwa,nazwabezpolskich,_mainframe));
        }
    }
}
