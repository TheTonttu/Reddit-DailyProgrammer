using System;

namespace Tonttu.Reddit.DailyProgrammer.Challenge1.ReversedGuessNumber {
    /// <summary>
    /// Challenge #1 difficult
    /// Guessing game... roles reversed by nottoobadguy
    /// http://www.reddit.com/r/dailyprogrammer/comments/pii6j/difficult_challenge_1/
    /// 
    /// Description:
    /// We all know the classic "guessing game" with higher or lower prompts. Let's do a role reversal;
    /// You create a program that will guess numbers between 1-100 and respond appropriately based on whether user says that the number is too high or too low.
    /// Try to make a program that can guess your number based on user input and great code!
    /// </summary>
    class Program {
        private static Random Random = new Random();

        static void Main(string[] args) {
            // TODO: Optionally give min and max as arguments.

            GameState gameState = GameState.MainMenu;
            bool isRunning = true;
            int min = 1;
            int max = 101;
            int guessCount = 0;
            while (isRunning) {
                switch (gameState) {
                    case GameState.MainMenu:
                        Console.Clear();
                        char mainMenuInput = HandleMainMenu();
                        gameState = GetNextMainMenuState(gameState, mainMenuInput);
                        break;
                    case GameState.InitializeGame:
                        Console.Clear();
                        min = 1;
                        max = 101;
                        guessCount = 0;
                        AnnounceGameBeginning(min, max);
                        gameState = GameState.GameOn;
                        break;
                    case GameState.GameOn:
                        int guess = Random.Next(min, max);
                        guessCount++;
                        Console.WriteLine("Is the number {0}? [Y]es/Too [L]ow/Too [H]igh/[Q]uit", guess);
                        var input = PromptForInput(InputtedYesOrLowOrHighOrQuit, String.Format("Is the number {0}? Y/L/H/Q", guess));

                        if (InputtedLow(input)) {
                            min = CalcNextMin(max, guess);
                        } else if (InputtedHigh(input)) {
                            max = CalcNextMax(min, guess);
                        } else if (InputtedYes(input)) {
                            Console.WriteLine("YAH-HOO! Mar- I'm number one! It took only {0} {1}.", guessCount, GetWordTry(guessCount));
                            gameState = HandlePlayAgainQuestion(gameState);
                            Console.Clear();
                        } else if (InputtedQuit(input)) {
                            gameState = GameState.QuitGame;
                        }

                        if (IsOneChoiceLeft(min, max)) {
                            string wordTry = GetWordTry(guessCount);
                            Console.WriteLine("The only choice left is {0}! I is winrar after {1} {2}! \\o/", min, guessCount, GetWordTry(guessCount));
                            gameState = HandlePlayAgainQuestion(gameState);
                            Console.Clear();
                        } else if (ChoiceRangeIsInvalid(min, max)) {
                            throw new NotSupportedException(String.Format("Guess range is unexpected: min {0}, max {1}", min, max));
                        }
                        break;
                    case GameState.QuitGame:
                        Console.Clear();
                        Console.WriteLine("Aww... game will now terminate. Please come back soon! :C");
                        isRunning = false;
                        break;
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static string GetWordTry(int guessCount) {
            return guessCount == 1
                ? "try"
                : "tries";
        }

        private static void AnnounceGameBeginning(int min, int max) {
            Console.WriteLine("Aaaaallrighty then! Let's get ready to rumble!");
            Console.WriteLine("Think of a number between (inclusive, smarty pants) {0} and {1}.", min, max - 1);
            Console.WriteLine();
        }

        private static int CalcNextMax(int min, int guess) {
            int nextMax = guess;
            if (nextMax <= min) {
                nextMax = min + 1;
            }
            return nextMax;
        }

        private static int CalcNextMin(int max, int guess) {
            int nextMin = guess + 1;
            if (nextMin >= max) {
                nextMin = max - 1;
            }
            return nextMin;
        }

        private static char HandleMainMenu() {
            Console.WriteLine("Welcome to number guessing game!");
            Console.WriteLine("...roles reversed!");
            Console.WriteLine("The program tries to guess your number and you tell if it's too low or too high. Fun!");
            Console.WriteLine();

            Console.WriteLine("Start the game? [Y]es/[N]o");
            char input = PromptForInput(InputtedYesOrNo, "Start the game? Y/N");
            return input;
        }

        private static GameState HandlePlayAgainQuestion(GameState prevState) {
            char input = PromptForPlayAgain();
            prevState = GetPlayAgainQuestionNextState(prevState, input);
            return prevState;
        }

        private static char PromptForPlayAgain() {
            Console.WriteLine("Play again? [Y]es/[N]o");
            char input = PromptForInput(InputtedYesOrNo, "Play again? Y/N");
            return input;
        }

        private static GameState GetPlayAgainQuestionNextState(GameState prevState, char input) {
            if (InputtedYes(input)) {
                return GameState.InitializeGame;
            } else if (InputtedNo(input)) {
                return GameState.QuitGame;
            }
            throw new NotSupportedException(String.Format("Play again handler encountered unexpected input. state: {0}, input: {1}", prevState, input));
        }

        private static bool IsOneChoiceLeft(int min, int max) {
            return (max - min) == 1;
        }

        private static bool ChoiceRangeIsInvalid(int min, int max) {
            return (max - min) < 1;
        }

        private static GameState GetNextMainMenuState(GameState prevState, char input) {
            if (InputtedYes(input)) {
                return GameState.InitializeGame;
            } else if (InputtedNo(input)) {
                return GameState.QuitGame;
            } else if (InputtedQuit(input)) {
                return GameState.QuitGame;
            }
            throw new NotSupportedException(String.Format("Unexpected input in {0} state", prevState));
        }

        private static char PromptForInput(Func<char, bool> validInputCheck, string repeatMsg) {
            char input = PromptForInput();
            Console.WriteLine();
            while (!validInputCheck(input)) {
                input = PromptForInputAgain(repeatMsg);
            }
            return input;
        }

        private static char PromptForInput() {
            ConsoleKeyInfo inputInfo = Console.ReadKey();
            Console.WriteLine();
            return inputInfo.KeyChar;
        }

        private static char PromptForInputAgain(string prompts) {
            Console.WriteLine("Huh? {0}", prompts);
            char input= PromptForInput();
            Console.WriteLine();
            return input;
        }

        private static bool InputtedYesOrLowOrHighOrQuit(char input) {
            return (InputtedYes(input)
                || InputtedLow(input)
                || InputtedHigh(input)
                || InputtedQuit(input));
        }

        private static bool InputtedYesOrNo(char input) {
            return (InputtedYes(input)
                || InputtedNo(input));
        }

        private static bool InputtedYesOrNoQuit(char input) {
            return (InputtedYes(input)
                || InputtedNo(input)
                || InputtedQuit(input));
        }

        private static bool InputtedNo(char input) {
            return input == 'n' || input == 'N';
        }

        private static bool InputtedYes(char input) {
            return input == 'y' || input == 'Y';
        }

        private static bool InputtedQuit(char input) {
            return input == 'q' || input == 'Q';
        }

        private static bool InputtedLow(char input) {
            return input == 'l' || input == 'L';
        }

        private static bool InputtedHigh(char input) {
            return input == 'h' || input == 'H';
        }
    }

    public enum GameState {
        MainMenu,
        InitializeGame,
        GameOn,
        QuitGame
    }
}
