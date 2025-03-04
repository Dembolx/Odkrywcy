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
            { "Australia", new string[] { "kangur.mp4", "Outback.mp4", "surfer.mp4", "Aboryganie.mp4" } }, // Zrobione
            { "Europa", new string[] { "zabytki.mp4", "alpy.mp4", "londyn.mp4", "hiszpania.mp4" } },       // Zrobione
            { "Azja", new string[] { "azja.mp4", "tokyo.mp4", "himalaje.mp4", "mur.mp4" } },               // Zrobione
            { "Afryka", new string[] { "afryka.mp4", "sahara.mp4", "safari.mp4", "plemie.mp4" } },         // Zrobione
            { "AmerykaPolnocna", new string[] { "kanion.mp4", "newyork.mp4", "park_yellowstone.mp4", "miasta.mp4" } },
            { "AmerykaPoludniowa", new string[] { "amazonka.mp4", "rio.mp4", "andy.mp4", "machu_picchu.mp4" } },
            { "Antarktyda", new string[] { "pingwiny.mp4", "lodowce.mp4", "stacje_badawcze.mp4", "burze_sniezne.mp4" } }
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

            MessageBox.Show(filePath);

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
