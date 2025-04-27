using UnityEngine;
using UnityEngine.UI;

namespace Minge2025Spring.EastSource.Script
{
    public class EnemySliderController : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private EnemyStatus enemyStatus;
        
        [Header("スライダー")]
        [SerializeField] private Slider hpSlider; // HPスライダー

        private void Start()
        {
            hpSlider.maxValue = enemyStatus.MaxHealth;
        }

        private void Update()
        {
            ChangeSliderValue();
        }

        // @brief スライダーの値を変更する
        private void ChangeSliderValue()
        {
            hpSlider.value = enemyStatus.CurrentHealth;
        }
    }
}