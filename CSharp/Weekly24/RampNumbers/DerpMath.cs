using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.RampNumbers {
    public class DerpMath {
        public static int DigitCount(long number) {
            //int length = number < 0 ? number.ToString().Length - 1 : number.ToString().Length;
            if (number == 0) return 1;

            long positiveNumber = Math.Abs(number);
            return (int)Math.Floor(Math.Log10(positiveNumber) + 1);
        }

        public static byte[] DigitsArray(long number) {
            int length = DerpMath.DigitCount(number);
            long positiveNumber = Math.Abs(number);

            var digits = new byte[length];
            long processedNumber = positiveNumber;
            int digitIndex = 0;
            for (int i = 0; i < length; i++) {
                digitIndex = (length-1) - i;
                byte digit = (byte)(processedNumber % 10);
                digits[digitIndex] = digit;
                processedNumber /= 10;
            }
            return digits;
        }

        public static bool IsRampNumber(long number) {
            var digits = DerpMath.DigitsArray(number);
            // Iterate only to the second last digit as we need to compare it to the next digit.
            int checkIndex = digits.Length - 1;
            for (int i = 0; i < checkIndex; i++) {
                byte currDigit = digits[i];
                byte nextDigit = digits[i + 1];
                if (currDigit > nextDigit) {
                    return false;
                }
            }
            return true;
        }

        public static bool IsRampNumberStringManipulation(long number) {
            var digits = number.ToString().ToCharArray();

            int checkIndex = digits.Length - 1;
            for (int i = 0; i < checkIndex; i++) {
                char currDigit = digits[i];
                char nextDigit = digits[i + 1];
                if (currDigit > nextDigit) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsRampNumberFaster(long number) {
            // Combines digit counting and digit comparison.
            
            long positiveNumber = Math.Abs(number);
            if (positiveNumber <= 9) return true;

            long processedNumber = positiveNumber;
            byte prevDigit = (byte)(processedNumber % 10);
            processedNumber /= 10;
            while (processedNumber > 0) {
                byte currDigit = (byte)(processedNumber % 10);
                if (currDigit > prevDigit) {
                    return false;
                }
                prevDigit = currDigit;
                processedNumber /= 10;
            }

            return true;
        }
    }
}
