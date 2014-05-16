﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeKnight.Core
{
    public static class ListExtensions
    {
        public static Dictionary<T1, T2> ForEach<T1, T2>(this IDictionary<T1, T2> dict, Func<T1, T1> keyMethod, Func<T2, T2> valueMethod)
        {
            var result = new Dictionary<T1, T2>();

            foreach (var kvp in dict)
            {
                var keyTemp = keyMethod == null ? kvp.Key : keyMethod.Invoke(kvp.Key);
                if (keyTemp == null)
                    keyTemp = kvp.Key;

                var valueTemp = valueMethod == null ? kvp.Value : valueMethod.Invoke(kvp.Value);
                if (keyTemp == null)
                    valueTemp = kvp.Value;

                result.Add(keyTemp, valueTemp);
            }
            return result;
        }

        public static string ToContentString<T>(this ICollection<T> list, string seperator = ";")
        {
            var result = new StringBuilder();
            foreach (var value in list)
            {
                if (result.Length > 0)
                    result.Append(seperator);

                result.Append(value);
            }
            return result.ToString();
        }
    }
}
