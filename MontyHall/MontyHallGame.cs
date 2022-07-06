using MontyHall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    public class MontyHallGame
    {
        public enum Strategy
        {
            Switch,
            Keep,
            None
        }

        ///<summary>
        /// Simulates one game of the Monty Hall Game.
        ///</summary>
        ///<param name="strategy">Determines whether or not the player would like to switch doors.</param>
        public virtual Result Play(Strategy strategy)
        {

            // Create three doors
            Door door1 = new Door { Number = Door.DoorNumber.Door1 };
            Door door2 = new Door { Number = Door.DoorNumber.Door2 };
            Door door3 = new Door { Number = Door.DoorNumber.Door3 };

            // Create a list of the doors so we can implement LINQ
            List<Door> doors = new List<Door>();
            doors.Add(door1);
            doors.Add(door2);
            doors.Add(door3);

            // Randomly set the winning door
            int winningDoorNumber = new Random(Guid.NewGuid().GetHashCode()).Next(1, 4);
            doors[winningDoorNumber - 1].IsWinningDoor = true;

            // Contestant picks a random door
            int firstChoiceDoorNumber = new Random(Guid.NewGuid().GetHashCode()).Next(1, 4);
            doors[firstChoiceDoorNumber - 1].ContestantFirstChoice = true;

            // Set the door that is revealed by Monty
            // The revealed door must:
            // 1) Not be a winning door
            // 2) Not be the contestant’s first choice door
            foreach (Door d in doors)
            {
                if (!d.ContestantFirstChoice && !d.IsWinningDoor)
                {
                    d.MontySelected = true;
                    break; // Break out to avoid Monty from selecting multiple doors (i.e. the case where the contestant’s first choice is the winning door.
                }
            }

            // If the strategy of this game is indifference to switching or keeping the intial pick, then randomly pick to switch or keep on behalf of the user.
            if (strategy == Strategy.None)
                strategy = (Strategy)new Random(Guid.NewGuid().GetHashCode()).Next(0, 2);

            if (strategy == Strategy.Switch)
            {
                foreach (Door d in doors)
                {
                    if (!d.ContestantFirstChoice && !d.MontySelected)
                    {
                        d.ContestantSecondChoice = true;
                        break; // Break out to avoid more than one second choice selections
                    }
                }
            }
            else if (strategy == Strategy.Keep)
            {
                // Contestant doesn’t want to change door
                foreach (Door d in doors)
                {
                    if (d.ContestantFirstChoice)
                        d.ContestantSecondChoice = d.ContestantFirstChoice;
                }
            }

            // Results
            Result result = new Result();
            foreach (Door d in doors)
            {
                if (d.ContestantSecondChoice)
                {
                    result.ContestantSwitchedDoor = !d.ContestantFirstChoice;
                    result.ContestantWins = d.IsWinningDoor;
                }
            }
            return (result);
        }
    }
}
