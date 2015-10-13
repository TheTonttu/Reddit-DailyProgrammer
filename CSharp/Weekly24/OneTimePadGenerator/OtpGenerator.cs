using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.OneTimePadGenerator {
    public class OtpGenerator {
        // A-Z count
        private const int LIMIT = 26;
        // A = 65
        private const int ASCII_UPPER_A = 65;

        private readonly static Random Random = new Random();

        public string Generate(int length) {
            if (length == 0) return String.Empty;

            var otpBuilder = new StringBuilder();
            for (int i = 0; i < length; i++) {
                otpBuilder.Append(GenRandomChar());
            }
            return otpBuilder.ToString();
        }

        private static char GenRandomChar() {
            int randomNumber = Random.Next(LIMIT);
            char randomChar = (char)(ASCII_UPPER_A + randomNumber);
            return randomChar;
        }
    }
}
