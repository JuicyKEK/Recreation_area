using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.Scripts.NPC.HSMBase.Interfaces;

namespace Game.Scripts.NPC.HSMBase.SequenceraseBase
{
    public class ParallelPhase: ISequencer
    {
        private readonly List<PhaseStep> _steps;
        private readonly CancellationToken _ct;
        
        private List<Task> _tasks;
        private int _index = -1;
        private Task _currentTask;
        
        public bool IsDone { get; private set; }

        public ParallelPhase(List<PhaseStep> steps, CancellationToken ct)
        {
            _steps = steps;
            _ct = ct;
        }

        public void Start()
        {
            if (_steps == null || _steps.Count == 0)
            {
                IsDone = true;
                return;
            }
            
            _tasks = new List<Task>(_steps.Count);
            for (int i = 0; i < _steps.Count; i++)
            {
                _tasks.Add(_steps[i](_ct));
            }
        }
        
        public bool Update()
        {
            if (IsDone)
            {
                return true;
            }

            IsDone = _tasks == null || _tasks.TrueForAll(t => t.IsCompleted);
            return IsDone;
        }

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
        
    }
}