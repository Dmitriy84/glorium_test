using System;
using System.Linq;

namespace OlxUaTests.Utils
{
    class Util
    {
        public static string GetRandomString(int length = 10, string chars = "abcdefghijklmnopqrstuvwxyz0123456789")
        {
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static DateTime GetWeekday(DayOfWeek day, DateTime start)
        {
            while (start.DayOfWeek != day)
                start = start.AddDays(1);
            return start;
        }

        private static Random random = new Random();
    }
}