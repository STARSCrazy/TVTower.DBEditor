using System;
using System.Collections.Generic;

namespace CodeKnight.Core
{
    public class EnumFlag<T> where T : struct, IConvertible
    {
        private int mask;

        public EnumFlag()
        {
            if ( !typeof( T ).IsEnum )
            {
                throw new ArgumentException( "T must be an enumerated type" );
            }
        }

        public EnumFlag<T> All()
        {
            mask = int.MaxValue;
            return this;
        }

        public EnumFlag<T> None()
        {
            mask = 0;
            return this;
        }

        public EnumFlag<T> Except( T type )
        {
            mask &= ~(int)( (object)type );
            return this;
        }

        public EnumFlag<T> Except( IEnumerable<T> types )
        {
            types.ForEach( x => Except( x ) );
            return this;
        }

        public EnumFlag<T> Accept( T type )
        {
            mask |= (int)( (object)type );
            return this;
        }

        public EnumFlag<T> Accept( IEnumerable<T> types )
        {
            types.ForEach( x => Accept( x ) );
            return this;
        }

        public bool Test( T type )
        {
            return ( mask & (int)( (object)type ) ) > 0;
        }

        //public void AddNamedFilter( UseCaseSpecification spec, string filterName, string parameterName )
        //{
        //    var parameterValue = new List<string>();

        //    foreach ( var value in Enum.GetValues( typeof( T ) ) )
        //    {
        //        if ( Test( (T)value ) )
        //            parameterValue.Add( ( value ).ToString() );
        //    }

        //    spec.AddNamedFilter( filterName, parameterName, parameterValue );
        //}

        public void SetFlagSum(int flagSum)
        {
            mask = flagSum;
        }

        public int ToBitMask()
        {
            return mask;
        }

        public List<T> ToTypeList()
        {
            var result = new List<T>();

            foreach ( var value in Enum.GetValues( typeof( T ) ) )
            {
                if ( Test( (T)value ) )
                    result.Add( (T)value );
            }
            return result;
        }
    }
}
