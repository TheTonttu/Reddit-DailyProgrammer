using System;

namespace Tonttu.Reddit.DailyProgrammer.Challenge4.NumberRelations {
    public class CalcOperation {
        public double A { get; private set; }
        public double B { get; private set; }

        public Operation Operation { get; private set; }

        public double Result { get; private set; }

        public CalcOperation(double a, double b, Operation op) {
            A = a;
            B = b;
            Operation = op;

            double result;
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
                case Operation.Modulo:
                    result = a % b;
                    break;
                default:
                    result = 0;
                    break;
            }
            Result = result;
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} = {3}", A, OperatorChar(), B, Result);
        }

        private char OperatorChar() {
            switch (Operation) {
                case Operation.Addition:
                    return '+';
                case Operation.Subtraction:
                    return '-';
                case Operation.Multiplication:
                    return '*';
                case Operation.Division:
                    return '/';
                case Operation.Modulo:
                    return '%';
                default:
                    throw new NotImplementedException(String.Format("Operator char for {0} not implemented", Operation));
            }
        }
    }

    public enum Operation {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Modulo
    }
}
