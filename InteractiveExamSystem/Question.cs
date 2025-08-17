using System;
using System.Collections.Generic;
using System.Linq;
using ExamSystem.Domain;

namespace ExamSystem.Questions
{
    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; protected set; }
        public string Body { get; protected set; }
        public int Mark { get; protected set; }

        public List<Answer> AnswerList { get; protected set; } = new();
        public int RightAnswerId { get; protected set; }

        protected Question(string header, string body, int mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
        }

        public abstract int AskAndGetStudentAnswer();

        public bool IsStudentAnswerRight(int studentAnswerId) => studentAnswerId == RightAnswerId;

        public override string ToString() => $"{Header}\n{Body} (Mark: {Mark})";

        public object Clone()
        {
            var clone = (Question)MemberwiseClone();
            clone.AnswerList = AnswerList.Select(a => (Answer)a.Clone()).ToList();
            return clone;
        }

        public int CompareTo(Question? other) => other is null ? 1 : Mark.CompareTo(other.Mark);

        protected static int ReadAnswerId(IEnumerable<int> valid)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int id) && valid.Contains(id))
                    return id;
                Console.Write("Invalid. Try again: ");
            }
        }
    }
}
