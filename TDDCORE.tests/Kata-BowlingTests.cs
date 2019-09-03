using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Kata_Bowling.Operators;
using Kata_Bowling.Models;
using System.Linq;

namespace TDDCORE.tests
{
    public class Kata_BowlingTests
    {
        [Fact]
        public void GetTotalOfOneFrame()
        {
            //Arrange            
            var oneFrame = new Frame(); 
            //Act
            oneFrame.FirstThrow = 5;
            oneFrame.SecondThrow = 2;
            //Assert
            Assert.Equal(7, oneFrame.Total);
            Assert.Null(oneFrame.Status);
        }
    }
}
