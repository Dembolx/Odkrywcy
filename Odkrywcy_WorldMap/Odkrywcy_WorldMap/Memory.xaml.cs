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

        public Memory(Frame mainframe)
        {
            InitializeComponent();
            _mainframe = mainframe;
            InitializeGame();
        }

        private void InitializeGame()
        {
            CreateGameBoard(4, 4);
            SetupCardPairs();
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

        private void SetupCardPairs()
        {
            List<string> words = new List<string>
            {
                "Ameryka Północna", "Kontynent skrajności",
                "Miasta Ameryki", "Nowy Jork, Toronto, Meksyk",
                "Niezwykła Przyroda", "Góry, równiny, plaże",
                "Rdzenne Ludy", "Indianie, Inuici, Meksykanie"
            };

            words = words.Concat(words).OrderBy(x => Guid.NewGuid()).ToList();
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
    }
}
