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


    }
}
