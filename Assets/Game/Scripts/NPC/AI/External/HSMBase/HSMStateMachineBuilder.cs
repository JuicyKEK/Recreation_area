using System.Collections.Generic;
using System.Reflection;

namespace Game.Scripts.NPC.HSMBase
{
    public class HSMStateMachineBuilder
    {
        private readonly HSMState _root;

        public HSMStateMachineBuilder(HSMState root)
        {
            _root = root;
        }

        public HSMStateMachine Build()
        {
            var m = new HSMStateMachine(_root);
            Wire(_root, m, new HashSet<HSMState>());
            return m;
        }
        
        //Внедряет автомат в каждое состояние и проходит по иерархии //Мб тут придется возиться
        private void Wire(HSMState s, HSMStateMachine m, HashSet<HSMState> visited) {
            if (s == null) return;
            if (!visited.Add(s)) return; // State is already wired
            
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            var machineField = typeof(HSMState).GetField("Machine", flags);
            if (machineField != null) machineField.SetValue(s, m);

            foreach (var fld in s.GetType().GetFields(flags)) {
                if (!typeof(HSMState).IsAssignableFrom(fld.FieldType)) continue; // Only consider fields that are State
                if (fld.Name == "Parent") continue; // Skip back-edge to parent
                
                var child = (HSMState)fld.GetValue(s);
                if (child == null) continue;
                if (!ReferenceEquals(child.Parent, s)) continue; // Ensure it's actually our direct child
                
                Wire(child, m, visited); // Recurse into the child
            }
        }
    }
}