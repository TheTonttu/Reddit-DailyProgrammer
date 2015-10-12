using System;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN {
    public class BaseNArgumentParser {
        private const int MIN_BASE = 2;
        private readonly int _maxBase;

        public BaseNArgumentParser(int maxBase) {
            _maxBase = maxBase;
        }

        public BaseNArguments ParseArgs(string[] args) {
            if (args.Length < 2) {
                throw new ArgumentException("All required arguments were not provided.");
            }
            int num;
            if (!int.TryParse(args[0], out num)) {
                throw new ArgumentException("Number provided is not valid or too large.");
            }
            int baseNum;
            if (!int.TryParse(args[1], out baseNum) || !IsBaseInRange(baseNum)) {
                throw new ArgumentException("Provided base is out of range.");
            }

            return new BaseNArguments(num, baseNum);
        }

        private bool IsBaseInRange(int baseNum) {
            return MIN_BASE <= baseNum && baseNum <= _maxBase;
        }
    }
}
