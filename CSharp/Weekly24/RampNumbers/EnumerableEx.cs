using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.RampNumbers {
    public static class EnumerableEx {
        public static IEnumerable<long> Range(long start, long count) {
            var end = start + count;
            for (var current = start; current < end; current++) {
                yield return current;
            }
        }
    }
}
