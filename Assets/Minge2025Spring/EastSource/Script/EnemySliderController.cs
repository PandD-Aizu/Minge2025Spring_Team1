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
            if (hpSlider.value == enemyStatus.MaxHealth)
                hpSlider.gameObject.SetActive(false);
            else
                hpSlider.gameObject.SetActive(true);

            ChangeSliderValue();
        }

        // @brief スライダーの値を変更する
        private void ChangeSliderValue()
        {
            hpSlider.value = enemyStatus.CurrentHealth;
        }
    }
}