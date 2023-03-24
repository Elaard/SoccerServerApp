using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Helpers
{
    public static class DictionaryValuesHelper
    {
        public static int? GetKeyBasedOnBiggestValueInDictContainsTwoPairs(Dictionary<int, int?> teams)
        {
            if (teams.Values.Contains(null))
                return null;

            return teams.Aggregate((h, a) => h.Value > a.Value ? h : a).Key;
        }
    }
}
