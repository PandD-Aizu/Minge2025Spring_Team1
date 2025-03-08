using System;
using UnityEngine;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleModel", menuName = "ScriptableObject/PuzzleModel")]
    public class PuzzleModel : ScriptableObject
    {
        private Action<int> onSolvePuzzle; // パズルを解いたときの処理
        
        /* getter と setter */
        public Action<int> OnSolvePuzzle { get => onSolvePuzzle; set => onSolvePuzzle = value; }
    }
}