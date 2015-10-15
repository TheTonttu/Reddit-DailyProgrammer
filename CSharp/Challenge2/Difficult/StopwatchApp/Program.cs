using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatchApp {
    /// <summary>
    /// Challenge #2 difficult
    /// Stopwatch by nottoobadguy
    /// http://www.reddit.com/r/dailyprogrammer/comments/pjsdx/difficult_challenge_2/
    /// 
    /// Description:
    /// Your mission is to create a stopwatch program. this program should have start, stop, and lap options, and it should write out to a file to be viewed later.
    /// </summary>
    class Program {
        private const long REFRESH_INTERVAL_MS = 250;

        private const string ResultFileName = "stopwatch.log";
        private static readonly string EmptyRow = new String(' ', Console.WindowWidth-1);

        static void Main(string[] args) {
            var internalClock = new Stopwatch();
            var stopWatch = new Stopwatch();
            Console.CursorVisible = false;
            using (var log = new StreamWriter(File.Open(ResultFileName, FileMode.Append, FileAccess.Write))) {
                PrintHeader();

                bool isRunning = true;
                List<TimeSpan> laps = new List<TimeSpan>();
                TimeSpan? previousLap = null;
                ConsoleKeyInfo? lastInput = null;
                internalClock.Start();
                long lastTimerUpdateMs = 0;
                int currLapNumber = 1;
                while (isRunning) {
                    if (Console.KeyAvailable) {
                        // Read input
                        lastInput = Console.ReadKey();
                        ClearConsoleKeyInput();
                    }
                    if (lastInput.HasValue) {
                        ConsoleKeyInfo input = lastInput.Value;
                        if (IsInputToggleTimer(input.Key)) {
                            ClearCurrentConsoleRow();
                            if (stopWatch.IsRunning) {
                                stopWatch.Stop();
                                TimeSpan lastLap = stopWatch.Elapsed;
                                laps.Add(lastLap);
                                int lapNumber = laps.Count;
                                if (laps.Count > 1) {
                                    TimeSpan prevLap = GetPreviousLap(laps);
                                    string logEntry = CreateLapComparisonLogEntry(lapNumber, lastLap, prevLap);
                                    Console.WriteLine(logEntry);
                                    log.WriteLine(logEntry);
                                } else {
                                    string logEntry = CreateLapLogEntry(lapNumber, lastLap);
                                    Console.WriteLine(logEntry);
                                    log.WriteLine(logEntry);
                                }
                                IEnumerable<string> runStatsLogEntries = CreateLapStatisticsLogEntry(laps);
                                foreach (var entry in runStatsLogEntries) {
                                    log.WriteLine(entry);
                                    Console.WriteLine(entry);
                                }
                                log.WriteLine();
                                Console.WriteLine();
                                laps.Clear();
                                currLapNumber = 1;
                                previousLap = null;
                            } else {
                                Console.Clear();
                                PrintHeader();

                                stopWatch.Restart();
                                log.WriteLine("{0:G}", DateTime.Now);
                                log.WriteLine();
                            }
                        } else if (IsInputLapSwitch(input.Key)) {
                            if (stopWatch.IsRunning) {
                                ClearCurrentConsoleRow();
                                stopWatch.Stop();
                                TimeSpan lap = stopWatch.Elapsed;
                                laps.Add(lap);
                                int lapNumber = laps.Count;
                                if (laps.Count > 1) {
                                    TimeSpan prevLap = GetPreviousLap(laps);
                                    string logEntry = CreateLapComparisonLogEntry(lapNumber, lap, prevLap);
                                    Console.WriteLine(logEntry);
                                    log.WriteLine(logEntry);
                                } else {
                                    string logEntry = CreateLapLogEntry(lapNumber, lap);
                                    Console.WriteLine(logEntry);
                                    log.WriteLine(logEntry);
                                }
                                previousLap = lap;
                                currLapNumber++;
                                stopWatch.Restart();
                            }
                        } else if (IsInputQuit(input.Key)) {
                            isRunning = false;
                        }
                        lastInput = null;
                    }

                    long elapsedMs = internalClock.ElapsedMilliseconds;
                    if (stopWatch.IsRunning && DueRefresh(lastTimerUpdateMs, elapsedMs)) {
                        TimeSpan runningLap = stopWatch.Elapsed;
                        RefreshRunningTimer(currLapNumber, runningLap, previousLap, elapsedMs);
                        lastTimerUpdateMs = elapsedMs;
                    }
                }
                if (stopWatch.IsRunning) {
                    stopWatch.Stop();
                    // Do not take the lap time as this might have been premature quit.

                    IEnumerable<string> runStatsLogEntries = CreateLapStatisticsLogEntry(laps);
                    foreach (var entry in runStatsLogEntries) {
                        log.WriteLine(entry);
                    }
                    log.WriteLine();
                }
            }
        }

        private static void RefreshRunningTimer(int runningLapNumber, TimeSpan runningLap, TimeSpan? previousLap, long elapsedMs) {
            ClearCurrentConsoleRow();
            string logEntry;
            if (previousLap.HasValue) {
                TimeSpan prevLap = previousLap.Value;
                logEntry = CreateLapComparisonLogEntry(runningLapNumber, runningLap, prevLap);
            } else {
                logEntry = CreateLapLogEntry(runningLapNumber, runningLap);
            }
            Console.Write(logEntry);
        }

        private static bool DueRefresh(long lastUpdateMs, long elapsedMs) {
            return (elapsedMs - lastUpdateMs) >= 250;
        }

        private static void ClearCurrentConsoleRow() {
            Console.CursorLeft = 0;
            Console.Write(EmptyRow);
            Console.CursorLeft = 0;
        }

        private static IEnumerable<string> CreateLapStatisticsLogEntry(List<TimeSpan> laps) {
            var logEntries = new List<string>();
            if (laps.Count > 0) {
                logEntries.Add("-------------------------------------------");
                logEntries.Add(String.Format("laps: {0}", laps.Count));
                logEntries.Add(String.Format("avg: {0}", new TimeSpan((long)Math.Round(laps.Average(l => l.Ticks)))));
                logEntries.Add(String.Format("min: {0}", laps.Min()));
                logEntries.Add(String.Format("max: {0}", laps.Max()));
                logEntries.Add(String.Format("sum: {0}", new TimeSpan(laps.Sum(l => l.Ticks))));
            } else {
                logEntries.Add("No complete laps made.");
            }
            return logEntries.AsEnumerable();
        }

        private static string CreateLapLogEntry(int lapNumber, TimeSpan lap) {
            return String.Format("#{0}: {1:c}", lapNumber, lap);
        }

        private static string CreateLapComparisonLogEntry(int lapNumber, TimeSpan lap, TimeSpan prevLap) {
            TimeSpan diff = lap - prevLap;
            bool isFaster = lap < prevLap;
            string logEntry = String.Format("#{0}: {1:c} ({2}{3:c})", lapNumber, lap, AddPlusSignIfSlower(isFaster), diff);
            return logEntry;
        }

        private static void PrintHeader() {
            Console.WriteLine("Stopwatch v1.3.3.7");
            Console.WriteLine("Options: [S]tart/[S]top timer, next [L]ap, [Q]uit");
            Console.WriteLine("Results are saved into {0}", ResultFileName);
            Console.WriteLine();
        }

        private static string AddPlusSignIfSlower(bool isFaster) {
            return isFaster ? String.Empty : "+";
        }

        private static TimeSpan GetPreviousLap(List<TimeSpan> laps) {
            int prevLapIndex = laps.Count - 2;
            TimeSpan prevLap = laps[prevLapIndex];
            return prevLap;
        }

        private static void ClearConsoleKeyInput() {
            if (Console.CursorLeft > 0) {
                Console.CursorLeft -= 1;
            }
            Console.Write(' ');
            Console.CursorLeft -= 1;
        }

        private static bool IsInputToggleTimer(ConsoleKey inputKey) {
            return inputKey == ConsoleKey.S;
        }

        private static bool IsInputQuit(ConsoleKey inputKey) {
            return inputKey == ConsoleKey.Q;
        }

        private static bool IsInputLapSwitch(ConsoleKey inputKey) {
            return inputKey == ConsoleKey.L;
        }
    }
}
