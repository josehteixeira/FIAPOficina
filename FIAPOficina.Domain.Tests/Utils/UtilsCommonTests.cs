using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAPOficina.Domain.Utils;

namespace FIAPOficina.Domain.Tests.Utils
{
    public class UtilsCommonTests
    {
        [Fact]
        public void Should_Return_Qunatity()
        {
            Assert.Equal(10, UtilsCommon.ValidQuantity(10));
        }
        
        [Fact]
        public void Should_Return_Value()
        {
            Assert.Equal(10.15m, UtilsCommon.ValidValue(10.15m));
        }
        
        [Fact]
        public void Should_Value_Throw_ArgumentOutOfRangeExceptio()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=>UtilsCommon.ValidValue(-10.15m));
        }

        [Fact]
        public void Should_Quantity_Throw_ArgumentOutOfRangeExceptio()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=>UtilsCommon.ValidQuantity(-15));
        }
    }
}
