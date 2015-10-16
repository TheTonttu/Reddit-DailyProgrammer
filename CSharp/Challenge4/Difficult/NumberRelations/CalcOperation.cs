using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tonttu.Reddit.DailyProgrammer.Challenge4.NumberRelations {
    public class CalcOperation {
        public long A { get; private set; }
        public long B { get; private set; }

        public Operation Operation { get; private set; }

        public long Result { get; private set; }

        public CalcOperation(long a, Operation op, long b) {
            A = a;
            B = b;
            Operation = op;

            long result;
            switch (op) {
                case Operation.Addition:
                    result = a + b;
                    break;
                case Operation.Subtraction:
                    result = a - b;
                    break;
                case Operation.Multiplication:
                    result = a * b;
                    break;
                case Operation.Division:
                    result = b == 0
                        ? 0
                        : a / b;                  
                    break;
                default:
                    result = 0;
                    break;
            }
            Result = result;
        }

        public override string ToString() {
            switch (Operation) {
                case Operation.Addition:
                    return String.Format("{0} + {1} = {2}", A, B, Result);
                case Operation.Subtraction:
                    return String.Format("{0} - {1} = {2}", A, B, Result);
                case Operation.Multiplication:
                    return String.Format("{0} * {1} = {2}", A, B, Result);
                case Operation.Division:
                    return String.Format("{0} / {1} = {2}", A, B, Result);
                default:
                    return String.Empty;
            }
        }
    }

    public enum Operation {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
}
