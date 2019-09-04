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
                if (value < 0)
                    throw new InvalidOperationException("Negatives not allowed: " + value);
                if (value > 10)
                    throw new InvalidOperationException("Values over 10 not allowed: " + value);

                secondThrow = value;
            }
        }
        private int? thirdThrow
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
                thirdThrow = value;
            }
        }

        public string Status
        {
            get
            {
                if (FirstThrow == 10)
                    return "Strike";
                if (FirstThrow == null)
                    return "Awaiting first throw for frame";
                if (FirstThrow < 10 && FirstThrow + SecondThrow == 10)
                    return "Spare";
                return null;
            }
        }      

        public int? GetScoreForFrame(Frame[] thisFramesArray)
        {
            
            if (FirstThrow == null)
                return null;
            if ((SecondThrow) == null && FirstThrow != 10)
                return FirstThrow;
            if(FirstThrow != 10 && FirstThrow + SecondThrow == 10) //if we got a spare
            {
                //return null if next throw is empty (as score can't be calculated yet)
                if (thisFramesArray[FrameNumber] == null) //the next frame
                    return null;
                if (thisFramesArray[FrameNumber].firstThrow != null)
                    return 10 + thisFramesArray[FrameNumber].firstThrow;

            }
            if(FirstThrow == 10)
            {
                if (thisFramesArray[FrameNumber] != null && thisFramesArray[FrameNumber + 1] != null && thisFramesArray[FrameNumber].Status == "Strike" && thisFramesArray[FrameNumber + 1].Status == "Strike")
                    return 30;
                if (thisFramesArray[FrameNumber] == null || thisFramesArray[FrameNumber].GetScoreForFrame(thisFramesArray) == null) //the next frame hasn't been calculated yet
                    return null;

                return 10 + thisFramesArray[FrameNumber].GetScoreForFrame(thisFramesArray);

            }
                //then implement strike here, can potentially get more complicated
            return FirstThrow + SecondThrow;           
        }
    }
}
