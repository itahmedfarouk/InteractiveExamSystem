using System;
using System.Linq;

namespace ExamSystem.Exams
{
    public class FinalExam : Exam
    {
        public FinalExam(TimeSpan time) : base(time) { }

        public override void ShowExam()
        {
            var (total, score, log) = RunExamCore();

            Console.WriteLine("\n== Exam Review ==");
            foreach (var (q, stud) in log)
            {
                Console.WriteLine(q);
                foreach (var ans in q.AnswerList)
                {
                    string tag = ans.AnswerId == q.RightAnswerId ? "[Correct]" :
                                 ans.AnswerId == stud ? "[Your Choice]" : "";
                    Console.WriteLine($"  - {ans} {tag}");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\nYour Grade: {score}/{total}");
        }
    }
}