using MontyHall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MontyHall.Program;

namespace MontyHall
{    
    public class MontyService 
    {
        public virtual montyModel PlayGame(string strategyChoice, int numberOfSimulations)
        {
            montyModel monty = new montyModel();
            try
            {
                MontyHallGame montyHallGame = new MontyHallGame();  
                MontyHallGame.Strategy strategy = MontyHallGame.Strategy.Switch;
                switch (strategyChoice)
                {
                    case "A":
                    case "a":
                        strategy = MontyHallGame.Strategy.Switch;
                        break;
                    case "B":
                    case "b":
                        strategy = MontyHallGame.Strategy.Keep;
                        break;
                    case "C":
                    case "c":
                        strategy = MontyHallGame.Strategy.None;
                        break;
                    default: throw new InvalidDataException();
                }
                List<Result> results = new List<Result>();
                for (int i = 0; i < numberOfSimulations; i++)
                {
                    results.Add(montyHallGame.Play(strategy));
                }
                // Count the number of wins
                var winCountList = from w in results
                                   where w.ContestantWins
                                   select w;

                var switchDoorCountList = from s in results
                                          where s.ContestantSwitchedDoor
                                          select s;

               
                monty.switchedDoorCount = switchDoorCountList.Count(dcl => dcl.ContestantSwitchedDoor);
                monty.totalWinCount = winCountList.Count(cl => cl.ContestantWins);
                monty.winPercentage = (decimal)monty.totalWinCount / (decimal)numberOfSimulations;
                monty.validModel = 0;
                return monty;
            }
            catch (DivideByZeroException exec)
            {
                Console.WriteLine(exec);
                monty = new montyModel();
                monty.validModel = 1;
                return monty;
            }
            catch (InvalidDataException exec)
            {
                Console.WriteLine("Please enter correct Strategy option A, B or C");
                monty = new montyModel();
                monty.validModel = 1;
                return monty;
            }
            catch (Exception exec)
            {
                Console.WriteLine(exec);
                monty = new montyModel();
                monty.validModel = 1;
                return monty;
            }

        }
    }  

}
