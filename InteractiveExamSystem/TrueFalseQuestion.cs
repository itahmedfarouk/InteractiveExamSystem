using System.Collections.Generic;
using ExamSystem.Domain;

namespace ExamSystem.Questions
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string header, string body, int mark, bool rightIsTrue)
            : base(header, body, mark)
        {
            AnswerList = new List<Answer>
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            };
            RightAnswerId = rightIsTrue ? 1 : 2;
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
