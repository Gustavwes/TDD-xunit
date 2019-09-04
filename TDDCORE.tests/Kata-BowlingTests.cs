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
            var frames = new Frame[10];
            frames[0] = oneFrame;
            //Act
            oneFrame.FirstThrow = 5;
            oneFrame.SecondThrow = 2;
            //Assert
            Assert.Equal(7, oneFrame.GetScoreForFrame(frames));
            Assert.Null(oneFrame.Status);
        }
        [Fact]
        public void GetTotalScoreOnPlayerRound_Simple_After_Two_Frames()
        {

            var testPlayer = new Player();
            var firstFrame = new Frame { FirstThrow = 4, SecondThrow = 1 };
            var secondFrame = new Frame() { FirstThrow = 0, SecondThrow = 2 };
            var frames = new Frame[10];
            frames[0] = firstFrame;
            frames[1] = secondFrame;
            var roundOfPlay = new RoundOfPlay(testPlayer, 1) { Frames = frames  };

            Assert.Equal(7, roundOfPlay.TotalScore);
        }
        [Fact]
        public void GetTotalScoreOnPlayerRound_Simple_After_Two_Frames_When_One_Score_Is_Null()
        {
            var testPlayer = new Player();
            var firstFrame = new Frame { FirstThrow = 4, SecondThrow = 1 };
            var secondFrame = new Frame() { FirstThrow = 0, SecondThrow = null };
            var frames = new Frame[10];
            frames[0] = firstFrame;
            frames[1] = secondFrame;
            var roundOfPlay = new RoundOfPlay(testPlayer, 1) { Frames = frames }; ;

            Assert.Equal(5, roundOfPlay.TotalScore);
        }
        [Fact]
        public void GetTotalScoresWithSpare()
        {
            var testPlayer = new Player();
            var firstFrame = new Frame { FrameNumber = 1, FirstThrow = 4, SecondThrow = 6 };
            var secondFrame = new Frame() { FrameNumber = 2, FirstThrow = 2, SecondThrow = 3 };
            var roundOfPlay = new RoundOfPlay(testPlayer, 1);
            roundOfPlay.Frames[0] = firstFrame;
            roundOfPlay.Frames[1] = secondFrame;

            var result = roundOfPlay.TotalScore;


            Assert.Equal(17, result);
        }

        [Fact]
        public void GetTotalScoresWithStrikeSpare()
        {
            var testPlayer = new Player();
            var firstFrame = new Frame { FrameNumber = 1, FirstThrow = 10, SecondThrow = null };
            var secondFrame = new Frame() { FrameNumber = 2, FirstThrow = 2, SecondThrow = 8 };
            var roundOfPlay = new RoundOfPlay(testPlayer, 1);
            roundOfPlay.Frames[0] = firstFrame;
            roundOfPlay.Frames[1] = secondFrame;

            var result = roundOfPlay.TotalScore;

            Assert.Null(result);
        }
        [Fact]
        public void GetTotalScoresWithSimpleStrike()
        {
            var testPlayer = new Player();
            var firstFrame = new Frame { FrameNumber = 1, FirstThrow = 10, SecondThrow = null };
            var secondFrame = new Frame() { FrameNumber = 2, FirstThrow = 2, SecondThrow = 3 };
            var roundOfPlay = new RoundOfPlay(testPlayer, 1);
            roundOfPlay.Frames[0] = firstFrame;
            roundOfPlay.Frames[1] = secondFrame;

            var result = roundOfPlay.TotalScore;


            Assert.Equal(20, result);
        }
        [Fact]
        public void GetTotalScoresWithStrikeSpareNormal()
        {
            var testPlayer = new Player();
            var firstFrame = new Frame { FrameNumber = 1, FirstThrow = 10, SecondThrow = null };
            var secondFrame = new Frame() { FrameNumber = 2, FirstThrow = 2, SecondThrow = 8 };
            var thirdFrame = new Frame() { FrameNumber = 3, FirstThrow = 5, SecondThrow = 0 };

            var roundOfPlay = new RoundOfPlay(testPlayer, 1);
            roundOfPlay.Frames[0] = firstFrame;
            roundOfPlay.Frames[1] = secondFrame;
            roundOfPlay.Frames[2] = thirdFrame;

            var result = roundOfPlay.TotalScore;


            Assert.Equal(45, result);
        }
        [Fact]
        public void GetMaxPointsForAFrame()
        {
            var testPlayer = new Player();
            var firstFrame = new Frame { FrameNumber = 1, FirstThrow = 10, SecondThrow = null };
            var secondFrame = new Frame() { FrameNumber = 2, FirstThrow = 10, SecondThrow = null };
            var thirdFrame = new Frame() { FrameNumber = 3, FirstThrow = 10, SecondThrow = null };
            var fourthFrame = new Frame() { FrameNumber = 3, FirstThrow = 10, SecondThrow = null };

            var roundOfPlay = new RoundOfPlay(testPlayer, 1);
            roundOfPlay.Frames[0] = firstFrame;
            roundOfPlay.Frames[1] = secondFrame;
            roundOfPlay.Frames[2] = thirdFrame;
            roundOfPlay.Frames[2] = fourthFrame;

            var result = roundOfPlay.Frames[0].GetScoreForFrame(roundOfPlay.Frames);

            Assert.Equal(30, result);

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
        [Fact]
        public void ThrowIndexOutOfRange_When_Over_Ten_Frames_Are_Added_To_Round()
        {
            var round = new RoundOfPlay(new Player(), 1);
            var frames = GenerateSpecifiedNumberOfFrames(10);
            var extraFrame = GenerateSpecifiedNumberOfFrames(1);
            
            round.Frames = frames.ToArray();
            _ = Assert.Throws<IndexOutOfRangeException>(() => round.Frames[10] = extraFrame.FirstOrDefault());
            
            Assert.Equal(10, round.Frames.Count());
        }


        private List<Frame> GenerateSpecifiedNumberOfFrames(int numberOfFrames)
        {
            var returnList = new List<Frame>();
            var rng = new Random();
            for (int i = 0; i < numberOfFrames; i++)
            {
                var firstThrow = rng.Next(0, 11);
                var secondThrow = rng.Next(0, 11);
                if ((firstThrow + secondThrow) > 10)
                {
                    while (secondThrow + firstThrow > 10)
                    {
                        secondThrow = rng.Next(0, 11);
                    }
                }
                returnList.Add(new Frame() { FirstThrow = firstThrow, SecondThrow = secondThrow });
            }
            return returnList;
        }
    }
}
