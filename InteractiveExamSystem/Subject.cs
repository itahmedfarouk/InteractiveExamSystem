using System;
using System.Linq;
using ExamSystem.Exams;
using ExamSystem.Questions;

namespace ExamSystem.Domain
{
    public class Subject : ICloneable, IComparable<Subject>
    {
        public int SubjectId { get; }
        public string SubjectName { get; }
        public Exam? Exam { get; private set; }

        public Subject(int id) : this(id, "Untitled Subject") { }
        public Subject(int id, string name)
        {
            SubjectId = id;
            SubjectName = name ?? "Untitled Subject";
        }

        public void CreateExamInteractive()
        {
            int examType = ReadChoice("Choose exam type: (1) Final  (2) Practical : ", new[] { 1, 2 });
            int minutes = ReadIntInRange("Enter exam time in minutes (30-180): ", 30, 180);
            int totalQuestions = ReadIntInRange("Enter number of questions: ", 1, 1000);

            Exam = (examType == 1)
                ? new FinalExam(TimeSpan.FromMinutes(minutes))
                : new PracticalExam(TimeSpan.FromMinutes(minutes));

            Console.Clear();

            for (int i = 1; i <= totalQuestions; i++)
            {
                Console.WriteLine($"Adding Question {i}/{totalQuestions}");

                var kind = examType == 2
                    ? QuestionKind.MCQ
                    : ReadQuestionKind("Choose question type: (1) True/False  (2) MCQ : ");

                Console.Clear();
                Console.WriteLine($"Adding Question {i}/{totalQuestions}");

                string body = ReadNonEmpty("Enter question text: ");

                Questions.Question q;
                if (kind == QuestionKind.MCQ)
                {
                    string[] choices = new string[4];
                    for (int c = 0; c < 4; c++)
                        choices[c] = ReadNonEmpty($"Enter choice {(c + 1)}: ");
                    int rightIdx = ReadIntInRange("Enter correct choice (1-4): ", 1, 4);
                    q = new MCQQuestion("MCQ", body, mark: 0, choices: choices, rightAnswerIndex1Based: rightIdx);
                }
                else
                {
                    int tf = ReadChoice("Correct answer: (1) True  (2) False : ", new[] { 1, 2 });
                    bool rightIsTrue = (tf == 1);
                    q = new TrueFalseQuestion("True/False", body, mark: 0, rightIsTrue: rightIsTrue);
                }

                int mark = ReadIntInRange("Enter question mark: ", 1, 1000);
                q = RebuildWithMark(q, mark);

                Exam!.AddQuestion(q);
                Console.Clear();
            }
        }


        private static int ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
                Console.WriteLine($"Enter a valid number in range [{min}-{max}].");
            }
        }

        private static int ReadChoice(string prompt, int[] allowed)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out int v) && allowed.Contains(v)) return v;
                Console.WriteLine("Invalid choice.");
            }
        }

        private static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                Console.WriteLine("Text cannot be empty.");
            }
        }

        private enum QuestionKind { TF, MCQ }

        private static QuestionKind ReadQuestionKind(string prompt)
        {
            int v = ReadChoice(prompt, new[] { 1, 2 });
            return v == 1 ? QuestionKind.TF : QuestionKind.MCQ;
        }

        private static Questions.Question RebuildWithMark(Questions.Question q, int mark)
        {
            if (q is TrueFalseQuestion tfq)
            {
                bool rightIsTrue = tfq.RightAnswerId == 1;
                return new TrueFalseQuestion(tfq.Header, tfq.Body, mark, rightIsTrue);
            }
            else if (q is MCQQuestion mcq)
            {
                var choices = mcq.AnswerList.Select(a => a.AnswerText).ToArray();
                int right = mcq.RightAnswerId;
                return new MCQQuestion(mcq.Header, mcq.Body, mark, choices, right);
            }
            return q;
        }

        public override string ToString()
            => $"Subject[{SubjectId}] {SubjectName} | Exam: {Exam?.GetType().Name ?? "None"}";

        public object Clone()
        {
            var copy = new Subject(SubjectId, string.Copy(SubjectName));
            copy.Exam = (Exam?)Exam?.Clone();
            return copy;
        }

        public int CompareTo(Subject? other)
            => other is null ? 1 : string.Compare(SubjectName, other.SubjectName, StringComparison.OrdinalIgnoreCase);
    }
}
