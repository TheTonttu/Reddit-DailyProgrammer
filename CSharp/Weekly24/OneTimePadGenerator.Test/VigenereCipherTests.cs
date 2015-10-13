using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.OneTimePadGenerator.Test {
    public class VigenereCipherTests {

        private VigenereCipher cipher;

        [TestFixtureSetUp]
        public void SetUpTests() {
            cipher = new VigenereCipher();
        }

        [TestCase("A", "ABC", Result="ABC")]
        [TestCase("AB", "ABC", Result="ACC")]
        [TestCase("ABC", "ABC", Result="ACE")]
        [TestCase("ABCD", "ABC", Result="ACE")]
        [TestCase("XYZ", "ABC", Result="XZB")]
        [TestCase("ZZZ", "ABC", Result = "ZAB")]
        [TestCase("LEMON", "ATTACKATDAWN", Result="LXFOPVEFRNHR")]
        [TestCase("ZZZZZZ", "ABC", Result = "ZAB")]
        // lowercase to uppercase
        [TestCase("a", "abc", Result = "ABC")]
        [TestCase("LEMON", "attackatdawn", Result = "LXFOPVEFRNHR")]
        [TestCase("zzz", "abc", Result = "ZAB")]
        // unexpected
        [TestCase("", "ABC", Result = "")]
        [TestCase("", "", Result = "")]
        [TestCase("ABC", "", Result = "")]
        [TestCase(null, "ABC", Result = "")]
        [TestCase(null, null, Result = "")]
        [TestCase("ABC", null, Result = "")]
        public string VigenereCipher_EncryptsCorrectly(string keyword, string plainMessage) {
            return cipher.Encrypt(keyword, plainMessage);
        }

        [TestCase("A", "ABC", Result="ABC")]
        [TestCase("AB", "ACC", Result="ABC")]
        [TestCase("ABC", "ACE", Result="ABC")]
        [TestCase("ABCD", "ACE", Result="ABC")]
        [TestCase("XYZ", "XZB", Result="ABC")]
        [TestCase("ZZZ", "ZAB", Result = "ABC")]
        [TestCase("LEMON", "LXFOPVEFRNHR", Result = "ATTACKATDAWN")]
        [TestCase("ZZZZZZ", "ZAB", Result = "ABC")]
        // lowercase to uppercase
        [TestCase("a", "abc", Result = "ABC")]
        [TestCase("LEMON", "lxfopvefrnhr", Result = "ATTACKATDAWN")]
        [TestCase("zzz", "zab", Result = "ABC")]
        // unexpected
        [TestCase("", "ABC", Result = "")]
        [TestCase("", "", Result = "")]
        [TestCase("ABC", "", Result = "")]
        [TestCase(null, "ABC", Result = "")]
        [TestCase(null, null, Result = "")]
        [TestCase("ABC", null, Result = "")]
        public string VigenereCipher_DecryptsCorrectly(string keyword, string encryptedMessage) {
            return cipher.Decrypt(keyword, encryptedMessage);
        }

        [TestCase("", 6, Result = "")]
        [TestCase("A", 0, Result = "")]
        [TestCase("A", 3, Result="AAA")]
        [TestCase("ABC", 3, Result="ABC")]
        [TestCase("ABC", 6, Result="ABCABC")]
        [TestCase("XYZABC", 3, Result="XYZ")]
        [TestCase("XYZABC", 9, Result="XYZABCXYZ")]
        // lowercase to uppercase
        [TestCase("a", 3, Result = "AAA")]
        [TestCase("xyzabc", 3, Result = "XYZ")]
        [TestCase("xyzabc", 9, Result = "XYZABCXYZ")]
        // unexpected
        [TestCase("ABC", -1, Result = "")]
        [TestCase(null, 0, Result = "")]
        [TestCase(null, 3, Result = "")]
        public string VigenereCipher_PadKeywordCorrectly(string keyword, int msgLength) {
            return VigenereCipher.PadKeyword(keyword, msgLength);
        }
    }
}
