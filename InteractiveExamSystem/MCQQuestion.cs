using System;
using System.Collections.Generic;
using ExamSystem.Domain;

namespace ExamSystem.Questions
{
    public class MCQQuestion : Question
    {
        public MCQQuestion(string header, string body, int mark, IEnumerable<string> choices, int rightAnswerIndex1Based)
            : base(header, body, mark)
        {
            int id = 1;
            foreach (var c in choices)
                AnswerList.Add(new Answer(id++, c));

            if (rightAnswerIndex1Based < 1 || rightAnswerIndex1Based > AnswerList.Count)
                throw new ArgumentOutOfRangeException(nameof(rightAnswerIndex1Based), "Right answer index out of range.");

            RightAnswerId = rightAnswerIndex1Based;
        }

        public override int AskAndGetStudentAnswer()
        {
            System.Console.WriteLine(ToString());
            foreach (var ans in AnswerList) System.Console.WriteLine(ans);
            System.Console.Write("Your answer (enter id): ");
            return ReadAnswerId(AnswerList.ConvertAll(a => a.AnswerId));
        }
    }
}
