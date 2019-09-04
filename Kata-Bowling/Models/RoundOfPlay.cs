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
        public int Round { get; set; }
        private Frame[] frames { get; set; }
        public Frame[] Frames
        {
            get { return frames; }
            set
            {
                frames = value;                
            }
        }
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
            foreach (var frame in Frames)
            {
                if (frame == null)
                    break;

                total += frame.GetScoreForFrame(frames) != null ? frame.GetScoreForFrame(frames) : 0;
            }
            return total;
        }

        public RoundOfPlay(Player player, int round)
        {
            Player = player;
            Round = round;
            frames = new Frame[10];
        }
    }
}
