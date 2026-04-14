using CrapsLibrary;
using CrapsLibrary.TableState;

namespace ConsoleAppForCraps
{
    internal class CrapsCLI
    {
        static void Main(string[] args)
        {
            DealerCLI dealer = new DealerCLI();



            /////////////////////////////////////////////////////////////
            var tableStateMachine = new TableStateMachine();
            tableStateMachine.ChangeTableState(new TableStateIdling(tableStateMachine));



            // you need a way to interpret user input
            // this could be some commandInterpreter object which hosts a switch
            // the switch then maps an input int to an ICommand (or just switch on the object)
            // that ICommand is the parameter for the PushCommand(ICommand iCommand) of the current state
            // e.g. do some stuff, then change to the next state



            /////////////////////////////////////////////////////////////


            // initialize table (with minimum bet)
            CrapsTable Table01 = new(5, 5);


            // player buys chips (can buy more at any time) starts with 100 by default

            // 1 player creates bets

            // TODO betting closed

            // dice rolled

            // kill bets, pay bets, flip pick, pause place bets (if puck switches to off)

            // cycle through players, (back to 1)

            Player player1 = new Player("Chase");
            player1.purse += 100;

            Table01.RollDice(6, 6);

            foreach (betType bet in Enum.GetValues<betType>())
            {
                Bet? newBet = CrapsTable.betFactory.CreateBet(player1, bet, 6);
                if (newBet != null)
                //if (newBet != null && newBet.betName != "PassBet")
                    player1.playerBetList.Add(newBet);
            }

            for (int i = 0; i < 100; i++)
            {
                Table01.RollDice(6, 6);
                Console.WriteLine($"{player1.playerName} now has {player1.purse} credits.");
            }

        }
    }
}
