using System;
using System.Collections.Generic;
using System.Linq;
using ExamSystem.Questions;

namespace ExamSystem.Exams
{
    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        public TimeSpan TimeOfExam { get; protected set; }
        public int NumberOfQuestions => Questions.Count;
        public List<Question> Questions { get; protected set; } = new();

        protected Exam(TimeSpan time) { TimeOfExam = time; }

        public abstract void ShowExam();

        public void AddQuestion(Question q) => Questions.Add(q);

        public override string ToString()
            => $"Exam: {GetType().Name}, Duration: {TimeOfExam}, Questions: {NumberOfQuestions}";

        public object Clone()
        {
            var clone = (Exam)MemberwiseClone();
            clone.Questions = Questions.Select(q => (Question)q.Clone()).ToList();
            return clone;
        }

        public int CompareTo(Exam? other)
            => other is null ? 1 : NumberOfQuestions.CompareTo(other.NumberOfQuestions);

        protected (int totalMark, int studentScore, List<(Question q, int studentAnswerId)> log) RunExamCore()
        {
            var log = new List<(Question, int)>();
            int total = Questions.Sum(q => q.Mark);
            int score = 0;

            Console.WriteLine($"-- {GetType().Name} starts. Duration {TimeOfExam} --");
            foreach (var q in Questions)
            {
                Console.WriteLine(new string('-', 40));
                int stud = q.AskAndGetStudentAnswer();
                log.Add((q, stud));
                if (q.IsStudentAnswerRight(stud)) score += q.Mark;
            }
            Console.WriteLine(new string('-', 40));
            return (total, score, log);
        }
    }
}
