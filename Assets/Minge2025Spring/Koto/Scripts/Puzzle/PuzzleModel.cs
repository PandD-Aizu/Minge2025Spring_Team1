using System;
using UnityEngine;

namespace Puzzle
{
    [Serializable]
    public class PuzzleModel
    {
        [SerializeField] private Action<int> onSolvePuzzle; // パズルを解いたときの処理
        
        /* getter と setter */
        public Action<int> OnSolvePuzzle { get => onSolvePuzzle; set => onSolvePuzzle = value; }
    }
}