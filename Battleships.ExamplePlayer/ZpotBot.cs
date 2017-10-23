﻿using System.Linq;
using System.Collections.Generic;


namespace Battleships.ExamplePlayer
{
    using Battleships.Player.Interface;
    using System.Collections.Generic;

    public class ZpotBot : IBattleshipsBot
    {
        internal IGridSquare LastTarget;

        private int BattleId = 0;
        private readonly Dictionary<GridSquare, SquareStats> movesMade = new Dictionary<GridSquare, SquareStats>();

        public ZpotBot()
        {
            // initialise movesMade dict
            for (char c = 'A'; c < 'K'; c++)
            {
                for (int i = 1; i < 11; i++)
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
            // this method is called at start of each new battle
            // increment battle ID at this point
            BattleId++;

            return new List<IShipPosition>
                   {
                       GetShipPosition('A', 1, 'A', 5),
                       GetShipPosition('C', 1, 'C', 4),
                       GetShipPosition('E', 1, 'E', 3),
                       GetShipPosition('G', 1, 'G', 3),
                       GetShipPosition('I', 1, 'I', 2)
                   };
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
            movesMade[(GridSquare)square].AddNewResult(BattleId, wasHit);
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