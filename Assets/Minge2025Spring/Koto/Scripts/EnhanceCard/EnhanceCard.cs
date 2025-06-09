using System.Collections.Generic;
using UnityEngine;

namespace EnhanceCard
{
    public class EnhanceCard : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> enhanceAsset; // カードの効果
        [SerializeField] private string cardName;                     // カードの名前
        [SerializeField] private int cost;                            // コスト
        [SerializeField] private int maxCost;                         // 最大コスト
        
        private Dictionary<EnhanceTrigger, List<ICardEffect>> enhances 
            = new Dictionary<EnhanceTrigger, List<ICardEffect>>();
        
        public int GetCost => cost;
        public Dictionary<EnhanceTrigger, List<ICardEffect>> Enhances => enhances;

        private void Start()
        {
            AddEnhance();
        }

        // @brief カードの効果を取得する
        private void AddEnhance()
        {
            foreach (var enhance in enhanceAsset)
            {
                if (enhance is ICardEffect cardEffect)
                {
                    if (!enhances.ContainsKey(EnhanceTrigger.ON_USE))
                        enhances[EnhanceTrigger.ON_USE] = new List<ICardEffect>();
                    
                    enhances[EnhanceTrigger.ON_USE].Add(cardEffect);
                }
            }
        }
    }
}