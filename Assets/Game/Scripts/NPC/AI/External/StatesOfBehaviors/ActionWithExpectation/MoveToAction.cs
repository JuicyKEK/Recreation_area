using Game.Scripts.NPC.AI.External.NPC;

namespace Game.Scripts.NPC.HSMBase.ActionWithExpectation
{
    public class MoveToAction: HSMState
    {
        private readonly NPCContext _context;
        public MoveToAction(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, null)
        {
            _context = context;
        }

        protected override void OnEnter()
        {
            if (_context.TargetObject != null)
            {
                //TODO: Animation move start
                _context.Agent.SetDestination(_context.TargetObject.position); //А может ActionObject передавать через SO?
            }
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_context.Agent.remainingDistance <= _context.Personality.ArmLength)
            {
                _context.Agent.isStopped = true;
                _context.CurrentState = NPCStates.InProgress;
                GetTransition();
            }
        }

        protected override HSMState GetTransition() => _context.CurrentState == NPCStates.InProgress ? 
            ((ActionWithTimerRoot)Parent).ActionWithTimer : null;
        //стоит переделать под дошел ли нпс
    }
}