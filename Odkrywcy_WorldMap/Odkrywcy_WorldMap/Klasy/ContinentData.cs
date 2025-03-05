using System;
using System.Collections.Generic;
using System.Linq;

namespace Odkrywcy_WorldMap
{
    public class ContinentData
    {
        private Dictionary<string, List<string>> continentWords;

        public ContinentData()
        {
            // Tworzymy słownik kontynentów z odpowiadającymi im hasłami
            continentWords = new Dictionary<string, List<string>>
            {
                { "Ameryka Północna", new List<string> { "Kanada", "Nowy Jork", "Meksyk", "Toronto", "Góry Skaliste", "Lasy", "Plaże", "Husky" } },
                { "Ameryka Południowa", new List<string> { "Amazonka", "Rio", "Karnawał", "Machu Picchu", "Andy", "Brazylia", "Patagonia", "Lima" } },
                { "Afryka", new List<string> { "Sahara", "Egipt", "Masaje", "Kair", "Kilimandżaro", "Zebra", "Góry Atlas", "Wielka Rafa" } },
                { "Azja", new List<string> { "Himalaje", "Japonia", "Chiny", "Indie", "Bali", "Singapur", "Zatoka", "Rekiny" } },
                { "Europa", new List<string> { "Paryż", "Rzym", "Alpy", "Berlin", "Watykan", "Madryt", "Londyn", "Amsterdam" } },
                { "Australia", new List<string> { "Sydney", "Uluru", "Rafa", "Aborygeni", "Wielbłąd", "Kangur", "Outback", "Bondi" } },
                { "Antarktyda", new List<string> { "Lód", "Pingwiny", "Stacja", "Góry", "Morze", "Wiatry", "Białe", "Zimno" } }
            };
        }

        public List<string> GetContinentWords(string continent)
        {
            if (continentWords.ContainsKey(continent))
            {
                // Zwracamy skopiowaną listę haseł dla konkretnego kontynentu
                return continentWords[continent].Concat(continentWords[continent]).OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            }
            else if (continent == "Ogólny")
            {
                // Tworzymy listę ogólną, łącząc wszystkie hasła z kontynentów
                var allWords = continentWords.Values.SelectMany(x => x).ToList();

                // Losujemy 8 słów z połączonej listy
                return allWords.OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            }

            return new List<string>(); // Jeśli kontynent nie istnieje, zwróć pustą listę
        }
    }
}
