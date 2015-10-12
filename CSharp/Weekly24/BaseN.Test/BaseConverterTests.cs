using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN;

namespace BaseN.Test {
    public class BaseConverterTests {

        private static readonly object[] Source_NumberConvertsToBaseDigits = {
            new object[] {0, 2, new Number(new List<int> { 0 }, 2)},
            new object[] {0, 10, new Number(new List<int> { 0 }, 10)},
            new object[] {1, 2, new Number(new List<int> { 1 }, 2)},
            new object[] {1, 10, new Number(new List<int> { 1 }, 10)},
            //[TestCase(10, 2, Result = 1010)]
            new object[] {10, 2, new Number(new List<int> { 1, 0, 1, 0 }, 2)},
            //[TestCase(10, 3, Result = 101)]
            new object[] {10, 3, new Number(new List<int> { 1, 0, 1 }, 3)},
            //[TestCase(10, 4, Result = 22)]
            new object[] {10, 4, new Number(new List<int> { 2, 2 }, 4)},
            //[TestCase(10, 5, Result = 20)]
            new object[] {10, 5, new Number(new List<int> { 2, 0 }, 5)},
            //[TestCase(10, 6, Result = 14)]
            new object[] {10, 6, new Number(new List<int> { 1, 4 }, 6)},
            //[TestCase(10, 7, Result = 13)]
            new object[] {10, 7, new Number(new List<int> { 1, 3 }, 7)},
            //[TestCase(10, 8, Result = 12)]
            new object[] {10, 8, new Number(new List<int> { 1, 2 }, 8)},
            //[TestCase(10, 9, Result = 11)]
            new object[] {10, 9, new Number(new List<int> { 1, 1 }, 9)},
            new object[] {10, 10, new Number(new List<int> { 1, 0 }, 10)},
            //[TestCase(100, 2, Result=1100100)]
            new object[] {100, 2, new Number(new List<int> { 1, 1, 0, 0, 1, 0, 0 }, 2)},
            //[TestCase(100, 5, Result = 400)]
            new object[] {100, 5, new Number(new List<int> { 4, 0, 0 }, 5)},
            new object[] {100, 10, new Number(new List<int> { 1, 0, 0 }, 10)},
            //[TestCase(1000, 2, Result = 1111101000)]
            new object[] {1000, 2, new Number(new List<int> { 1, 1, 1, 1, 1, 0, 1, 0, 0, 0 }, 2)},
            //[TestCase(1000, 5, Result = 13000)]
            new object[] {1000, 5, new Number(new List<int> { 1, 3, 0, 0, 0 }, 5)},
            //[TestCase(1000, 10, Result = 1000)]
            new object[] {1000, 10, new Number(new List<int> { 1, 0, 0, 0 }, 10)},
            new object[] {9999, 10,  new Number(new List<int> { 9, 9, 9, 9 }, 10)},
            new object[] {999999999999, 10, new Number(new List<int> { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, 10)},
            new object[] {16, 16, new Number(new List<int> { 1, 0 }, 16)},
            new object[] {35, 36, new Number(new List<int> { 35 }, 36)},
            new object[] {36, 36, new Number(new List<int> { 1, 0 }, 36)},
            new object[] {-1, 2, new Number(new List<int> {  1 }, 2, true)},
            new object[] {-36, 36, new Number(new List<int> { 1, 0 }, 36, true)}
        };

        [Test, TestCaseSource("Source_NumberConvertsToBaseDigits")]
        public void ConvertsCorrectly(long number, int baseNum, Number expectedResult) {
            Number result = BaseConverter.Convert(number, baseNum);

            Assert.AreEqual(expectedResult.Digits, result.Digits, "Digits diff");
            Assert.AreEqual(expectedResult.Base, result.Base, "Base diff");
            Assert.AreEqual(expectedResult.IsNegative, result.IsNegative, "Negative diff");
        }
    }
}
