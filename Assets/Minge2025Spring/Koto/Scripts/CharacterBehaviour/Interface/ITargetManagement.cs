using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
    public interface ITargetManagement
    {
        public abstract GameObject SetAttackPrioritySingle(List<GameObject> targets, Vector3 goalPos);
        public abstract List<GameObject> SetAttackPriorityMultiple(List<GameObject> targets, Vector3 goalPos, int maxTargets);
    }
}