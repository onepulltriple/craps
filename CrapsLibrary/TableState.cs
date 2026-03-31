namespace CrapsLibrary
{
    public abstract class TableState // interchangeable behavior models (this would be the default behavior, but nothing is implemented here right now)
    {
        protected TableStateMachine tableStateMachine;

        public TableState(TableStateMachine tableStateMachine)
        {
            this.tableStateMachine = tableStateMachine;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
