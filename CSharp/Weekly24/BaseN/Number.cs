using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN {
    public class Number {
        public bool IsNegative { get; private set; }
        public ReadOnlyCollection<int> Digits { get; private set; }
        public int Base { get; private set; }

        public Number (IList<int> digits, int baseNum, bool isNegative = false) {
            IsNegative = isNegative;
            Digits = new ReadOnlyCollection<int>(digits);
            Base = baseNum;
        }
    }
}
