using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
    public interface ICharacterAttack
    {
        public abstract void AttackSingleCloseRange(GameObject target, float attackValue);
        public abstract void AttackMultipleCloseRange(List<GameObject> targets, float attackValue);
        public abstract void AttackSingleLongRange(GameObject target, float attackValue);
        public abstract void AttackMultipleLongRange(List<GameObject> targets, float attackValue);
    }
}