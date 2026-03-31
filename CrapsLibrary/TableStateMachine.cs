namespace CrapsLibrary
{
    public class TableStateMachine // the engine
        // holds the current state, controls transitions, drives updates
    {
        private TableState currentTableState;

        public void ChangeTableState(TableState newTableState)
        {
            currentTableState?.Exit();
            currentTableState = newTableState;
            currentTableState.Enter();
        }

        public void Update()
        {
            currentTableState?.Update();
        }
    }
}
