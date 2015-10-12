
namespace Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN {
    public class BaseNArguments {
        public int Number { get; private set; }
        public int Base { get; private set; }
        
        public BaseNArguments(int number, int baseNum) {
            Number = number;
            Base = baseNum;
        }
    }
}
