using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeKnight.Core.Test
{
    [TestClass]
    public class EnumFlagTest
    {
        [TestMethod]
        public void SetFlagSumTest()
        {
            var enumFlag = new EnumFlag<TestFlag>();

            enumFlag.SetFlagSum( 5 );
            Assert.IsFalse( enumFlag.Test( TestFlag.None ) );
            Assert.IsTrue( enumFlag.Test( TestFlag.Value1 ) );
            Assert.IsFalse( enumFlag.Test( TestFlag.Value2 ) );
            Assert.IsTrue( enumFlag.Test( TestFlag.Value3 ) );
            Assert.IsFalse( enumFlag.Test( TestFlag.Value4 ) );
            Assert.IsFalse( enumFlag.Test( TestFlag.Value5 ) );

            enumFlag.SetFlagSum( 7 );
            Assert.IsFalse( enumFlag.Test( TestFlag.None ) );
            Assert.IsTrue( enumFlag.Test( TestFlag.Value1 ) );
            Assert.IsTrue( enumFlag.Test( TestFlag.Value2 ) );
            Assert.IsTrue( enumFlag.Test( TestFlag.Value3 ) );
            Assert.IsFalse( enumFlag.Test( TestFlag.Value4 ) );
            Assert.IsFalse( enumFlag.Test( TestFlag.Value5 ) );

            enumFlag.Accept( TestFlag.Value5 );
            Assert.AreEqual( 23, enumFlag.ToBitMask() );
        }

        [TestMethod]
        public void FlagTagTest()
        {
            var flagSum = TestFlag2.Value2 | TestFlag2.Value4;

            Assert.AreEqual( 10, (int)flagSum );

            Assert.AreEqual( TestFlag2.Value2, flagSum.GetFlags().ToList()[0] );
            Assert.AreEqual( TestFlag2.Value4, flagSum.GetFlags().ToList()[1] );

            Assert.AreEqual( TestFlag2.None, flagSum.GetFlags( true ).ToList()[0] );
            Assert.AreEqual( TestFlag2.Value2, flagSum.GetFlags( true ).ToList()[1] );
            Assert.AreEqual( TestFlag2.Value4, flagSum.GetFlags( true ).ToList()[2] );
        }
    }

    public enum TestFlag
    {
        None = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 4,
        Value4 = 8,
        Value5 = 16
    }

    [Flags]
    public enum TestFlag2
    {
        None = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 4,
        Value4 = 8,
        Value5 = 16
    }
}
