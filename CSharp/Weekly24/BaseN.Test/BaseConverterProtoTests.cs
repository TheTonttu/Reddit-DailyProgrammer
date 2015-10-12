using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN;

namespace BaseN.Test {
    public class BaseConverterProtoTests {

        private BaseConverterProto converter;

        [TestFixtureSetUp]
        public void TestsSetUp() {
            converter = new BaseConverterProto();
        }

        [TestCase(10, 2, Result = 1010)]
        [TestCase(10, 3, Result = 101)]
        [TestCase(10, 4, Result = 22)]
        [TestCase(10, 5, Result = 20)]
        [TestCase(10, 6, Result = 14)]
        [TestCase(10, 7, Result = 13)]
        [TestCase(10, 8, Result = 12)]
        [TestCase(10, 9, Result = 11)]
        [TestCase(10, 10, Result=10)]
        [TestCase(100, 2, Result=1100100)]
        [TestCase(100, 5, Result = 400)]
        [TestCase(100, 10, Result=100)]
        [TestCase(1000, 2, Result = 1111101000)]
        [TestCase(1000, 5, Result = 13000)]
        [TestCase(1000, 10, Result = 1000)]
        public long ConvertsCorrectly(int number, int toBase) {
            return converter.Convert(number, toBase);
        }
    }
}
