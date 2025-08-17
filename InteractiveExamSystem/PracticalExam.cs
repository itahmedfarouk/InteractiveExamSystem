using System;
using System.Linq;

namespace ExamSystem.Exams
{
    public class PracticalExam : Exam
    {
        public PracticalExam(TimeSpan time) : base(time) { }

        public override void ShowExam()
        {
            var (total, score, log) = RunExamCore();
            Console.WriteLine("\n== Correct Answers ==");
            foreach (var (q, _) in log)
            {
                var right = q.AnswerList.First(a => a.AnswerId == q.RightAnswerId);
                Console.WriteLine($"{q.Body} -> Correct: {right.AnswerText}");
            }
            Console.WriteLine($"\nYour Grade: {score}/{total}");
        }
    }
}