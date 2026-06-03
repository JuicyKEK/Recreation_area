using System.Threading;
using System.Threading.Tasks;
using Game.Scripts.NPC.HSMBase.SequenceraseBase;

namespace Game.Scripts.NPC.HSMBase.Interfaces
{
    public interface IActivity {
        ActivityMode Mode { get; }
        Task ActivateAsync(CancellationToken ct);
        Task DeactivateAsync(CancellationToken ct);
    }
}