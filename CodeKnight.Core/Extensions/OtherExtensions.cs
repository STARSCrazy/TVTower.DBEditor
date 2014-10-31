using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeKnight.Core
{
    public static class OtherExtensions
    {
        public static bool IsNumeric( this string text )
        {
            int n;
            return int.TryParse( text, out n );
        }

        public static IEnumerable<Enum> ToEnumFlags( this int value, Type enumType )
        {
            var enumObj = Enum.ToObject(enumType, value);
            return ( (Enum)enumObj ).GetFlags();
        }

        public static IEnumerable<Enum> GetFlags( this Enum input, bool withZeroValue = false )
        {
            //foreach ( Enum value in Enum.GetValues( input.GetType() ) )
            //    if ( input.HasFlag( value ) )
            //        yield return value;

            var result = new List<Enum>();

            foreach ( Enum value in Enum.GetValues( input.GetType() ) )
            {
                if ( input.HasFlag( value ) )
                {
                    if ( ( (int)Enum.ToObject( input.GetType(), value ) ) != 0 || withZeroValue )
                    {
                        result.Add( value );
                    }
                }
            }
            return result;
        }
    }
}
