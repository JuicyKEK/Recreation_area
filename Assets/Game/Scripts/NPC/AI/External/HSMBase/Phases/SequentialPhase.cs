using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.Scripts.NPC.HSMBase.Interfaces;

namespace Game.Scripts.NPC.HSMBase.SequenceraseBase
{
    public class SequentialPhase : ISequencer
    {
        private readonly List<PhaseStep> _steps;
        private readonly CancellationToken _ct;
        private int _index = -1;
        private Task _currentTask;
        
        public bool IsDone { get; private set; }

        public SequentialPhase(List<PhaseStep> steps, CancellationToken ct)
        {
            _steps = steps;
            _ct = ct;
        }

        public void Start() => Next();

        private void Next()
        {
            _index++;
            if (_index >= _steps.Count)
            {
                IsDone = true;
                return;
            }

            _currentTask = _steps[_index](_ct);
        }

        public bool Update()
        {
            if (IsDone)
            {
                return true;
            }

            if (_currentTask == null || _currentTask.IsCompleted)
            {
                Next();
            }
            
            return IsDone;
        }
    }
}