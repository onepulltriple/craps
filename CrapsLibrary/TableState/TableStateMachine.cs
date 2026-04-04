using System.Windows.Input;

namespace CrapsLibrary.TableState
{
    public class TableStateMachine // the engine
        // holds the current state, controls transitions, drives updates
    {
        private TableState currentTableState; // no default starting state

        public void ChangeTableState(TableState newTableState)
        {
            this.currentTableState?.Exit();
            this.currentTableState = newTableState;
            this.currentTableState.Enter();
        }

        public void Update()
        {
            this.currentTableState?.Update(); // calls update on the current state
        }

        public void PushCommand(ICommand iCommand)
        {
            this.currentTableState?.PushCommand(iCommand); // let's the state itself decide what comes next
        }
    }
}
