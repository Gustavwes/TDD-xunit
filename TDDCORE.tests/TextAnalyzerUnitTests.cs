using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TDDCORE.Operators;

namespace TDDCORE.tests
{
    public class TextAnalyzerUnitTests
    {
        [Fact]
        public void GetTotalWordCount_WhenPassedRegularText_ShouldReturnExpectedCount()
        {
            //arrange
            const string textToAnalyze = @"These are some words organized into a sentence";
            var analyzer = new TextAnalyzer();

            //act
            var count = analyzer.GetTotalWordCount(textToAnalyze);

            //assert
            Assert.Equal(8, count);
        }
    }
}
