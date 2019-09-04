using Kata_Bowling.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata_Bowling.Models
{
    public class Frame
    {
        public int FrameNumber { get; set; }
        private int ? firstThrow { get; set; }
        public int? FirstThrow {
            get
            {
                return firstThrow;
            }
            set
            {
                if(value < 0)
                    throw new InvalidOperationException("Negatives not allowed: " + value);
                if(value > 10)
                    throw new InvalidOperationException("Values over 10 not allowed: " + value);
                firstThrow = value;
            }
        }
        private int? secondThrow { get; set; }
        public int? SecondThrow {
            get
            {
                return secondThrow; 
            }
            set
            {
                if(value + FirstThrow > 10 && FrameNumber != 10)
                    throw new InvalidOperationException($"Knocked down pins in frame are greater than 10 ({FirstThrow} + {value}). Something went wrong");
                if (value < 0)
                    throw new InvalidOperationException("Negatives not allowed: " + value);
                if (value > 10)
                    throw new InvalidOperationException("Values over 10 not allowed: " + value);

                secondThrow = value;
            }
        }
        private int? thirdThrow { get; set; }
        public int? ThirdThrow
        {
            get
            {
                return thirdThrow;
            }
            set
            {
                if (value < 0)
                    throw new InvalidOperationException("Negatives not allowed: " + value);
                if (value > 10)
                    throw new InvalidOperationException("Values over 10 not allowed: " + value);
                if(FrameNumber != 10)
                    throw new InvalidOperationException("Can only have a third throw in frame 10. Current frame number: " + FrameNumber);
                //if (Status != (int)StatusUtility.FrameStatus.WaitingOnFirstThrow || Status != (int)StatusUtility.FrameStatus.WaitingOnSecondThrow)
                //    throw new InvalidOperationException("Can't set third throw without a value in First & Second that is greater than 10. TEST:" + (FirstThrow + SecondThrow));
                thirdThrow = value;
            }
        }
        private int? scoreForFrame { get; set; }
        
        public int Status
        {
            get
            {
                if (FirstThrow == 10)
                    return (int)StatusUtility.FrameStatus.Strike;
                if (FirstThrow == null)
                    return (int)StatusUtility.FrameStatus.WaitingOnFirstThrow;
                if (FirstThrow < 10 && SecondThrow == null)
                    return (int)StatusUtility.FrameStatus.WaitingOnSecondThrow;
                //need to implement third throw logic here in the future
                if (FirstThrow < 10 && FirstThrow + SecondThrow == 10)
                    return (int)StatusUtility.FrameStatus.Spare;
                if (FirstThrow != null && SecondThrow != null)
                    return (int)StatusUtility.FrameStatus.Completed;
                return (int)StatusUtility.FrameStatus.Error;
            }
        }      

        public int? GetScoreForFrame(Frame[] thisFramesArray)
        {
            if (scoreForFrame != null)
                return scoreForFrame;
            if(FrameNumber == 10)
            {
                scoreForFrame = FirstThrow + (SecondThrow == null ? 0 : SecondThrow) + (ThirdThrow == null ? 0 : ThirdThrow);
                return FirstThrow + (SecondThrow == null ? 0 : SecondThrow) + (ThirdThrow == null ? 0 : ThirdThrow);
            }
            if (FrameNumber == 9 && Status == (int)StatusUtility.FrameStatus.Strike)
            {
                var nextFrame = thisFramesArray[FrameNumber];
                if(nextFrame != null)
                {
                    var nextRoundTotal = (nextFrame.FirstThrow ?? 0) + (nextFrame.SecondThrow ?? 0);
                    scoreForFrame = 10 + nextRoundTotal;
                    return 10 + nextRoundTotal;
                }
                scoreForFrame = FirstThrow + (SecondThrow == null ? 0 : SecondThrow) + (ThirdThrow == null ? 0 : ThirdThrow);
                return FirstThrow + (SecondThrow == null ? 0 : SecondThrow) + (ThirdThrow == null ? 0 : ThirdThrow);
            }
            switch (Status)
            {
                case (int)StatusUtility.FrameStatus.WaitingOnFirstThrow:
                    return null;
                case (int)StatusUtility.FrameStatus.WaitingOnSecondThrow:
                    return FirstThrow;
                case (int)StatusUtility.FrameStatus.Spare:
                    if (thisFramesArray[FrameNumber] == null) //the next frame
                        return null;
                    if (thisFramesArray[FrameNumber].FirstThrow != null)
                    {
                        scoreForFrame = 10 + thisFramesArray[FrameNumber].FirstThrow;
                        return 10 + thisFramesArray[FrameNumber].FirstThrow;
                    }
                    break;
                case (int)StatusUtility.FrameStatus.Strike:
                    if (thisFramesArray[FrameNumber] != null
                        && thisFramesArray[FrameNumber].Status == (int)StatusUtility.FrameStatus.Spare)
                    {
                        scoreForFrame = 20;
                        return 20;
                    }
                    if (thisFramesArray[FrameNumber] != null 
                        && 
                        thisFramesArray[FrameNumber + 1] != null 
                        && 
                        thisFramesArray[FrameNumber].Status == (int)StatusUtility.FrameStatus.Strike 
                        &&
                        thisFramesArray[FrameNumber + 1].Status == (int)StatusUtility.FrameStatus.Strike)
                    {
                        scoreForFrame = 30;
                        return 30;
                    }
                    if (thisFramesArray[FrameNumber] == null)
                    {
                        scoreForFrame = null;
                        return null;
                    }
                    if (thisFramesArray[FrameNumber] != null && thisFramesArray[FrameNumber].Status == (int)StatusUtility.FrameStatus.Strike && thisFramesArray[FrameNumber + 1] == null)
                        return null;

                    scoreForFrame = 10 + (thisFramesArray[FrameNumber].GetScoreForFrame(thisFramesArray) ?? 0);
                    return 10 + thisFramesArray[FrameNumber].GetScoreForFrame(thisFramesArray);
                default:
                    scoreForFrame = FirstThrow + SecondThrow;
                    return FirstThrow + SecondThrow;
            }

            return null;
                 
        }
    }
}
