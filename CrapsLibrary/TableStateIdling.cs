namespace CrapsLibrary
{
    public class TableStateIdling : TableState
    {
        public TableStateIdling(TableStateMachine tableStateMachine) : base(tableStateMachine) { }

        public override void Enter()
        {
            Console.WriteLine("Entering Idle");
        }

        public override void Update()
        {
            Console.WriteLine("Idle...");

            if (Console.KeyAvailable)
            {
                tableStateMachine.ChangeTableState(new TableStateAcceptingPlayers(tableStateMachine));
            }
        }

        public override void Exit()
        {
            Console.WriteLine("Exiting Idle");
        }
    }
}
