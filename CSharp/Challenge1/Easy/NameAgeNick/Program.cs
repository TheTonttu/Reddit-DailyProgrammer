using System;
using System.IO;
using System.Text;

namespace NameAgeNick {
    class Program {

        delegate bool CheckAnswer(string answer);

        static void Main(string[] args) {
            string name = AskQuestion("What is your name?");
            string age = AskQuestion("How old are you?", IsPossibleAge);
            string nick = AskQuestion("What is your Reddit username?");

            var pluralisation = Int32.Parse(age) != 1 ? "s" : "";
            var conclusion = $"Your name is {name}, you are {age} year{pluralisation} old, and your username is {nick}.";
            Console.WriteLine(conclusion);
            LogConclusion(conclusion);
            Console.WriteLine();

            Console.WriteLine("Thank you come again!");
            Console.ReadKey();
        }

        private static string AskQuestion(string question, CheckAnswer isAnswerAcceptable = null) {
            if (isAnswerAcceptable == null) {
                isAnswerAcceptable = YesMan;
            }

            string answer = null;
            while (answer == null) {
                Console.WriteLine(question);
                answer = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(answer) || !isAnswerAcceptable(answer)) {
                    Console.Clear();
                    Console.WriteLine("Riiiight...");
                    answer = null;
                }
            }
            Console.Clear();

            return answer.Trim();
        }

        private static bool IsPossibleAge(string answer) {
            if (Int32.TryParse(answer, out int age)) {
                return age > 0;
            }
            return false;
        }

        private static bool YesMan(string answer) {
            return true;
        }

        private static void LogConclusion(string conclusion) {
            var timestamp = DateTime.Now.ToString("s");
            var newline = Environment.NewLine;
            File.AppendAllText("log.txt", $"{timestamp}\t{conclusion}{newline}", Encoding.UTF8);
        }
    }
}
