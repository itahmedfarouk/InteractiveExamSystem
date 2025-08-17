# ExamSystemCSharp 🧠🎯
[![.NET](https://img.shields.io/badge/.NET-8.0-512bd4)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20Linux%20%7C%20macOS-informational)](#)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Status](https://img.shields.io/badge/Status-Active-success)](#)

> A clean, **console-based Examination System** in C# showcasing **OOP** fundamentals (Inheritance, Polymorphism, Encapsulation, Abstraction) with a fully **interactive exam builder**.

---

## 🔥 Highlights
- **Two exam types**  
  - **Final Exam** → True/False + MCQ, shows full review (questions, answers, and grade)  
  - **Practical Exam** → MCQ only, shows correct answers after submission
- **Interactive builder**: choose exam type, set time (30–180 mins), set questions count, pick type per question (TF/MCQ), enter text/options/correct answer, set mark
- **Validation** for inputs & smooth UX with screen clearing
- **Interfaces**: `ICloneable`, `IComparable` + `ToString()` overrides
- **Extensible** architecture: add new Question/Exam types easily

---

## 🚀 Quick Start
```bash
# .NET 8 SDK required
dotnet build
dotnet run
```
Workflow:
1) Choose **Exam Type** (Final / Practical)  
2) Enter **Exam Time** (30–180 minutes)  
3) Enter **Number of Questions**  
4) For each question:
   - Pick **Type** (TF/MCQ) — Practical forces MCQ
   - Enter **Question Text**
   - If MCQ: enter **4 choices** (1..4) + **correct index**
   - If TF: choose **True** or **False**
   - Enter **Mark**
5) Press **Enter** to start the exam  
6) Answer interactively → see results

---

## 🧩 Project Structure
```
ExamSystemCSharp/
├─ Program.cs
├─ Domain/
│  ├─ Answer.cs
│  └─ Subject.cs
├─ Questions/
│  ├─ Question.cs
│  ├─ TrueFalseQuestion.cs
│  └─ MCQQuestion.cs
└─ Exams/
   ├─ Exam.cs
   ├─ FinalExam.cs
   └─ PracticalExam.cs
```

---

## 🏗️ OOP Design (Class Diagram – ASCII)
```
                +--------------------+
                |      Subject       |
                |--------------------|
                | Id, Name, Exam     |
                | CreateExamInteractive()
                +----------+---------+
                           |
                           v
                +--------------------+
                |        Exam        |<<abstract>>
                |--------------------|
                | TimeOfExam         |
                | Questions          |
                | ShowExam()         |<-- polymorphic
                +----+----------+----+
                     |          |
          +----------+          +-----------+
          |                                 |
+--------------------+             +---------------------+
|     FinalExam      |             |    PracticalExam    |
|--------------------|             |---------------------|
| ShowExam(): review |             | ShowExam(): answers |
+--------------------+             +---------------------+

+--------------------+    inherits     +--------------------+
|      Question      |<----------------|  TrueFalseQuestion |
|--------------------|                 +--------------------+
| Header, Body, Mark |    inherits     +--------------------+
| AnswerList, Right  |<----------------|     MCQQuestion    |
| AskAndGet...()     |                 +--------------------+

+--------------------+
|       Answer       |
|--------------------|
| AnswerId, Text     |
+--------------------+
```

---

## 🧪 Manual Test Scenarios
- **Final / 2 questions**: Q1 TF (True), Q2 MCQ (correct=3), marks 5+10 → shows detailed review + grade  
- **Practical / 1 question**: MCQ only, correct=2 → shows correct answers after finish  
- **Invalid Inputs**: time=500 → re-prompt; correct choice=5 → re-prompt; empty text → re-prompt

---

## 🧱 Extensibility Guide
**Add a new Question type (e.g., Multi-Select):**
1. Create `MultiSelectQuestion : Question`
2. Override `AskAndGetStudentAnswer()` to collect multiple indices
3. Integrate creation flow in `Subject.CreateExamInteractive()`

**Add a new Exam type (e.g., TimedReviewExam):**
1. Create `TimedReviewExam : Exam`
2. Implement a custom `ShowExam()` (e.g., hide answers, delay review)
3. Add option in subject builder

---

## 🛠️ Troubleshooting
**“Could not copy apphost.exe… file is locked”**  
- Stop the running app (close console or **Shift+F5**)  
- Kill processes if needed:
  ```powershell
  taskkill /F /IM ExamCsharp.exe
  taskkill /F /IM dotnet.exe
  ```
- Clean and rebuild: **Build → Clean Solution**, then **Rebuild**  
- If Hot Reload complains (“requires restart”), stop debugging and start again

**Obsolete `string.Copy` warnings**  
- Strings are immutable; just pass the string as-is in `Clone()`.

---

## 📦 .gitignore (Recommended)
Create a `.gitignore` with Visual Studio template to exclude:  
`bin/`, `obj/`, `.vs/`, `*.user`, `*.suo`, `TestResults/`, `*.DS_Store`

---

## 🧰 Tech Stack
- **Language:** C#  
- **Runtime:** .NET 8  
- **Type:** Console Application  
- **Paradigm:** OOP (Inheritance, Polymorphism, Abstraction, Encapsulation)  
- **Interfaces:** `ICloneable`, `IComparable`

---

## 📅 Roadmap
- [ ] Export/Import exams to **JSON**
- [ ] Add **question pools** and randomization
- [ ] Timer countdown during exam
- [ ] Multi-select question type
- [ ] Localization (EN/AR prompts switch)
- [ ] Unit tests (xUnit) and CI pipeline (GitHub Actions)

---

## 🤝 Contributing
PRs are welcome! Please open an issue first to discuss major changes.

---

## 📜 License
MIT — do whatever you want, just keep the license header.

---

## 🙌 Credits
Built with ❤️ to demonstrate solid OOP design in C# and a smooth interactive console UX.
