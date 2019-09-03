using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata_Bowling.Models
{
    public class RoundOfPlay
    {
        public Player Player { get; set; }
        public List<Frame> Frames { get; set; }
        public int? TotalScore
        {
            get
            {
                return GetTotalScore();
            }
        }

        private int? GetTotalScore()
        {
            int? total = 0;
            foreach(var frame in Frames)
            {
                total += frame?.Total;
            }
            return total;
        }
    }
}
