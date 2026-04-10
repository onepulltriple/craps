using System.Transactions;

namespace ConsoleAppForCraps.DealerCLIState
{
    abstract class DealerCLIState
    {
        protected DealerCLIStateMachine dealerCLIStateMachine; // the garage

        public DealerCLIState(DealerCLIStateMachine dealerCLIStateMachine)
        {
            this.dealerCLIStateMachine = dealerCLIStateMachine; // one of several cars
        }

        public abstract void Enter();
        public abstract void PerformTask(int input);
        public abstract void Exit();

        public int ValidateUserInput(List<int> listOfAcceptableInts)
        {
            bool isInt;
            int result;

            do {
                Console.Write("Please enter choice: ");
                string? input = Console.ReadLine();
                isInt = int.TryParse(input, out result);

            } while (!(isInt && listOfAcceptableInts.Contains(result)));

            return result;
        }
    }
}
