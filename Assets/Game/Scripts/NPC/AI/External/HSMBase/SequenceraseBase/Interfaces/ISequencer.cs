using System.Threading;
using System.Threading.Tasks;

namespace Game.Scripts.NPC.HSMBase.Interfaces
{
    public interface ISequencer
    {
        bool IsDone { get; }
        void Start();
        bool Update();
    }

    public delegate Task PhaseStep(CancellationToken ct);
}