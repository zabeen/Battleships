using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace Battleships.ExamplePlayer
{
    public static class SquareCalculator
    {
        public static GridSquare GetRandomGridSquare()
        {
            Random random = new Random();
            char randomRow = (char)random.Next(GameRules.FIRST_ROW, GameRules.LAST_ROW);
            int randomCol = random.Next(GameRules.FIRST_COL, GameRules.LAST_COL + 1);

            return new GridSquare(randomRow, randomCol);
        }

        public static ShipPosition GetShipPosition(char startRow, int startColumn, char endRow, int endColumn)
        {
            return new ShipPosition(new GridSquare(startRow, startColumn), new GridSquare(endRow, endColumn));
        }

        public static bool IsSquareOnBoard(GridSquare square)
        {
            // as long as all following conditions true, square is within board boundaries
            return (square.Row >= GameRules.FIRST_ROW && square.Row <= GameRules.LAST_ROW && square.Column >= GameRules.FIRST_COL && square.Column <= GameRules.LAST_COL);
        }

        public static List<GridSquare> GetSurroundingSquares(GridSquare square)
        {
            List<GridSquare> squares = new List<GridSquare>();

            // iterate +/- row/col around square, - end result will cover original square
            for (char row = (char)(square.Row - 1); row <= (char)(square.Row + 1); row++)
            {
                for (int col = square.Column - 1; col <= square.Column + 1; col++)
                {
                    squares.Add(new GridSquare(row, col));
                }
            }

            // remove original square
            squares.RemoveAll(s => s.Equals(square));

            // return distinct list
            return squares.Distinct().ToList();
        }

    }
}
