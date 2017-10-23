using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.ExamplePlayer
{
    public class SquareStats
    {
        public List<SquareResult> ResultsSoFar = new List<SquareResult>();

        public int HitCount
        {
            get { return ResultsSoFar.Where(r => r.WasHit).Count(); }
        }

        public void AddNewResult(int roundId, bool wasHit)
        {
            ResultsSoFar.Add(new SquareResult() { RoundId = roundId, WasHit = wasHit });
        }

        public class SquareResult
        {
            public int RoundId { get; set; }
            public bool WasHit { get; set; }
        }
    }

}
