using System;
using UnityEngine;

namespace Game.Scripts.NPC.RunTime.NPCPersonalityCreator
{
    /// <summary>
    /// Персональность НПС, определяющая его поведение
    /// </summary>
    [CreateAssetMenu(fileName = "NPCPersonality", menuName = "AI/NPC Personality")]
    [Serializable]
    public class NPCPersonalitySO : ScriptableObject
    {
        [Range(0f, 1f)]
        public float LevelOfCourage = 0.5f; //Уровень смелости
        [Range(0f, 1f)]
        public float LevelOfTrust = 0.5f; //Уровень доверия игроку
        [Range(0f, 1f)]
        public float LevelOfcuriosity = 0.5f; // Любопытство

        public float SpeedAction = 1;
        public float ArmLength = 1;
        public float MaxHP = 10f;
        public float SpeedMove = 2;
        public float SpeedRun = 2;
    }
}
