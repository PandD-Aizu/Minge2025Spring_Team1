using System;
using CharacterInfo;
using UnityEngine;

public class StatusContainerManager : MonoBehaviour
{
    
    [Header("ステータス情報")]
    [SerializeField] private AllyInfo allyInfo;
    [Header("攻撃力テキスト")]
    [SerializeField] private TMPro.TextMeshProUGUI attackPowerText;
    [Header("防御力テキスト")]
    [SerializeField] private TMPro.TextMeshProUGUI defencePowerText;
    [Header("HPテキスト")]
    [SerializeField] private TMPro.TextMeshProUGUI hpText;
    [Header("HPスライダー")]
    [SerializeField] private UnityEngine.UI.Slider hpSlider;
    
    private AllyInfo AllyInfo => allyInfo;

    private void Start()
    {
        hpSlider.maxValue = allyInfo.MaxHp;
    }


    // テキストにATKとDEFの値を表示
    /*!
     * @brief ステータス情報を更新する
     * 
     */
    public void UpdateStatusText()
    {
        attackPowerText.text = "ATK: " + allyInfo.Attack.ToString();
        defencePowerText.text = "DEF: " + allyInfo.Defence.ToString();
        hpText.text = "HP: " + AllyInfo.Hp.ToString() + "/" + AllyInfo.MaxHp.ToString();
        // Hpのスライダーを表示
        hpSlider.value = allyInfo.Hp;
    }

    private void Update()
    {
        UpdateStatusText();
    }
    
}
