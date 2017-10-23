using System.Linq;
using System.Collections.Generic;
using Battleships.Player.Interface;

namespace Battleships.ExamplePlayer
{
    public class ZpotBot : IBattleshipsBot
    {
        internal IGridSquare LastTarget;

        private int RoundId = 0;
        private readonly Dictionary<GridSquare, SquareStats> movesMade = new Dictionary<GridSquare, SquareStats>();

        public ZpotBot()
        {
            // initialise movesMade dict
            for (char c = GameRules.FIRST_ROW; c <= GameRules.LAST_ROW; c++)
            {
                for (int i = GameRules.FIRST_COL; i <= GameRules.LAST_COL; i++)
                {
                    movesMade.Add(new GridSquare(c, i), new SquareStats());
                }
            }
        }

        public string Name
        {
            get { return "ZpotBot"; }
        }

        public IEnumerable<IShipPosition> GetShipPositions()
        {
            // As this method is called at start of each new round
            // increment Round ID at this point
            RoundId++;

            // get new positions
            ShipPositioner positioner = new ShipPositioner();

            return positioner.GetShipPositions();
        }

        public IGridSquare SelectTarget()
        {
            var nextTarget = GetNextTarget();
            LastTarget = nextTarget;
            return nextTarget;
        }

        public void HandleShotResult(IGridSquare square, bool wasHit)
        {
            // add result to movesMade dict
            movesMade[(GridSquare)square].AddNewResult(RoundId, wasHit);
        }

        public void HandleOpponentsShot(IGridSquare square) { }

        private static ShipPosition GetShipPosition(char startRow, int startColumn, char endRow, int endColumn)
        {
            return new ShipPosition(new GridSquare(startRow, startColumn), new GridSquare(endRow, endColumn));
        }

        private IGridSquare GetNextTarget()
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
            return new GridSquare(row, col);
        }
    }
}