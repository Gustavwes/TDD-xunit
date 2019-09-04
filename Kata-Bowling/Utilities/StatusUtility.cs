using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata_Bowling.Utilities
{
    public class StatusUtility
    {
        public enum FrameStatus
        {
            Error = 0,
            WaitingOnFirstThrow = 1,
            WaitingOnSecondThrow = 2,
            WatingOnThirdThrow = 3,
            Spare = 4,
            Strike = 5,
            Completed = 6
        }
    }
}
