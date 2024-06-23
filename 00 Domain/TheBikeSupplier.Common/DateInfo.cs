using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Common
{
    public enum Months
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public static class DateInfo
    {
        public static IEnumerable<Months> MonthList => Enum.GetValues(typeof(Months)).Cast<Months>().ToList();
        public static IEnumerable<int> YearList => Enumerable.Range(1900, DateTime.Now.Year - 1900 - 12).OrderByDescending(x => x);
        public static IEnumerable<int> CalculateDayList(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month));
        }
    }
}
