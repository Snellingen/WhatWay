using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Score : IComparable<Score> 
    {

        public int Points { get; set; }
        public int Count { get; set; }
        public int Time { get; set; }
        public Score() : this(0,0,0) {}
        public Score(int points, int count, int time)
        {
            Points = points;
            Count = count;
            Time = time; 
        }
        public int CompareTo(Score other)
        {
            return Points.CompareTo(other.Points);
        }
    }
}
