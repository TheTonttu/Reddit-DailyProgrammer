using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tonttu.Reddit.DailyProgrammer.Weekly24.RampNumbers;

namespace RampNumbers.Test {
    public class DerpMathTests {

        [TestCase(-10, Result = 2)]
        [TestCase(-1, Result = 1)]
        [TestCase(0, Result = 1)]
        [TestCase(1, Result = 1)]
        [TestCase(9, Result = 1)]
        [TestCase(10, Result = 2)]
        [TestCase(99, Result = 2)]
        [TestCase(100, Result = 3)]
        [TestCase(999, Result = 3)]
        [TestCase(1000, Result = 4)]
        [TestCase(1010101010101010101, Result = 19)]
        public int DerpMath_DigitCountIsCalculatedCorrectly(long number) {
            return DerpMath.DigitCount(number);
        }

        [TestCase(-10, Result = new byte[] { 1, 0 })]
        [TestCase(-1, Result = new byte[] { 1 })]
        [TestCase(0, Result = new byte[] { 0 })]
        [TestCase(1, Result = new byte[] { 1 })]
        [TestCase(9, Result = new byte[] { 9 })]
        [TestCase(10, Result = new byte[] { 1, 0 })]
        [TestCase(100, Result = new byte[] { 1, 0, 0 })]
        public byte[] DerpMath_DigitsExtracted(long number) {
            return DerpMath.DigitsArray(number);
        }

        [TestCase(-11, Result = true)]
        [TestCase(-10, Result = false)]
        [TestCase(0, Result = true)]
        [TestCase(1, Result = true)]
        [TestCase(10, Result = false)]
        [TestCase(11, Result = true)]
        [TestCase(21, Result = false)]
        [TestCase(112, Result = true)]
        [TestCase(123, Result = true)]
        [TestCase(321, Result = false)]
        [TestCase(7357, Result = false)]
        [TestCase(1223445, Result = true)]
        [TestCase(1223435, Result = false)]
        [TestCase(1223453, Result = false)]
        public bool DerpMath_RampNumberCheckIsCorrect(long number) {
            bool isRampNumber = DerpMath.IsRampNumber(number);
            bool isRampNumberFaster = DerpMath.IsRampNumberFaster(number);
            bool isRampNumberStrManip = DerpMath.IsRampNumberStringManipulation(number);
            Assert.AreEqual(isRampNumber, isRampNumberFaster, "Faster failed");
            Assert.AreEqual(isRampNumber, isRampNumberStrManip, "String manip failed");

            return isRampNumber;
        }
    }
}
