using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;
using Game.Scripts.NPC.NPC.External.NPCConstants;
using UnityEngine;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs
{
    public class FollowingState : HSMState
    {
        private const float _degreeCircle = 360f;
        private const float _turningSpeedCoefficient = 60f;
        
        private readonly NPCContext _context;
        
        public FollowingState(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, parent)
        {
            _context = context;
        }
        
        protected override void OnEnter()
        {
            Debug.Log("Entered FollowingState");
            
            if (_context.TargetObject != null && _context.Agent != null)
            {
                _context.Agent.isStopped = false;
                _context.Agent.speed = _context.Personality.SpeedMove;
                _context.Agent.angularSpeed = _degreeCircle;
                _context.Agent.stoppingDistance = _context.Personality.ArmLength;
                _context.Agent.SetDestination(_context.TargetObject.position);
                //TODO: Анимация ходьбы
            }
            else if (_context.Agent != null)
            {
                _context.Agent.isStopped = true;
            }
        }

        protected override HSMState GetTransition()
        {
            if (_context.LevelTrustPlayer <= NPCConstants.NpcMinLevelTrustPlayerToFollowing)
            {
                return ((BaseNPCRoot)Parent).IdleBaseState;
            }
            
            return null;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_context?.TargetObject == null || _context.Agent == null)
                return;
            
            _context.Agent.SetDestination(_context.TargetObject.position);
            
            float distanceToTarget = Vector3.Distance(
                _context.Agent.transform.position, 
                _context.TargetObject.position);


            if (distanceToTarget <= _context.Personality.ArmLength)
            {
                _context.Agent.isStopped = true;
                
                Vector3 directionToTarget = (_context.TargetObject.position - _context.Agent.transform.position).normalized;
                directionToTarget.y = 0;
                
                if (directionToTarget != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    _context.Agent.transform.rotation = Quaternion.Slerp(
                        _context.Agent.transform.rotation, 
                        targetRotation, 
                        deltaTime * _context.Agent.angularSpeed / _turningSpeedCoefficient);
                }
                
                //TODO: Анимация остановки
            }
            else
            {
                _context.Agent.isStopped = false;
            }
        }

        protected override void OnExit()
        {
            if (_context?.Agent != null)
            {
                _context.CurrentState = NPCStates.IsIdle;
                _context.Agent.isStopped = true;
                _context.Agent.ResetPath();
            }
        }
    }
}