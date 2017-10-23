using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace Battleships.ExamplePlayer
{
    public class MoveCalculator
    {
        public readonly Dictionary<GridSquare, SquareStats> MovesMade = new Dictionary<GridSquare, SquareStats>();
        internal IGridSquare LastTarget;

        public MoveCalculator()
        {
            for (char c = GameRules.FIRST_ROW; c <= GameRules.LAST_ROW; c++)
            {
                for (int i = GameRules.FIRST_COL; i <= GameRules.LAST_COL; i++)
                {
                    MovesMade.Add(new GridSquare(c, i), new SquareStats());
                }
            }
        }

        public IGridSquare GetNextTarget()
        {
            if (LastTarget == null)
            {
                return new GridSquare('A', 1);
            }

            var row = LastTarget.Row;
            var col = LastTarget.Column + 1;
            if (LastTarget.Column != 10)
            {
                return new GridSquare(row, col);
            }

            row = (char)(row + 1);
            if (row > 'J')
            {
                row = 'A';
            }
            col = 1;

            GridSquare nextTarget = new GridSquare(row, col);
            LastTarget = nextTarget;

            return nextTarget;
        }

    }
}
