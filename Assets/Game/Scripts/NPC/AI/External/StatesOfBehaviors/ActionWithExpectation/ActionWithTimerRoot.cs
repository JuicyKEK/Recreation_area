using Game.Scripts.NPC.AI.External.NPC;

namespace Game.Scripts.NPC.HSMBase.ActionWithExpectation
{
    public class ActionWithTimerRoot : HSMState
    {
        public readonly HSMState MoveToAction;
        public readonly HSMState ActionWithTimer;
        public readonly HSMState AfterAction;
        
        private NPCContext _context;
        
        public ActionWithTimerRoot(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, null)
        {
            _context = context;
            MoveToAction = new MoveToAction(machine, _context, this);
            ActionWithTimer = new ActionWithTimer(machine, _context, this);
            AfterAction = new AfterAction(machine, _context, this);
        }

        protected override HSMState GetInitialState() => MoveToAction;
        protected override HSMState GetTransition() => _context.CurrentState == NPCStates.InProgress ? ActionWithTimer : null; 
        //стоит переделать под дошел ли нпс
    }
}