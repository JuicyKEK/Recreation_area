using Game.Scripts.NPC.RunTime.NPC;

namespace Game.Scripts.NPC.RunTime.NPCPersonalityCreator
{
    public class ServiceNPCPersonalityCreator
    {
        public NPCPersonality CreateNPCPersonality(NPCPersonalitySO npc)
        {
            return new NPCPersonality(npc.LevelOfCourage, npc.LevelOfTrust, npc.SpeedMove, npc.SpeedRun,
                npc.SpeedAction, npc.ArmLength, npc.MaxHP);
        }
    }
}