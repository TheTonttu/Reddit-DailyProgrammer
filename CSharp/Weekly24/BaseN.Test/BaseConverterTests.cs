using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN;

namespace BaseN.Test {
    public class BaseConverterTests {

        private static readonly object[] Source_BaseConverter_ConvertsCorrectly = {
            new object[] {0, 2, new Number(new List<int> { 0 }, 2)},
            new object[] {0, 10, new Number(new List<int> { 0 }, 10)},
            new object[] {1, 2, new Number(new List<int> { 1 }, 2)},
            new object[] {1, 10, new Number(new List<int> { 1 }, 10)},
            new object[] {10, 2, new Number(new List<int> { 1, 0, 1, 0 }, 2)},
            new object[] {10, 3, new Number(new List<int> { 1, 0, 1 }, 3)},
            new object[] {10, 4, new Number(new List<int> { 2, 2 }, 4)},
            new object[] {10, 5, new Number(new List<int> { 2, 0 }, 5)},
            new object[] {10, 6, new Number(new List<int> { 1, 4 }, 6)},
            new object[] {10, 7, new Number(new List<int> { 1, 3 }, 7)},
            new object[] {10, 8, new Number(new List<int> { 1, 2 }, 8)},
            new object[] {10, 9, new Number(new List<int> { 1, 1 }, 9)},
            new object[] {10, 10, new Number(new List<int> { 1, 0 }, 10)},
            new object[] {100, 2, new Number(new List<int> { 1, 1, 0, 0, 1, 0, 0 }, 2)},
            new object[] {100, 5, new Number(new List<int> { 4, 0, 0 }, 5)},
            new object[] {100, 10, new Number(new List<int> { 1, 0, 0 }, 10)},
            new object[] {1000, 2, new Number(new List<int> { 1, 1, 1, 1, 1, 0, 1, 0, 0, 0 }, 2)},
            new object[] {1000, 5, new Number(new List<int> { 1, 3, 0, 0, 0 }, 5)},
            new object[] {1000, 10, new Number(new List<int> { 1, 0, 0, 0 }, 10)},
            new object[] {9999, 10,  new Number(new List<int> { 9, 9, 9, 9 }, 10)},
            new object[] {999999999999, 10, new Number(new List<int> { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, 10)},
            new object[] {16, 16, new Number(new List<int> { 1, 0 }, 16)},
            new object[] {35, 36, new Number(new List<int> { 35 }, 36)},
            new object[] {36, 36, new Number(new List<int> { 1, 0 }, 36)},
            new object[] {-1, 2, new Number(new List<int> {  1 }, 2, true)},
            new object[] {-36, 36, new Number(new List<int> { 1, 0 }, 36, true)}
        };

        [Test, TestCaseSource("Source_BaseConverter_ConvertsCorrectly")]
        public void BaseConverter_ConvertsCorrectly(long number, int baseNum, Number expectedResult) {
            Number result = BaseConverter.Convert(number, baseNum);

            Assert.AreEqual(expectedResult.Digits, result.Digits, "Digits diff");
            Assert.AreEqual(expectedResult.Base, result.Base, "Base diff");
            Assert.AreEqual(expectedResult.IsNegative, result.IsNegative, "Negative diff");
        }
    }
}
