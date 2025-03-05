using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

}
