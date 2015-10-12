using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.Grab {
    /// <summary>
    /// Weekly 24 - mini challenges
    /// Grab by adrian17
    /// https://www.reddit.com/r/dailyprogrammer/comments/3o4tpz/weekly_24_mini_challenges/cvu1763
    /// 
    /// Input - a single line of text and a file name. You can take input via command line arguments or stdin, whichever is easier for you. You can also just take a single word instead of a line.
    /// Output - all lines of the checked file which contain this piece of text, along with line numbers. Make it work case-insensitive.
    /// <example>
    /// <para>Example:</para>
    /// <para>$ grab mark file.txt</para>
    /// <para>7: Oh hi mark.</para>
    /// <para>15: Mark frowned.</para>
    /// </example>
    /// 
    /// Extra - Make a second program (or modify the first one to do it when no filename was given) that, instead of checking a single file, does it for all text files in the current directory. When showing matching lines, also show the file you've found it in.
    /// <example>
    /// <para>Example:</para>
    /// <para>$ grab mark</para>
    /// <para>the_room.txt: 7: Oh hi mark.</para>
    /// <para>the_room.txt: 15: Mark frowned.</para>
    /// <para>story.txt: 127: Markings on the wall.</para>
    /// </example>
    /// </summary>
    class GrabProgram {

        static void Main(string[] args) {
            // TODO: Separate output printing and data collecting.
            // TODO: Pull separate functionalities to own classes.

            GrabArguments arguments;
            List<string> filePaths;
            try {
                arguments = ParseGrabArguments(args);
                filePaths = ParseTargetFiles(arguments.TargetFilePath);
            } catch (ArgumentException ae) {
                PrintUsage(ae.Message);
                return;
            }

            try {
                SearchForFileContentMatch(arguments, filePaths);
            } catch (Exception e) {
                Console.WriteLine("ERROR: {0}", e);
            }
        }

        private static GrabArguments ParseGrabArguments(string[] args) {
            if (args.Length == 0) { throw new ArgumentException("No arguments provided."); }

            Encoding encoding;
            int phraseIndex;
            if (EncodingProvided(args[0], out encoding)) {
                if (args.Length > 1) {
                    phraseIndex = 1;
                } else {
                    throw new ArgumentException("Search phrase missing.");
                }
            } else {
                phraseIndex = 0;
            }
            string searchedPhrase = args[phraseIndex];

            string targetFilePath = null;
            int filePathIndex = phraseIndex + 1;
            if (args.Length > filePathIndex) {
                targetFilePath = args[filePathIndex];
            }

            var arguments = new GrabArguments(searchedPhrase, targetFilePath, encoding);
            return arguments;
        }

        private static bool EncodingProvided(string argument, out Encoding encoding) {
            if (argument.StartsWith("-e") && argument.Contains("=")) {
                string[] split = argument.Split('=');
                if (split.Length > 1) {
                    string encodingName = split[1];
                    try {
                        encoding = Encoding.GetEncoding(encodingName);
                        return true;
                    } catch (ArgumentException ae) {
                        throw new ArgumentException(String.Format("Unknown encoding '{0}'.", encodingName), ae);
                    }
                }
            }
            encoding = Encoding.UTF8;
            return false;
        }

        private static List<string> ParseTargetFiles(string targetFilePath) {
            var filePaths = new List<string>();

            if (String.IsNullOrWhiteSpace(targetFilePath)) {
                foreach (var textFilePath in Directory.GetFiles(".", "*.txt")) {
                    filePaths.Add(textFilePath);
                }
            } else if (File.Exists(targetFilePath)) {
                FileAttributes attributes;
                try {
                    attributes = File.GetAttributes(targetFilePath);
                } catch (Exception e) {
                    throw new ArgumentNullException("Failed to check attributes of provided file.", e);
                }
                if (IsFile(attributes)) {
                    filePaths.Add(Path.GetFullPath(targetFilePath));
                } else {
                    throw new ArgumentException("Provided file path needs to point to a file.");
                }

            } else {
                throw new ArgumentException(String.Format("Provided '{0}' file does not exist.", targetFilePath));
            }
            return filePaths;
        }

        private static bool IsFile(FileAttributes attributes) {
            return !((attributes & FileAttributes.Directory) == FileAttributes.Directory);
        }

        private static bool LineContainsSearchPhrase(string line, string searchedPhrase) {
            bool match = line.IndexOf(searchedPhrase, StringComparison.InvariantCultureIgnoreCase) >= 0;
            return match;
        }

        private static void SearchForFileContentMatch(GrabArguments arguments, List<string> filePaths) {
            bool filePathProvided = !String.IsNullOrWhiteSpace(arguments.TargetFilePath) && filePaths.Count == 1;
            foreach (var filePath in filePaths) {
                using (var textStream = new StreamReader(File.OpenRead(filePath), arguments.Encoding)) {
                    string line;
                    int lineCount = 0;
                    while ((line = textStream.ReadLine()) != null) {
                        lineCount++;
                        if (!LineContainsSearchPhrase(line, arguments.SearchPhrase)) continue;

                        if (filePathProvided) {
                            Console.WriteLine("{0}: {1}", lineCount, line);
                        } else {
                            string fileName = Path.GetFileName(filePath);
                            Console.WriteLine("{0}: {1}: {2}", fileName, lineCount, line);
                        }
                    }
                }
            }
        }

        private static void PrintUsage(string exceptionMsg) {
            Console.WriteLine(exceptionMsg);
            Console.WriteLine("Usage: grab.exe [-e=encoding_name] searched_phrase [target_file]");
            Console.WriteLine();

            Console.WriteLine("encoding_name:");
            Console.WriteLine("--------------");
            Console.WriteLine("UTF-8 is used if param not provided.");
            Console.WriteLine("Encoding options:");
            Console.WriteLine(String.Join(", ", Encoding.GetEncodings().Select(e => e.Name)));
            Console.WriteLine();

            Console.WriteLine("searched_phrase:");
            Console.WriteLine("----------------");
            Console.WriteLine("Phrase that is searched from file contents. Quotation marks are required if the phrase contains spaces.");
            Console.WriteLine();

            Console.WriteLine("target_file:");
            Console.WriteLine("------------");
            Console.WriteLine("Complete or related file path. Quotation marks required if path contains spaces. Search is done on all files in the executable folder if no target file is provided.");
            Console.WriteLine();
        }
    }
}
