using System;
using UnityEngine;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleStateModel", menuName = "ScriptableObject/PuzzleStateModel")]
    public class PuzzleStateModel : ScriptableObject
    {
        public enum PuzzleState { samplePuzzle, samplePuzzle2, samplePuzzle3 }
        
        [Header("パズルの状態")]
        [SerializeField] private PuzzleState currentState;
        
        /* getter と setter */
        public PuzzleState CurrentState { get => currentState; set => currentState = value; }
    }
}