using System;
using UnityEngine;

namespace Cost
{
    [CreateAssetMenu(fileName = "CostControllerModel", menuName = "ScriptableObject/CostControllerModel")]
    public class CostControllerModel : ScriptableObject
    {
        [Header("初期化時の値")]
        [SerializeField] private int initCost;
        
        [Header("コスト")]
        [SerializeField] private int cost;
        
        /* getter と setter */
        public int Cost { get => cost; set => cost = value; }

        // @brief 初期化処理
        public void Init()
        {
            cost = initCost;
        }
    }
}