using EnhanceCard.Param;
using EnhanceCard.Result;
using UnityEngine;

namespace EnhanceCard
{
    [CreateAssetMenu(fileName = "AttackEnhance", menuName = "ScriptableObjects/EnhanceCard/AttackEnhance")]
    public class AttackEnhance : ScriptableObject, ICardEffect
    {
        [Header("攻撃力増加量")]
        [SerializeField] private int attackPower;

        // @brief カードの効果を適用する
        // @param EnhanceParam 効果を適用するためのパラメーター
        // @return EnhanceResult 効果の結果
        public IEnhanceResult GiveEffect(IEnhanceParam param)
        {
            return new AttackResult(attackPower);
        }
    }
}