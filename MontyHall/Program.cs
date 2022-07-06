using MontyHall.Model;
using System;

using System.Collections.Generic;

using System.Linq;



namespace MontyHall
{
    public class Program
    {
        public static void Main()
        {
            bool playGame = true;
            while (playGame)
            {
                Console.WriteLine("What strategy do you want to use?\r\nA) Always switch the door\r\nB) Never switch the door\r\nC) Both");
                string strategyChoice = "C";
                strategyChoice = Console.ReadLine();
                int numberOfSimulations = 100;

                Console.WriteLine("How many simulations do you want to run ?");
                if (!int.TryParse(Console.ReadLine(), out numberOfSimulations))
                    numberOfSimulations = 100;
                MontyService montyService = new MontyService();
                montyModel monty = montyService.PlayGame(strategyChoice, numberOfSimulations);
                if (monty.validModel == 0)
                {
                    Console.WriteLine("\r\n * ***************************************\r\nMonty Hall Problem Simulation Results\r\n * ***************************************\r\n");
                    Console.WriteLine($"Total Simulations…….. { numberOfSimulations}");
                    Console.WriteLine($"Total Wins…………… { monty.totalWinCount}");
                    Console.WriteLine($"Win Percentage……….. { monty.winPercentage.ToString("0.0 %")}");
                    Console.WriteLine($"Total Switched Doors….. { monty.switchedDoorCount}");
                    Console.WriteLine("\r\n\r\n");
                }
                else if (monty.validModel == 1)
                {
                    Console.WriteLine("\r\n * ***************************************\r\nMonty Hall Problem Simulation Results is null\r\n * ***************************************\r\n");
                }

                Console.WriteLine("Hit any key to continue or enter ‘Exit’ to quit.");

                switch (Console.ReadLine())
                {
                    case "exit":
                    case "Exit":
                    case "EXIT":
                    case "quit":
                    case "QUIT":
                    case "Quit":
                        playGame = false;
                        break;
                }
            }
        }
      
    }

}