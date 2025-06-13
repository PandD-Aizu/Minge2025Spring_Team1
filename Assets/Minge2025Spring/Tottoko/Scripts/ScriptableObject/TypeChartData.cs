using UnityEngine;
using System;
using System.Collections.Generic;

/**
 * @brief ゲーム内のキャラクターの属性相性を管理する。
 */
[CreateAssetMenu(fileName = "TypeChartData", menuName = "Character/TypeChartData")]
public class TypeChartData : ScriptableObject
{
    /**
     * @brief 属性相性のデータ構造
     * 各属性の攻撃が他の属性に対してどれだけ効果的かを示す。
     */
    [Serializable] 
    public class TypeEffectiveness
    {
        public CharacterType attacker;
        public CharacterType defender;
        public float multiplier = 1.0f;
    }

    /*
     * @brief 属性相性のリスト
     * 各属性の攻撃が他の属性に対してどれだけ効果的かを示すリスト。
     * このリストは、エディタで設定可能で、ゲーム内で使用される。
     */
    [Header("属性相性リスト")]
    [SerializeField]
    private List<TypeEffectiveness> effectivenessList = new List<TypeEffectiveness>();

    private Dictionary<(CharacterType, CharacterType), float> effectivenessMap;

    private void OnEnable()
    {
        effectivenessMap = new Dictionary<(CharacterType, CharacterType), float>();

        foreach (var entry in effectivenessList)
        {
            effectivenessMap[(entry.attacker, entry.defender)] = entry.multiplier;
        }
    }

    /**
     * @brief 属性相性に基づく攻撃の効果倍率を取得
     * @param attacker 攻撃側の属性
     * @param defender 防御側の属性
     * @return 攻撃の効果倍率
     */
    public float GetMultiplier(CharacterType attacker, CharacterType defender)
    {
        if (effectivenessMap != null && 
            effectivenessMap.TryGetValue((attacker, defender), out float multiplier)) 
        {
            return　multiplier; // 効果倍率を返す
        }

        return 1.0f; // 効果は普通
    }

    /**
     * @brief ダメージ計算
     * @param baseDamage 基本ダメージ
     * @param attacker 攻撃側の属性
     * @param defender 防御側の属性
     * @return 計算後のダメージ
     */
    public float CalculateDamage(float baseDamage, CharacterType attacker,CharacterType defender)
    {
        return baseDamage * GetMultiplier(attacker, defender);
    }

}
