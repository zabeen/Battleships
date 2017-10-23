using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.ExamplePlayer
{
    public class GameRules
    {
        public const char FIRST_ROW = 'A';
        public const char LAST_ROW = 'J';
        public const int FIRST_COL = 1;
        public const int LAST_COL = 10;

        public int[] ShipSizes
        {
            get
            {
                return new int[]{ 5, 4, 3, 3, 2 };
            }
        }

    }
}
