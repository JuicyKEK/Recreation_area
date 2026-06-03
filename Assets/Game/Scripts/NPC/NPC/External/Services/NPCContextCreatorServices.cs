using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.AI.External.Services.Interfaces;
using Game.Scripts.NPC.RunTime.NPC;
using UnityEngine.AI;

namespace Game.Scripts.NPC.AI.External.Services
{
    public class NPCContextCreatorServices : INPCContextCreatorServices
    {
        public NPCContext NPCContextCreat(NPCPersonality personality, NavMeshAgent navMeshAgent)
        {
            //return new NPCContext(navMeshAgent, personality);
            return null;
        }
    }
}