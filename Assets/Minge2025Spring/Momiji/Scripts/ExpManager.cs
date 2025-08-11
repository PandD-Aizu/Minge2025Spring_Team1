using System.Collections.Generic;
using UnityEngine;
using CharacterInfo;
using DG.Tweening;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    [Header("味方キャラの現在の経験値")]
    [SerializeField] private int playerExp;
    
    [Header("得られる経験値の量")]
    [SerializeField] private int addExp = 10;
    
    [Header("ステータス上昇量")]
    [SerializeField] private int upAttack = 25;
    
    [Header("現在のレベル")]
    [SerializeField] private int level = 0;
    
    [Header("スライダーの参照")]
    [SerializeField] private Slider expSlider;
    
    [Header("適用するキャラクターの情報")]
    [SerializeField] private AllyInfo character;
    
    [Header("レベルごとの経験値の上限")]
    [SerializeField] private List<int> expLimits = new (6);
    
    void Start()
    {
        playerExp = 0;
        UpdateSliderMaxValue();
    }
    
    void Update()
    {
        // 現在のレベルが経験値上限リストの範囲内であり、かつ現在の経験値が次のレベルの条件を満たしている限りレベルアップを繰り返す
        while (level < expLimits.Count && playerExp >= expLimits[level])
        {
            LevelUp();
        }
    }

    void UpdateSliderMaxValue()
    {
        playerExp = 0;
        expSlider.DOValue(0, 0.5f);
        expSlider.maxValue = expLimits[level];
    }

    // @brief レベルアップ処理
    void LevelUp()
    {
        character.Attack += upAttack;
        level++;
        UpdateSliderMaxValue();
    }

    // @brief 経験値を追加する
    public void AddExp(GameObject enemy)
    {
        if (Equals(enemy.gameObject.name, "_BaseEnemyExample(Clone)"))
        {
            playerExp += 10;
            expSlider.DOValue(playerExp, 0.5f);
        }
        else if (Equals(enemy.gameObject.name, "FlyEnemy(Clone)"))
        {
            playerExp += 15;
            expSlider.DOValue(playerExp, 0.5f);
        }
    }
}
