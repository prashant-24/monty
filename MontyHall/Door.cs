using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    public class Door
    {
        bool contestantFirstChoice;
        bool contestantSecondChoice;
        bool montySelected;
        public DoorNumber Number { get; set; }
        public enum DoorNumber
        {
            Door1 = 1,
            Door2,
            Door3
        }
        public bool ContestantFirstChoice
        {
            get
            {
                return (this.contestantFirstChoice);
            }
            set
            {
                this.contestantFirstChoice = value;

                if (this.contestantFirstChoice && this.montySelected)
                    throw new Exception("Contestant’s first choice cannot be the door that Monty revealed!");

            }
        }

        public bool ContestantSecondChoice
        {
            get
            {
                return (this.contestantSecondChoice);
            }
            set
            {
                this.contestantSecondChoice = value;
                if (this.contestantSecondChoice && this.montySelected)
                    throw new Exception("Contestant’s second choice cannot be the door that Monty revealed!");
            }
        }
        public bool MontySelected
        {
            get
            {
                return (this.montySelected);
            }
            set
            {
                this.montySelected = value;

                if (this.contestantSecondChoice && this.montySelected)
                    throw new Exception("Monty cannot reveal the contestant’s first choice door!");

                if (this.IsWinningDoor && this.montySelected)
                    throw new Exception("Monty cannot reveal the winning door!");

            }
        }
        public bool IsWinningDoor { get; set; }
    }
}
