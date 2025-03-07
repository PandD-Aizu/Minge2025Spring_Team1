using TMPro;
using UnityEngine;

namespace Cost
{
    public class CostControllerView : MonoBehaviour
    {
        [Header("コストを表示するテキスト")]
        [SerializeField] private TextMeshProUGUI costText;
        
        // @brief コストの表示を更新する
        // @param cost 現在のコスト
        public void UpdateCostText(int cost)
        {
            costText.text = "Cost " + cost.ToString();
        }
    }
}