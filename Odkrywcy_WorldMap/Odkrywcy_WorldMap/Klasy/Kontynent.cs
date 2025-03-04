using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Odkrywcy_WorldMap.Klasy
{
    class Kontynent
    {
        public string Nazwa { get; private set; }
        public Dictionary<string, string> OpisySlajdow { get; private set; }
        public string[] Filmy { get; private set; }

        private static Dictionary<string, string[]> FilmyKontynenty = new Dictionary<string, string[]>
        {
            { "Australia", new string[] { "kangur.mp4", "Outback.mp4", "surfer.mp4", "Aboryganie.mp4" } },        // Zrobione
            { "Europa", new string[] { "zabytki.mp4", "alpy.mp4", "londyn.mp4", "hiszpania.mp4" } },              // Zrobione
            { "Azja", new string[] { "azja.mp4", "tokyo.mp4", "himalaje.mp4", "mur.mp4" } },                      // Zrobione
            { "Afryka", new string[] { "afryka.mp4", "sahara.mp4", "safari.mp4", "plemie.mp4" } },                // Zrobione
            { "AmerykaPolnocna", new string[] { "yellowstone.mp4", "newyork.mp4", "gory.mp4", "tradycje.mp4" } }, // Zrobione
            { "AmerykaPoludniowa", new string[] { "amazonia.mp4", "rio.mp4", "uyni.mp4", "gory.mp4" } },          // Zrobione
            { "Antarktyda", new string[] { "antarktyda.mp4", "zorza.mp4", "pingwiny.mp4", "badania.mp4" } }       // Zrobione
        };

        public Kontynent(string nazwa, string nazwaBezPolskich)
        {
            Nazwa = nazwa;
            OpisySlajdow = WczytajOpisy(nazwaBezPolskich);
            Filmy = FilmyKontynenty.ContainsKey(nazwaBezPolskich) ? FilmyKontynenty[nazwaBezPolskich] : new string[0];
        }

        private Dictionary<string, string> WczytajOpisy(string nazwa)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Informacje", $"{nazwa}.txt");
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Nie ma");
                return dataDictionary;
            }

            string[] lines = File.ReadAllLines(filePath);
            string currentTitle = "";
            StringBuilder currentText = new StringBuilder();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Sprawdzamy, czy linia zaczyna się i kończy cudzysłowem (obsługujemy zarówno "..." jak i „...”)
                if (Regex.IsMatch(line, @"^[""„].+[""”]$"))
                {
                    if (!string.IsNullOrEmpty(currentTitle))
                        dataDictionary[currentTitle] = currentText.ToString().Trim();

                    currentTitle = line.Trim(' ', '"', '„', '”'); // Usuwamy cudzysłowy
                    currentText.Clear();
                }
                else
                {
                    currentText.AppendLine(line);
                }
            }

            if (!string.IsNullOrEmpty(currentTitle))
                dataDictionary[currentTitle] = currentText.ToString().Trim();

            return dataDictionary;
        }
    }
}
