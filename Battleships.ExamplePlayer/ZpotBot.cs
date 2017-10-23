using System.Linq;
using System.Collections.Generic;
using Battleships.Player.Interface;

namespace Battleships.ExamplePlayer
{
    public class ZpotBot : IBattleshipsBot
    {
        private int RoundId = 0;
        private MoveCalculator MoveCalc = new MoveCalculator();

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
            return MoveCalc.GetNextTarget();
        }

        public void HandleShotResult(IGridSquare square, bool wasHit)
        {
            // add result to movesMade dict
            MoveCalc.MovesMade[(GridSquare)square].AddNewResult(RoundId, wasHit);
        }

        public void HandleOpponentsShot(IGridSquare square) { }


    }
}