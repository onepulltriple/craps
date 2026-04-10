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

        public int ValidateUserInputCLIMenu(List<int> listOfAcceptableInts)
        {
            bool isInt;
            int result;

            do {
                Console.Write("\nPlease enter choice: ");
                string? input = Console.ReadLine();
                isInt = int.TryParse(input, out result);

            } while (!(isInt && listOfAcceptableInts.Contains(result)));

            return result;
        }

        public uint ValidateUserInputUInt()
        {
            bool isUInt;
            uint result;

            do
            {
                Console.Write("Please enter an amount to credit to the player (enter a positive whole number or 0 to abort): ");
                string? input = Console.ReadLine();
                isUInt = uint.TryParse(input, out result);

            } while (!isUInt);

            return result;
        }

        public void SleepCLI(int milliseconds = DealerCLI.sleepDurationMilliseconds)
        {
            Thread.Sleep(milliseconds);
        }


    }
}
