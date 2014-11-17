using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TVTower.UnitTests
{
    public static class AssertExt
    {
        public static void AreCollectionEqual<T>(ICollection<T> expected, ICollection<T> actual)
        {
            if ( expected == null )
            {
                if ( actual != null )
                    Assert.Fail( string.Format( "Assert.AreCollectionEqual failed. Expected:<{0}>. Actual:<{1}>.", "null", actual.Count() ) );
            }
            else
            {
                if ( actual == null )
                    Assert.Fail( string.Format( "Assert.AreCollectionEqual failed. Expected:<{0}>. Actual:<{1}>.", expected.Count(), "null" ) );
                else
                {
                    Assert.AreEqual( expected.Count, actual.Count, "Assert.AreCollectionEqual failed. Expected:<{0}>. Actual:<{1}>.", expected.Count(), actual.Count() );

                    for ( var i = 0; i < expected.Count(); i++ )
                    {
                        Assert.AreEqual( expected.ElementAt( i ), actual.ElementAt( i ), "Assert.AreCollectionEqual item failed." );
                    }
                }
            }

            
        }
    }
}
