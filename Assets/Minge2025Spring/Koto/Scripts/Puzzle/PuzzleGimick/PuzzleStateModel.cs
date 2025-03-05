using System;
using UnityEngine;

namespace Puzzle
{
    [Serializable]
    public class PuzzleStateModel
    {
        public enum PuzzleState { samplePuzzle, samplePuzzle2, samplePuzzle3 }
        
        [Header("パズルの状態")]
        [SerializeField] private PuzzleState currentState;
        
        /* getter と setter */
        public PuzzleState CurrentState { get => currentState; set => currentState = value; }
    }
}