using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.OneTimePadGenerator {
    /// <summary>
    /// One-time pad generator by BaronPaprika
    /// https://www.reddit.com/r/dailyprogrammer/comments/3o4tpz/weekly_24_mini_challenges/cvu1z9b
    /// 
    /// One-time pad generator - generate a one-time-pad for use in encryption
    /// Input - the length of the one-time-pad
    /// Output - (length) characters of random letters
    /// </summary>
    class OtpProgram {
        // Out of memory when int.MaxValue used >_>
        private const int MAX_OTP_LENGTH = ushort.MaxValue;

        static void Main(string[] args) {
            // TODO: Check that plain_message contains only characters
            if (args.Length == 0) {
                PrintUsage("OTP length is required.");
                return;
            }

            int length;
            if (!int.TryParse(args[0], out length) || length > MAX_OTP_LENGTH) {
                PrintUsage(String.Format("Length argument is invalid. Max length is {0:N0}.", MAX_OTP_LENGTH));
                return;
            }

            var otpGenerator = new OtpGenerator();
            string otp = otpGenerator.Generate(length);

            if (args.Length >= 2) {
                var cipher = new VigenereCipher();
                string plainMessage = args[1];
                Console.WriteLine("{0} {1}", otp, cipher.Encrypt(otp, plainMessage));
            } else {
                Console.WriteLine(otp);
            }
        }

        private static void PrintUsage(string errorMsg) {
            Console.WriteLine(errorMsg);
            Console.WriteLine("Usage: otpgenerator.exe len [plain_message]");
            Console.WriteLine("Where len is OTP length. Optional plain_message is encrypted with Vigenère cipher and OTP is used as keyword.");
            Console.WriteLine();
            Console.WriteLine("If plain_message contains other characters than aA-zZ then encrypted message is not quaranteed to be valid.");
            Console.WriteLine();
            Console.WriteLine("Output: OTP [and encrypted message]");
            Console.WriteLine();
        }
    }
}
