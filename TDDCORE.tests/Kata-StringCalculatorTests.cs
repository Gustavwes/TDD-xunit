using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TDDCORE.Operators;

namespace TDDCORE.tests
{
    //based on this: https://osherove.com/tdd-kata-1
    public class Kata_StringCalculatorTests
    {
        [Fact]
        public void Add_Test_Simple()
        {
            //Arrange
            var numberString = "1,2,3";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(6,result);

        }
        [Fact]
        public void Add_Test_With_Null()
        {
            //Act
            var result = new StringCalculator().Add(null);
            //Assert
            Assert.Equal(0, result);

        }
        [Fact]
        public void Add_Test_With_Spaces()
        {
            //Arrange
            var numberString = " 1 ,2 , 3";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_Test_With_Invalid_Number()
        {
            //Arrange
            var numberString = " 1 ,2 , three";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(3, result);
        }
        [Fact]
        public void Add_Test_With_Empty_String()
        {
            //Arrange
            var numberString = "";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(0, result);
        }
        [Fact]
        public void Add_Test_With_New_Lines()
        {
            //Arrange
            var numberString = "1\n2,3";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_Test_With_New_Delimiter()
        {
            //Arrange
            var numberString = "//;\n1;2";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(3, result);
        }
        [Fact]
        public void Add_Test_With_Negative_Number()
        {
            //Arrange
            var numberString = "-2,15,4";
            //Act
            var ex = Assert.Throws<InvalidOperationException>(() => new StringCalculator().Add(numberString));
            //Assert
            Assert.Equal("Negatives not allowed: -2", ex.Message);
        }
        [Fact]
        public void Add_Test_With_Negative_Numbers()
        {            
            //Arrange
            var numberString = "-2,-5,4";
            //Act
            var ex = Assert.Throws<InvalidOperationException>(() => new StringCalculator().Add(numberString));
            //Assert
            Assert.Equal("Negatives not allowed: -2,-5", ex.Message);
        }
        [Fact]
        public void Add_Test_Ignore_Numbers_Over_1000()
        {
            //Arrange
            var numberString = "1,1002";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(1, result);
        }
        [Fact]
        public void Add_Test_With_New_Delimiter_Of_SeveralCharacters()
        {
            //Arrange 
            var numberString = "//[*][%]\n1*2%3"; 
            //Act 
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_Test_With_New_Delimiter_Of_SeveralStrings()
        {
            //Arrange
            var numberString = "//[****][%%%%][$$$$$]\n1****2%%%%3$$$$$4";
            //Act
            var result = new StringCalculator().Add(numberString);
            //Assert
            Assert.Equal(10, result);
        }
    }
}
