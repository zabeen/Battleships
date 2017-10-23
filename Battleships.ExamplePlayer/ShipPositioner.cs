using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace Battleships.ExamplePlayer
{
    public class ShipPositioner
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
                    shipPositions.Add(SquareCalculator.GetShipPosition(newShip[0].Row, newShip[0].Column, newShip[size - 1].Row, newShip[size - 1].Column));

                    // extend list of impemissible spaces
                    ExtendImpermissibleSquaresList(newShip);

                }
            }
            while (shipPositions.Count < rules.ShipSizes.Length);


            return shipPositions;
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
                GridSquare startingSq = SquareCalculator.GetRandomGridSquare();

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
                    if (_impermissibleSpaces.Contains(newSquare) || !SquareCalculator.IsSquareOnBoard(newSquare))
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

        private void ExtendImpermissibleSquaresList(List<GridSquare> ship)
        {
            List<GridSquare> squares = new List<GridSquare>();

            foreach (GridSquare square in ship)
            {
                // add this square
                squares.Add(square);

                // add surrounding squares
                squares.AddRange(SquareCalculator.GetSurroundingSquares(square));

            }

            // add distinct values to impermissible spaces list
            _impermissibleSpaces.AddRange(squares.Distinct().ToList());
        }

    }
}
