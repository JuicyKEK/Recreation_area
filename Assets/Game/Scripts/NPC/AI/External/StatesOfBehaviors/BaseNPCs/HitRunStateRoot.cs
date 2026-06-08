using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs.HitRunStates;
using Game.Scripts.NPC.HSMBase;
using Game.Scripts.NPC.NPC.External.NPCConstants;
using UnityEngine;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs
{
    public class HitRunStateRoot : HSMState
    {
        private readonly HSMState _combatRunState;
        private readonly HSMState _combatHitState;
        private readonly NPCContext _context;
        
        public HitRunStateRoot(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, parent)
        {
            _context = context;
            _combatRunState = new CombatRunState(machine, _context, this);
            _combatHitState = new CombatHitState(machine, _context, this);
        }
        
        protected override HSMState GetInitialState() => _combatHitState;
        protected override HSMState GetTransition()
        {
            if (_context.CurrentState != NPCStates.InDanger)
            {
                return Parent;
            }
            
            if (_context.Personality.StartLevelOfCourage >= NPCConstants.NpcMaxLevelOfCourage)
            {
                return null;
            }
            
            if (_context.Personality.StartLevelOfCourage <= NPCConstants.NpcMinLevelOfCourageToFight
                || _context.HPLevel <= 
                _context.Personality.MaxHP * (1 - Mathf.Clamp(_context.HPLevel, 0, _context.Personality.MaxHP)))
            {
                return _combatRunState;
            }
            
            return null;
        }
    }
}