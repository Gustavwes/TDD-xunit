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
        public int? FirstThrow {
            get
            {
                return FirstThrow;
            }
            set
            {
                if(value < 0)
                    throw new InvalidOperationException("Negatives not allowed: " + value);
                if(value > 10)
                    throw new InvalidOperationException("Values over 10 not allowed: " + value);
            }
        }
        public int? SecondThrow {
            get
            {
                return SecondThrow;
            }
            set
            {
                if (value < 0)
                    throw new InvalidOperationException("Negatives not allowed: " + value);
                if (value > 10)
                    throw new InvalidOperationException("Values over 10 not allowed: " + value);
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
        public int? Total
        {
            get
            {
                if (FirstThrow == null)
                    return 0;
                if ((SecondThrow) == null)
                    return FirstThrow;
                return FirstThrow + SecondThrow;
            }
        }

        public void PlayFrame()
        {

        }
    }
}
