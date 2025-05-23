using UnityEngine;

namespace EnhanceCard
{
    [CreateAssetMenu(fileName = "AttackEnhance", menuName = "ScriptableObjects/EnhanceCard/AttackEnhance")]
    public class AttackEnhance : ScriptableObject, ICardEffect
    {
        [SerializeField] private int attackPower;

        public EnhanceResult GiveEffect(EnhanceParam param)
        {
            return new EnhanceResult(attackPower);
        }
    }
}