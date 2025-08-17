using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Extensions
{
    internal static class DateExtensions
    {
        public static int Age(this DateTime dob)
        {
            var today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }

        public static bool IsWeekend(this DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        public static DateTime NextWorkingDay(this DateTime date)
        {
            DateTime next = date.AddDays(1);
            while (next.IsWeekend()) next = next.AddDays(1);
            return next;
        }

        public static int DaysBetween(this DateTime from, DateTime to) => (to - from).Days;

    }
}
