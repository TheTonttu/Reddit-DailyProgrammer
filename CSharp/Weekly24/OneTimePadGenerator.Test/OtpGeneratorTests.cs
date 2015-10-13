using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.OneTimePadGenerator.Test {
    public class OtpGeneratorTests {

        private const int BATCH_SIZE = 50;
        private const int MAX_DIFF_CHARS_COUNT = 26;

        private static readonly HashSet<char> AllowedChars = new HashSet<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());

        [Test]
        [Timeout(250)]
        public void OtpGenerator_GeneratesCharsAToZ() {
            var otpGenerator = new OtpGenerator();
            var genSet = new HashSet<char>();
            while (genSet.Count < MAX_DIFF_CHARS_COUNT) {
                foreach (char c in otpGenerator.Generate(BATCH_SIZE)) {
                    if (!AllowedChars.Contains(c)) {
                        Assert.Fail("Generated disallowed char: {0}", c);
                    }
                    genSet.Add(c);
                }
            }

            if (genSet.Count > MAX_DIFF_CHARS_COUNT) {
                Assert.Fail("Generated too many diff characters: gen {0} vs max {0}", genSet.Count, MAX_DIFF_CHARS_COUNT);
            }
            foreach (char c in AllowedChars) {
                if (!genSet.Contains(c)) {
                    Assert.Fail("{0} was not generated", c);
                }
            }
        }
    }
}
