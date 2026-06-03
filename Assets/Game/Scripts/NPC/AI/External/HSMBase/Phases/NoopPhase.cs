using Game.Scripts.NPC.HSMBase.Interfaces;

namespace Game.Scripts.NPC.HSMBase.SequenceraseBase
{
    public class NoopPhase : ISequencer
    {
        public bool IsDone { get; private set; }
        public void Start() => IsDone = true; //IsDone = true - последовательность завершается немедленно
        public bool Update() => IsDone;
    }
}