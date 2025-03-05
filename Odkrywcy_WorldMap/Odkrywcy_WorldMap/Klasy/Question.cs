namespace Odkrywcy_WorldMap.Klasy
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string questionText, List<string> answers, int correctAnswerIndex)
        {
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
        }

        // Statyczna metoda do zwrócenia listy pytań na podstawie kontynentu
        public static List<Question> GetQuestions(string continent)
        {
            switch (continent)
            {
                case "Afryka":
                    return new List<Question>
            {
                new Question("Jaki jest największy park narodowy w Afryce?", new List<string> { "Kruger", "Serengeti", "Ngorongoro", "Wielki Kanion" }, 1),
                new Question("Które zwierzę z tzw. 'Wielkiej Piątki' żyje na sawannie Afryki?", new List<string> { "Słoń", "Żyrafa", "Lew", "Bawół" }, 2),
                new Question("W którym państwie znajduje się Sahara?", new List<string> { "Egipt", "Nigeria", "Chad", "Mali" }, 0),
                new Question("Kiedy powstało Królestwo Mali?", new List<string> { "XIV w.", "XV w.", "X w.", "XI w." }, 0),
                new Question("Jakie miasto w Afryce jest jednym z najstarszych na kontynencie?", new List<string> { "Marrakesz", "Kair", "Tunis", "Algier" }, 1)
            };

                case "Antarktyda":
                    return new List<Question>
            {
                new Question("Jaki jest główny cel stacji badawczej w Antarktydzie?", new List<string> { "Badania geologiczne", "Badania meteorologiczne", "Badania biologiczne", "Badania oceanograficzne" }, 1),
                new Question("Kto jako pierwszy odkrył Antarktydę?", new List<string> { "James Cook", "Roald Amundsen", "Ernest Shackleton", "Robert Scott" }, 0),
                new Question("Jaka jest średnia temperatura w Antarktydzie?", new List<string> { "-5°C", "-20°C", "-30°C", "-50°C" }, 3),
                new Question("W jakim roku powstał Traktat Antarktyczny?", new List<string> { "1947", "1959", "1965", "1973" }, 1)
            };

                case "Azja":
                    return new List<Question>
            {
                new Question("Jaka jest największa religia w Azji?", new List<string> { "Hinduizm", "Buddyzm", "Islam", "Chrześcijaństwo" }, 2),
                new Question("Gdzie znajduje się najwyższy szczyt Azji?", new List<string> { "Himalaje", "Tian Shan", "Karakoram", "Kunlun" }, 0),
                new Question("Jakie państwo w Azji ma najwięcej ludności?", new List<string> { "Indie", "Chiny", "Indonezja", "Pakistan" }, 1),
                new Question("Jak nazywa się najbardziej zaludniona wyspa Azji?", new List<string> { "Jawa", "Sumatra", "Borneo", "Cejlon" }, 0)
            };

                case "Europa":
                    return new List<Question>
            {
                new Question("Które miasto jest stolicą Włoch?", new List<string> { "Mediolan", "Rzym", "Neapol", "Turyn" }, 1),
                new Question("Jakie jest najwyższe pasmo górskie w Europie?", new List<string> { "Alpy", "Karpaty", "Pireneje", "Apeniny" }, 0),
                new Question("Kiedy powstała Unia Europejska?", new List<string> { "1957", "1973", "1986", "1992" }, 0),
                new Question("Kto jest twórcą teorii heliocentrycznej?", new List<string> { "Galileo Galilei", "Nicolaus Copernicus", "Johannes Kepler", "Isaac Newton" }, 1)
            };

                case "AmerykaPolnocna":
                    return new List<Question>
            {
                new Question("Jakie miasto w Ameryce Północnej jest znane jako 'Miasto Aniołów'?", new List<string> { "Nowy Jork", "Toronto", "Los Angeles", "Chicago" }, 2),
                new Question("Kiedy powstało pierwsze państwo w Ameryce Północnej?", new List<string> { "1492", "1607", "1776", "1821" }, 1),
                new Question("Które państwo w Ameryce Północnej ma największą powierzchnię?", new List<string> { "USA", "Kanada", "Meksyk", "Gwatemala" }, 1),
                new Question("Jakie zwierzę jest symbolem Kanady?", new List<string> { "Lis", "Bison", "Orzeł", "Bóbr" }, 3)
            };

                case "AmerykaPoludniowa":
                    return new List<Question>
            {
                new Question("Które państwo w Ameryce Południowej ma największą powierzchnię?", new List<string> { "Brazylia", "Argentyna", "Chile", "Kolumbia" }, 0),
                new Question("Jaka rzeka jest najdłuższa w Ameryce Południowej?", new List<string> { "Amazonka", "Parana", "Orinoko", "Madeira" }, 0),
                new Question("W jakim państwie znajduje się największy las deszczowy?", new List<string> { "Brazylia", "Peru", "Kolumbia", "Wenezuela" }, 0),
                new Question("Jaki język jest najczęściej używany w Ameryce Południowej?", new List<string> { "Hiszpański", "Portugalski", "Angielski", "Francuski" }, 0)
            };

                case "Australia":
                    return new List<Question>
            {
                new Question("Jakie miasto jest stolicą Australii?", new List<string> { "Sydney", "Melbourne", "Canberra", "Brisbane" }, 2),
                new Question("Jaka jest najwyższa góra Australii?", new List<string> { "Mount Kosciuszko", "Mount Cook", "Mount Everest", "Uluru" }, 0),
                new Question("Kiedy Australia stała się federacją?", new List<string> { "1901", "1920", "1888", "1850" }, 0),
                new Question("Kiedy została odkryta Australia?", new List<string> { "1606", "1492", "1770", "1800" }, 0)
            };

                case "Ogolny":
                    // Łączymy wszystkie pytania z różnych kontynentów w jedną listę
                    var allQuestions = new List<Question>();
                    allQuestions.AddRange(GetQuestions("Afryka"));
                    allQuestions.AddRange(GetQuestions("Antarktyda"));
                    allQuestions.AddRange(GetQuestions("Azja"));
                    allQuestions.AddRange(GetQuestions("Europa"));
                    allQuestions.AddRange(GetQuestions("AmerykaPolnocna"));
                    allQuestions.AddRange(GetQuestions("AmerykaPoludniowa"));
                    allQuestions.AddRange(GetQuestions("Australia"));

                    // Losowanie pytania z połączonej listy
                    Random random = new Random();
                    int randomIndex = random.Next(allQuestions.Count);
                    return new List<Question> { allQuestions[randomIndex] }; // Zwracamy jedno losowe pytanie

                default:
                    return new List<Question>();
            }
        }

    }
}
