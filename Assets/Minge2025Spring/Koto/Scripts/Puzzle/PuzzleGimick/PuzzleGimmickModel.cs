using System;
using UnityEngine;

namespace Puzzle
{
    [Serializable]
    public class PuzzleGimmickModel
    {
        [Header("得られたコスト")]
        [SerializeField] private int cost; // パズルを解いたときに得られたコスト
        
        /* getter と setter */
        public int Cost { get => cost; set => cost = value; }
    }
}