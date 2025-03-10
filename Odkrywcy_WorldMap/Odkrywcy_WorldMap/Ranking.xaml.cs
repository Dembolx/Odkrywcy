using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Odkrywcy_WorldMap
{
    public partial class Ranking : Page
    {
        private string historyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Informacje", "Quiz_Historia.txt");

        private Frame _mainframe;

        public Ranking(Frame mainframe)
        {
            InitializeComponent();
            LoadGameHistory();
            _mainframe = mainframe;
        }

        private void LoadGameHistory()
        {
            if (!File.Exists(historyFilePath))
                return;

            var historyEntries = new List<GameHistoryEntry>();

            foreach (var line in File.ReadAllLines(historyFilePath))
            {
                var parts = line.Split('|');

                if (parts.Length < 5) continue;

                string date = parts[0].Trim().Trim('[', ']');
                string game = parts[1].Trim();
                string continent = parts.Any(p => p.Contains("Kontynent:")) ? parts.FirstOrDefault(p => p.Contains("Kontynent:"))?.Replace("Kontynent:", "").Trim() : "Brak";
                string pointsStr = parts.Any(p => p.Contains("Punkty:")) ? parts.FirstOrDefault(p => p.Contains("Punkty:"))?.Replace("Punkty:", "").Trim() : "0";
                string time = parts.FirstOrDefault(p => p.Contains("Czas:"))?.Replace("Czas:", "").Trim() ?? "N/A";
                string result = parts.Last().Trim();

                int points = int.TryParse(pointsStr, out int parsedPoints) ? parsedPoints : 0;

                historyEntries.Add(new GameHistoryEntry
                {
                    Date = date,
                    Game = game,
                    Continent = continent,
                    Points = points,
                    Time = time,
                    Result = result
                });
            }

            // Sortowanie historii gier od najnowszych do najstarszych
            var sortedHistoryEntries = historyEntries.OrderByDescending(entry => DateTime.Parse(entry.Date)).ToList();

            // Wypełnienie sekcji Historia Gier
            GameHistoryList.ItemsSource = sortedHistoryEntries;

            // Wypełnienie sekcji Ranking Gier (posortowane według ilości zdobytych punktów)
            var rankingEntries = historyEntries.OrderByDescending(entry => entry.Points).ToList();
            GameRankingList.ItemsSource = rankingEntries;
        }


        // Obsługa przycisku Wyjście
        private void Wyjscie_Click(object sender, RoutedEventArgs e)
        {
            _mainframe.Navigate(new WorldMap(_mainframe));
        }
    }

    public class GameHistoryEntry
    {
        public string Date { get; set; }
        public string Game { get; set; }
        public string Continent { get; set; }
        public int Points { get; set; }
        public string Time { get; set; }
        public string Result { get; set; }
    }
}
