using System;

namespace ExamSystem.Domain
{
    public class Answer : ICloneable, IComparable<Answer>
    {
        public int AnswerId { get; }
        public string AnswerText { get; }

        public Answer(int id) : this(id, string.Empty) { }

        public Answer(int id, string text)
        {
            AnswerId = id;
            AnswerText = text ?? string.Empty;
        }

        public override string ToString() => $"{AnswerId}. {AnswerText}";
        public object Clone() => new Answer(AnswerId, string.Copy(AnswerText));
        public int CompareTo(Answer? other) => other is null ? 1 : AnswerId.CompareTo(other.AnswerId);
    }
}