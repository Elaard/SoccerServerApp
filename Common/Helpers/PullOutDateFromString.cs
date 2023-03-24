using Common.Const;
using Common.CustomNaming;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Helpers
{
    public static class PullOutDateFromString
    {
        public static DateTime? Get(string date)
        {
            if(String.IsNullOrEmpty(date))
                return null;

            var day = StringMappingHelper.GetNumberBeforeSpace(date);
            var month = StringMappingHelper.GetStringAfterSpace(date);
            month = StringMappingHelper.GetStringBeforeComa(month);
            var hour = StringMappingHelper.GetStringAfterSecondSpace(date);
            var formattedDay = FormatDayToString(day.ToString());

            var year = DateTime.Now.Year;
            var formattedMonth = FormatMonthToDateTimeValue(month);
            //var formattedHour = Int32.Parse(StringMappingHelper.GetStringBeforeColon(hour));
            //var formattedMinute = Int32.Parse(StringMappingHelper.GetStringAfterColon(hour));

            string iDate = $"{year}-{formattedMonth}-{formattedDay} {hour}";
            try
            {
                DateTime oDate = DateTime.ParseExact(iDate, "yyyy-MM-dd H:mm", null);
                return oDate;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"{CustomException.CannotCreateData}: {ex.Message}");
            }
        }
        public static string FormatMonthToDateTimeValue(string month)
        {
            string monthLowerCase = month.ToLower();

            if (monthLowerCase.StartsWith(PrefixMonthInPolish.January))
                return "01";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.February))
                return "02";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.March))
                return "03";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.April))
                return "04";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.May))
                return "05";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.June))
                return "06";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.July))
                return "07";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.August))
                return "08";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.September))
                return "09";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.October))
                return "10";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.November))
                return "11";
            if (monthLowerCase.StartsWith(PrefixMonthInPolish.December))
                return "12";

            return null;
        }
        public static string FormatDayToString(string day)
        {
            int formattedDay = Int32.Parse(day);

            if (formattedDay < 10)
                return $"0{day}";

            return day;

        }
        public static string GetHourRelation(int hour, int minutes)
        {
            string am = "AM";
            string pm = "PM";

            if (hour >= 12)
                return pm;

            return am;
        }
    }
}
