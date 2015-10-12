using System;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN {
    public class BaseConverterProto {
        // Doesn't work very well with long numbers.

        public long Convert(int number, int toBase) {
            return Convert(number, 10, toBase);
        }

        private long Convert(int number, int fromBase, int toBase) {
            if (fromBase == toBase) return number;
            if (fromBase != 10) throw new NotImplementedException("Only base10 conversion supported atm.");

            int remnants = number;
            long multiplier = 1;
            long result = 0;
            while (remnants > 0) {
                int remainder = remnants % toBase;
                remnants /= toBase;

                result += multiplier * remainder;
                multiplier *= 10;
            }

            return result;
        }
    }
}
