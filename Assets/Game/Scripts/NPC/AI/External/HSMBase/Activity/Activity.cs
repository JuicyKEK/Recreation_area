using System.Threading;
using System.Threading.Tasks;
using Game.Scripts.NPC.HSMBase.Interfaces;
using UnityEngine;

namespace Game.Scripts.NPC.HSMBase.SequenceraseBase
{
    public abstract class Activity : IActivity 
    {
        public ActivityMode Mode { get; protected set; } = ActivityMode.Inactive;

        public virtual async Task ActivateAsync(CancellationToken ct) {
            if (Mode != ActivityMode.Inactive) return;
            
            Mode = ActivityMode.Activating;
            await Task.CompletedTask;
            Mode = ActivityMode.Active;
            Debug.Log($"Activated {GetType().Name} (mode={Mode})");
        }

        public virtual async Task DeactivateAsync(CancellationToken ct) {
            if (Mode != ActivityMode.Active) return;
            
            Mode = ActivityMode.Deactivating;
            await Task.CompletedTask;
            Mode = ActivityMode.Inactive;
            Debug.Log($"Deactivated {GetType().Name} (mode={Mode})");
        }
    }
    
    public enum ActivityMode { Inactive, Activating, Active, Deactivating }
}