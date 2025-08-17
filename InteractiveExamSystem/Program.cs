using System;
using ExamSystem.Domain;

namespace ExamSystem
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Examination System ===\n");

            var subject = new Subject(id: 101, name: "OOP Fundamentals");
            subject.CreateExamInteractive();

            Console.Clear();
            Console.WriteLine(subject);

            if (subject.Exam is null)
            {
                Console.WriteLine("No exam created.");
                return;
            }

            Console.WriteLine("\nPress Enter to start the exam...");
            Console.ReadLine();

            subject.Exam.ShowExam();

            Console.WriteLine("\n=== End ===");
        }
    }
}
