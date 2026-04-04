namespace CrapsLibrary.TableState
{
    public class TableStateIdling : TableState
    {
        public TableStateIdling(TableStateMachine tableStateMachine) : base(tableStateMachine) { }

        public override void Enter()
        {
            Console.WriteLine("Entering Idle");
            Console.ReadLine(); // interpret user input
            // change states, or do other actions
            // input from the outside should get pushed intot he state machine (i.e. a state object with some parameters gets pushed into the state machine
        }

        public override void Update() // this approach "pulls" information
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

        public override void PushCommand(ICommand iCommand) // for handling of commands for this state
            // only objects of type ICommand can be accepted
            // may have a return object if it makes sense to
        {
            // the implementation goes here
            // e.g. placing a bet

            // then change the state of TableStaeMachine to a different state
            //  tableStateMachine.ChangeTableState(new TableStateAcceptingPlayers(tableStateMachine));
        }
    }
}
