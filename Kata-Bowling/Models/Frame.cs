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
        public int? FirstThrow { get; set; }
        public int? SecondThrow { get; set; }
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
        public int? Total
        {
            get
            {
                return FirstThrow + SecondThrow;
            }
        }

        public void PlayFrame()
        {

        }
    }
}
