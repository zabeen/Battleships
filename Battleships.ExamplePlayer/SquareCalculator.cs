using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace Battleships.ExamplePlayer
{
    public class SquareCalculator
    {
        List<GridSquare> _impermissibleSpaces = new List<GridSquare>();

        public List<IShipPosition> GetShipPositions()
        {
            List<IShipPosition> shipPositions = new List<IShipPosition>();
            GameRules rules = new GameRules();

            do
            {
                // for each ship size as outlined in game rules
                foreach (int size in rules.ShipSizes)
                {
                    // Build a new ship according to required size
                    List<GridSquare> newShip = BuildShip(size);

                    // add new ship position to list
                    shipPositions.Add(GetShipPosition(newShip[0].Row, newShip[0].Column, newShip[size - 1].Row, newShip[size - 1].Column));

                    // extend list of impemissible spaces
                    ExtendImpermissibleSquaresList(newShip);

                }
            }
            while (shipPositions.Count < rules.ShipSizes.Length);


            return shipPositions;
        }

        public GridSquare GetRandomGridSquare()
        {
            Random random = new Random();
            char randomRow = (char)random.Next(GameRules.FIRST_ROW, GameRules.LAST_ROW);
            int randomCol = random.Next(GameRules.FIRST_COL, GameRules.LAST_COL + 1);

            return new GridSquare(randomRow, randomCol);
        }

        private ShipPosition GetShipPosition(char startRow, int startColumn, char endRow, int endColumn)
        {
            return new ShipPosition(new GridSquare(startRow, startColumn), new GridSquare(endRow, endColumn));
        }

        private List<GridSquare> BuildShip(int shipSize)
        {
            bool isInvalidShip = true;
            List<GridSquare> thisShip = new List<GridSquare>();

            do
            {
                // clear list in case not first iteration of loop
                thisShip.Clear();

                // get starting square
                GridSquare startingSq = GetRandomGridSquare();

                // randomly select if ship will be placed Hor or Ver
                // if rand %2 == 0, then h, else v
                Random random = new Random();
                int direction = random.Next(0, 10000);

                // build ship
                char row = startingSq.Row;
                int col = startingSq.Column;
                for (int i = 0; i < shipSize; i++)
                {
                    GridSquare newSquare = new GridSquare(row, col);

                    // Rules - if any broken, then jump to 'outer' marker
                    // 1: no part of Ship can overlap existing ships or their surrounding squares ("impermissible spaces")
                    // 2: if Square is not on board
                    if (_impermissibleSpaces.Contains(newSquare) || !IsSquareOnBoard(newSquare))
                    {
                        isInvalidShip = true;
                        break;
                    }
                    else
                    {
                        isInvalidShip = false;
                        thisShip.Add(newSquare);
                        if (direction % 2 == 0)
                            col++;
                        else
                            row++;
                    }
                }
            }
            while (isInvalidShip);

            return thisShip;
        }

        private bool IsSquareOnBoard(GridSquare square)
        {
            // as long as all following conditions true, square is within board boundaries
            return (square.Row >= GameRules.FIRST_ROW && square.Row <= GameRules.LAST_ROW && square.Column >= GameRules.FIRST_COL && square.Column <= GameRules.LAST_COL);
        }

        private void ExtendImpermissibleSquaresList(List<GridSquare> ship)
        {
            List<GridSquare> squares = new List<GridSquare>();

            foreach (GridSquare sq in ship)
            {
                // impermissible squares are those +/- row/col of each square, as well as the original square
                for (char row = (char)(sq.Row - 1); row <= (char)(sq.Row + 1); row++)
                {
                    for (int col = sq.Column - 1; col <= sq.Column + 1; col++)
                    {
                        squares.Add(new GridSquare(row, col));
                    }
                }
            }

            // there will be overlap so return a distinct list
            _impermissibleSpaces.AddRange(squares.Distinct().ToList());
        }
    }
}
