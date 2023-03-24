using Common.CustomNaming;
using System;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class StringMappingHelper
    {
        public static int? GetNumberBeforeDash(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return ParseStringToInt(name.Split('-')[0]);

            return null;
        }
        public static int? GetNumberAfterDash(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return ParseStringToInt(name.Split('-')[1]);

            return null;
        }
        public static int? GetNumberBeforeSlash(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return ParseStringToInt(name.Split('/')[0]);

            return null;
        }
        public static string GetStringBeforeSpace(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return name.Split(' ')[0];

            return null;
        }
        public static int? GetNumberBeforeSpace(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return ParseStringToInt(name.Split(' ')[0]);

            return null;
        }
        public static int? GetNumberAfterSlash(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return ParseStringToInt(GetStringAfterSlash(name));

            return null;
        }
        public static string GetStringAfterSlash(string name)
        {
            return name.Split('/')[1];
        }
        public static string GetStringBetweenQuatationonMark(string name)
        {
            if (String.IsNullOrEmpty(name))
                return null;

            if (name.Split().Length < 1)
                return null;

            return name.Split('"')[1];
        }
        public static string GetStringAfterSpace(string name)
        {
            try
            {

                var item= name.Split(' ');

                if (item.Length <= 1)
                    return null;

                if (item[1] == "brak")
                    return null;

                return item[1];
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
            
        }
        public static string GetStringBeforeComa(string name)
        {
            try
            {

                var item = name.Split(',');

                if (item.Length <= 1)
                    return null;

                return item[0];
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
        public static string GetStringsAfterSpace(string name)
        {
            try
            {
                var item = name.Split(' ');

                if (item.Length <= 1)
                    return null;

                return name.Substring(name.IndexOf(' ') + 1); ;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
        public static string GetStringAfterSecondSpace(string name)
        {
            try
            {
                return name.Split(' ')[2];
            }
            catch (Exception ex)
            {
                return String.Empty;
            }

        }
        public static int? GetNumberAfterSemicolon(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return ParseStringToInt(name.Split(';')[1]);

            return null;
        }
        public static string GetStringAfterSemicolon(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return name.Split(";")[1];

            return null;
        }
        public static int? ParseStringToInt(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return Int32.Parse(name);

            return null;
        }
        public static string GetStringIfLengthIsNotZero(string name)
        {
            if (name.Length > 1)
                return name;

            return null;
        }
        public static string GetStringBeforeColon(string name)
        {
            try
            {
                var item = name.Split(':');

                if (item.Length < 1)
                    return null;

                return item[0];
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
        public static string GetStringAfterColon(string name)
        {
            try
            {
                var item = name.Split(':');

                if (item.Length < 1)
                    return null;

                return item[1];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string RemoveNElement(string name,char el, int numberOfEl)
        {
            int i = 0;
            string newName = "";

            foreach (var c in name)
            {
                if (c == el)
                {
                    i++;
                    if (i == numberOfEl)
                        continue;
                }
                newName += c;
            }
            return newName;
        }
        public static string RemoveDigits(string name)
        {
            return Regex.Replace(name, @"[\d-]", string.Empty);
        }
        public static string RemoveSlashes(string name)
        {
            return name.Replace("/", string.Empty);
        }
        public static string RemoveYears(string name)
        {
            var rmD = RemoveDigits(name);
            var rmS= RemoveSlashes(rmD);

            return RemoveNElement(rmS, ' ', 2);
        }
    }
}
