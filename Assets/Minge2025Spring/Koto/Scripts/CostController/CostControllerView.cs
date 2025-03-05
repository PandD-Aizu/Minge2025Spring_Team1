using TMPro;
using UnityEngine;

namespace Cost
{
    public class CostControllerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI costText; // コストの表示テキスト
        
        // @brief コストの表示を更新する
        // @param cost 現在のコスト
        public void UpdateCostText(int cost)
        {
            costText.text = cost.ToString();
        }
    }
}