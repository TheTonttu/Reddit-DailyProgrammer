using System;
using System.Collections.Generic;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN {
    public class BaseConverter {
        public static Number Convert(long number, int toBase) {
            if (number == 0) return new Number(new List<int> { 0 }, toBase, false);
            Stack<int> digitStack = new Stack<int>();

            bool isNegative = number < 0;
            long remnants = Math.Abs(number);
            while (remnants > 0) {
                int digit = (int)(remnants % toBase);
                remnants /= toBase;
                digitStack.Push(digit);
            }

            List<int> digits = new List<int>();
            while (digitStack.Count > 0) {
                digits.Add(digitStack.Pop());
            }
            return new Number(digits, toBase, isNegative);
        }

        // TODO: Convert from base n to base m
    }
}
