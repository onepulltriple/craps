namespace CrapsLibrary
{
    public class TableStateAcceptingPlayers : TableState
    {
        public TableStateAcceptingPlayers(TableStateMachine tableStateMachine) : base(tableStateMachine) { }

        public override void Enter()
        {
            Console.WriteLine("Entering 'Accepting Players'");
        }

        public override void Update()
        {
            Console.WriteLine("Running...");

            if (!Console.KeyAvailable)
            {
                tableStateMachine.ChangeTableState(new TableStateIdling(tableStateMachine));
            }
        }

        public override void Exit()
        {
            Console.WriteLine("Exiting 'Accepting Players'");
        }
    }
}
