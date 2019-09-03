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
        [Fact]
        public void GetTotalScoreOnPlayerRound_Simple_After_Two_Frames()
        {

            var testPlayer = new Player();
            var oneFrame = new Frame { FirstThrow = 4, SecondThrow = 1 };
            var secondFrame = new Frame() { FirstThrow = 0, SecondThrow = 2 };
            var roundOfPlay = new RoundOfPlay() { Player = testPlayer, Frames = new List<Frame>() { oneFrame, secondFrame } };

            Assert.Equal(7, roundOfPlay.TotalScore);
        }
        [Fact]
        public void GetTotalScoreOnPlayerRound_Simple_After_Two_Frames_When_One_Score_Is_Null()
        {
            var testPlayer = new Player();
            var oneFrame = new Frame { FirstThrow = 4, SecondThrow = 1 };
            var secondFrame = new Frame() { FirstThrow = 0, SecondThrow = null };
            var roundOfPlay = new RoundOfPlay() { Player = testPlayer, Frames = new List<Frame>() { oneFrame, secondFrame } };

            Assert.Equal(5, roundOfPlay.TotalScore);
        }
        [Fact]
        public void GetStatusFromFrame_Expecting_Strike()
        {
            //Arrange
            var oneFrame = new Frame { FirstThrow = 10 };
            //Act
            var result = oneFrame.Status;
            //Assert
            Assert.Equal("Strike", result);

        }
        [Fact]
        public void GetStatusFromFrame_Expecting_Spare()
        {
            //Arrange
            var oneFrame = new Frame { FirstThrow = 0, SecondThrow = 10 };
            //Act
            var result = oneFrame.Status;
            //Assert
            Assert.Equal("Spare", result);
        }
        [Fact]
        public void ThrowInvalidOperation_When_Negative_Score_Set()
        {
            //Act
            var ex = Assert.Throws<InvalidOperationException>(() => new Frame { FirstThrow = -1 });
            //Assert
            Assert.Equal("Negatives not allowed: -1", ex.Message);
        }

        [Fact]
        public void ThrowInvalidOperation_When_Too_High_Score_Set()
        {
            //Act
            var ex = Assert.Throws<InvalidOperationException>(() => new Frame { FirstThrow = 111 });
            //Assert
            Assert.Equal("Values over 10 not allowed: 111", ex.Message);
        }

    }
}
