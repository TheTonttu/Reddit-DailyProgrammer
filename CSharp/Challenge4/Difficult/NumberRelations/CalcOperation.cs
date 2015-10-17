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
            switch (Operation) {
                case Operation.Addition:
                    return String.Format("{0} + {1} = {2}", A, B, Result);
                case Operation.Subtraction:
                    return String.Format("{0} - {1} = {2}", A, B, Result);
                case Operation.Multiplication:
                    return String.Format("{0} * {1} = {2}", A, B, Result);
                case Operation.Division:
                    return String.Format("{0} / {1} = {2}", A, B, Result);
                case Operation.Modulo:
                    return String.Format("{0} % {1} = {2}", A, B, Result);
                default:
                    return String.Empty;
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
