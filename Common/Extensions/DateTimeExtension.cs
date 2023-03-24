using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Extensions
{
    public static class DateTimeExtension
    {
        public static List<DateTime> GetCurrentWeekDays(this DateTime dtt)
        {
            var startDate = DateTime.Today;

            startDate = startDate
            .AddDays(-(((startDate.DayOfWeek - DayOfWeek.Monday) + 7) % 7));

            var endDate = startDate.AddDays(7);
            //the number of days in our range of dates
            var numDays = (int)((endDate - startDate).TotalDays);
            List<DateTime> dates = Enumerable
                       //creates an IEnumerable of ints from 0 to numDays
                       .Range(0, numDays)
                       //now for each of those numbers (0..numDays), 
                       //select startDate plus x number of days
                       .Select(x => startDate.AddDays(x))
                       //and make a list
                       .ToList();

            return dates;
        }
    }
}
