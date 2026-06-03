using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.RunTime.NPC;
using UnityEngine.AI;

namespace Game.Scripts.NPC.AI.External.Services.Interfaces
{
    public interface INPCContextCreatorServices
    {
        public NPCContext NPCContextCreat(NPCPersonality personality, NavMeshAgent navMeshAgent);
    }
}