using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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

        public static string ToContentString(this IEnumerable list, char seperator = ';')
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

        public static string ToContentString<T>( this IEnumerable list, char seperator = ';' )
        {
            var result = new StringBuilder();
            foreach ( var value in list )
            {
                if ( result.Length > 0 )
                    result.Append( seperator );

                result.Append( ((T)value).ToString() );
            }
            return result.ToString();
        }

        public static List<string> ToStringList(this string sourceString, char seperator = ';')
        {
            return sourceString.Split(seperator).ToList();
        }

        public static IEnumerable<T> ForEach<T>( this IEnumerable<T> enumerable, Action<T> action )
        {
            foreach ( var item in enumerable )
            {
                action.Invoke( item );
            }
            return enumerable;
        }
    }
}
