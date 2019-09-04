using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Kata_Bowling.Operators;
using Kata_Bowling.Models;
using System.Linq;
using Kata_Bowling.Utilities;

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
            Assert.Equal(6, oneFrame.Status);
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

            Assert.Equal(20, result);
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


            Assert.Equal(40, result);
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
            roundOfPlay.Frames[3] = fourthFrame;

            var result1 = roundOfPlay.Frames[0].GetScoreForFrame(roundOfPlay.Frames);
            var result2 = roundOfPlay.Frames[1].GetScoreForFrame(roundOfPlay.Frames);
            var shouldBeNull = roundOfPlay.Frames[2].GetScoreForFrame(roundOfPlay.Frames);
            var resultTotal = roundOfPlay.TotalScore;
            Assert.Equal(30, result1);
            Assert.Equal(30, result2);
            Assert.Equal(60, resultTotal);
            Assert.Null(shouldBeNull);

        }

        [Fact]
        public void GetMaxPointsForAFrame_AndThenStopStrikeChain()
        {
            var testPlayer = new Player();
            var firstFrame = new Frame { FrameNumber = 1, FirstThrow = 10, SecondThrow = null };
            var secondFrame = new Frame() { FrameNumber = 2, FirstThrow = 10, SecondThrow = null };
            var thirdFrame = new Frame() { FrameNumber = 3, FirstThrow = 10, SecondThrow = null };
            var fourthFrame = new Frame() { FrameNumber = 4, FirstThrow = 10, SecondThrow = null };
            var fifthFrame = new Frame() { FrameNumber = 5, FirstThrow = 1, SecondThrow = 0 };

            var roundOfPlay = new RoundOfPlay(testPlayer, 1);
            roundOfPlay.Frames[0] = firstFrame;
            roundOfPlay.Frames[1] = secondFrame;
            roundOfPlay.Frames[2] = thirdFrame;
            roundOfPlay.Frames[3] = fourthFrame;
            roundOfPlay.Frames[4] = fifthFrame;

            var result = roundOfPlay.Frames[0].GetScoreForFrame(roundOfPlay.Frames);
            var resultTotal = roundOfPlay.TotalScore;
            Assert.Equal(30, result);
            Assert.Equal(93, resultTotal);

        }

        [Fact]
        public void GetStatusFromFrame_Expecting_Strike()
        {
            //Arrange
            var oneFrame = new Frame { FirstThrow = 10 };
            //Act
            var result = oneFrame.Status;
            //Assert
            Assert.Equal((int)StatusUtility.FrameStatus.Strike, result);

        }

        [Fact]
        public void GetStatusFromFrame_Expecting_Spare()
        {
            //Arrange
            var oneFrame = new Frame { FirstThrow = 0, SecondThrow = 10 };
            //Act
            var result = oneFrame.Status;
            //Assert
            Assert.Equal((int)StatusUtility.FrameStatus.Spare, result);
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
        public void ThrowInvalidOperation_When_Impossible_Score_Set()
        {
            //Act
            var ex = Assert.Throws<InvalidOperationException>(() => new Frame { FirstThrow = 7, SecondThrow = 5 });
            //Assert
            Assert.Equal("Knocked down pins in frame are greater than 10 (7 + 5). Something went wrong", ex.Message);
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
            var frames = GenerateSpecifiedNumberOfRandomFrames(10);
            var extraFrame = GenerateSpecifiedNumberOfRandomFrames(1);
            
            round.Frames = frames.ToArray();
            _ = Assert.Throws<IndexOutOfRangeException>(() => round.Frames[10] = extraFrame.FirstOrDefault());
            
            Assert.Equal(10, round.Frames.Count());
        }

        [Fact]
        public void CorrectTotalScoreForFullRound_WithoutThirdThrow()
        {
            //Arrange
            var round = new RoundOfPlay(new Player(), 1);
            round.Frames = GeneratePredictableFrames(10);

            //Act
            var result = round.TotalScore;

            //Assert
            Assert.Equal(45, result);
        }

        [Fact]
        public void CorrectCountStrikeSpareNormal()
        {
            //Arrange
            var testPlayer = new Player();
            var firstFrame = new Frame { FrameNumber = 1, FirstThrow = 10, SecondThrow = null };
            var secondFrame = new Frame() { FrameNumber = 2, FirstThrow = 3, SecondThrow = 7 };
            var thirdFrame = new Frame() { FrameNumber = 3, FirstThrow = 5, SecondThrow = 3 };

            var roundOfPlay = new RoundOfPlay(testPlayer, 1);
            roundOfPlay.Frames[0] = firstFrame;
            roundOfPlay.Frames[1] = secondFrame;
            roundOfPlay.Frames[2] = thirdFrame;
            //Act
            var result = roundOfPlay.TotalScore;

            //Assert
            Assert.Equal(43, result);
        }

        [Fact]
        public void CorrectTotalScoreForFullRound_WithSomeStrikesAndSpares_WithoutThirdthrow()
        {
            //Arrange
            var round = new RoundOfPlay(new Player(), 1);
            round.Frames = GeneratePredictableFrames(10);


            round.Frames[0] = new Frame() { FrameNumber = 1, FirstThrow = 10 };
            round.Frames[1] = new Frame() { FrameNumber = 2, FirstThrow = 2, SecondThrow = 8 };
            round.Frames[3] = new Frame() { FrameNumber = 4, FirstThrow = 0, SecondThrow = 10 };
            round.Frames[5] = new Frame() { FrameNumber = 6, FirstThrow = 10 };
            foreach(var test in round.Frames)
            {
                var totalFrame = test.GetScoreForFrame(round.Frames);
            }
            //Act
            //var result = round.Frames[0].GetScoreForFrame(round.Frames);
            var result = round.TotalScore;

            //Assert
            Assert.Equal(98, result);
        }

        [Fact]
        public void OnlyAllowThirdThrowOnRoundTen()
        {
            var round = new RoundOfPlay(new Player(), 1);
            round.Frames = GeneratePredictableFrames(10);
            _ = Assert.Throws<InvalidOperationException>(() => round.Frames[0].ThirdThrow = 3);

            round.Frames[9].ThirdThrow = 3;
            Assert.Equal(3, round.Frames[9].ThirdThrow);

        }

        [Fact]
        public void CalculateCorrectValueOnRoundTenAllStrikes()
        {
            var frames = GeneratePredictableFrames(10);
            frames[9] =  new Frame() { FrameNumber = 10, FirstThrow = 10, SecondThrow = 10  };           
            frames[9].ThirdThrow = 10;

            var result = frames[9].GetScoreForFrame(frames);
            Assert.Equal(30, result);

        }

        [Fact]
        public void CalculateCorrectWithStrikeOnNine_AndOpeningStrikeOn10Ten()
        {
            var frames = GeneratePredictableFrames(10);
            frames[8] = new Frame() { FrameNumber = 9, FirstThrow = 10 };
            frames[9] = new Frame() { FrameNumber = 10, FirstThrow = 10, SecondThrow = 1 };
            frames[9].ThirdThrow = 5;

            var result = frames[8].GetScoreForFrame(frames);
            Assert.Equal(21, result);

        }

        [Fact]
        public void TryGenerateAndCalculate100000Games()
        {
            var round = new RoundOfPlay(new Player(), 1);
            var total = 0;
            var highestRound = 0;
            var highestRoundFrames = new Frame[10];
            var lowestRound = 100;
            var lowestRoundFrames = new Frame[10];
            try
            {
                for (int i = 0; i < 100000; i++)
                {
                    var fullRoundOfFrames = GenerateSpecifiedNumberOfRandomFrames(10);
                    round.Frames = fullRoundOfFrames.ToArray();
                    total += (int)round.TotalScore;
                    if ((int)round.TotalScore > highestRound)
                    {
                        highestRound = (int)round.TotalScore;
                        highestRoundFrames = round.Frames;
                    }
                    if ((int)round.TotalScore < lowestRound)
                    {
                        lowestRound = (int)round.TotalScore;
                        lowestRoundFrames = round.Frames;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Did not expect an exception, but got: " + ex.Message);
            }

        }
        private Frame[] GeneratePredictableFrames(int numberOfFrames)
        {
            var frames = new Frame[10];
            for (int i = 0; i < numberOfFrames; i++)
            {
                frames[i] = new Frame() { FrameNumber = i + 1, FirstThrow = i, SecondThrow = 0 };
               
            }
            return frames;
        }

        private List<Frame> GenerateSpecifiedNumberOfRandomFrames(int numberOfFrames)
        {
            var returnList = new List<Frame>();
            var rng = new Random();
            for (int i = 0; i < numberOfFrames; i++)
            {
                var firstThrow = rng.Next(0, 11);
                
                var secondThrow = firstThrow != 10 ? rng.Next(0, 11) : 0;
                if ((firstThrow + secondThrow) > 10)
                {
                    while (secondThrow + firstThrow > 10)
                    {
                        secondThrow = rng.Next(0, 11);
                    }
                }
                var frame = new Frame() { FrameNumber = i + 1, FirstThrow = firstThrow, SecondThrow = secondThrow };
                if (i == 9)
                {
                    if (firstThrow == 10)
                        frame.ThirdThrow = rng.Next(0, 11);

                }
                returnList.Add(frame);
            }
            return returnList;
        }
    }
}
