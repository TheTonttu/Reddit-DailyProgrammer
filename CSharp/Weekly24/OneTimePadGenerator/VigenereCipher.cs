using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.OneTimePadGenerator {
    public class VigenereCipher {
        // A = 65
        private const int ASCII_UPPER_A = 65;
        // Z = 90 = A + (count - 1)
        private const int ASCII_UPPER_Z = 90;
        // A-Z count
        private const int ASCII_UPPERCOUNT = 26;

        public string Encrypt(string keyword, string plainMessage) {
            if (String.IsNullOrEmpty(plainMessage)) return String.Empty;

            string paddedKeyword = PadKeyword(keyword, plainMessage.Length);
            string upperMsg = plainMessage.ToUpperInvariant();

            var msgBuilder = new StringBuilder();
            for (int i = 0; i < paddedKeyword.Length; i++) {
                char keyChar = paddedKeyword[i];
                char plainChar = upperMsg[i];

                char encryptedChar = EncryptCharacter(keyChar, plainChar);
                msgBuilder.Append(encryptedChar);
            }

            return msgBuilder.ToString();
        }

        public string Decrypt(string keyword, string encryptedMessage) {
            if (String.IsNullOrEmpty(encryptedMessage)) return String.Empty;

            string paddedKeyword = PadKeyword(keyword, encryptedMessage.Length);
            string upperMsg = encryptedMessage.ToUpperInvariant();

            var msgBuilder = new StringBuilder();
            for (int i = 0; i < paddedKeyword.Length; i++) {
                char keyChar = paddedKeyword[i];
                char encryptedChar = upperMsg[i];

                char decryptedChar = DecryptCharacter(keyChar, encryptedChar);
                msgBuilder.Append(decryptedChar);
            }

            return msgBuilder.ToString();
        }

        public static string PadKeyword(string keyword, int msgLength) {
            if (String.IsNullOrEmpty(keyword) || msgLength <= 0) {
                return String.Empty;
            }

            string upperKeyword = keyword.ToUpperInvariant();
            if (keyword.Length == msgLength) return upperKeyword;

            var padBuilder = new StringBuilder();
            int iterLimit;
            if (msgLength > upperKeyword.Length) {
                padBuilder.Append(upperKeyword);
                iterLimit = msgLength - upperKeyword.Length;
            } else if (msgLength < upperKeyword.Length) {
                return upperKeyword.Substring(0, msgLength);
            } else {
                iterLimit = msgLength;
            }

            for (int i = 0; i < iterLimit; i++) {
                int keywordIndex = i % upperKeyword.Length;
                char nextChar = upperKeyword[keywordIndex];
                padBuilder.Append(nextChar);
            }
            return padBuilder.ToString();
        }

        private static char EncryptCharacter(char keyChar, char plainChar) {
            int keyOffset = CalcKeyOffset(keyChar);
            int offsetMsgCharVal = (int)plainChar + keyOffset;
            if (offsetMsgCharVal > ASCII_UPPER_Z) {
                offsetMsgCharVal -= ASCII_UPPERCOUNT;
            }
            char encryptedChar = (char)offsetMsgCharVal;
            return encryptedChar;
        }

        private static char DecryptCharacter(char keyChar, char encryptedChar) {
            int keyOffset = CalcKeyOffset(keyChar);
            int offsetEncryptCharVal = (int)encryptedChar - keyOffset;
            if (offsetEncryptCharVal < ASCII_UPPER_A) {
                offsetEncryptCharVal += ASCII_UPPERCOUNT;
            }
            char decryptedChar = (char)offsetEncryptCharVal;
            return decryptedChar;
        }

        private static int CalcKeyOffset(char keyChar) {
            int keyOffset = ((int)keyChar - ASCII_UPPER_A);
            return keyOffset;
        }
    }
}
