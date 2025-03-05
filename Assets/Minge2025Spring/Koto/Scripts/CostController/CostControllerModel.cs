using System;
using UnityEngine;

namespace Cost
{
    [Serializable]
    public class CostControllerModel
    {
        [Header("テキスト")]
        [SerializeField] private int cost; // コスト
        
        /* getter と setter */
        public int Cost { get => cost; set => cost = value; }
    }
}