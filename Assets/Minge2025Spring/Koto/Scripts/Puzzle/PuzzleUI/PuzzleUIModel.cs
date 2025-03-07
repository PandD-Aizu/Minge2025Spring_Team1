using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleUIModel", menuName = "ScriptableObject/PuzzleUIModel")]
    public class PuzzleUIModel : ScriptableObject
    {
        [Header("フラグ")] 
        [SerializeField] private bool isShowing = false; // パズルが表示されているかどうか
        
        /* getter と setter */
        public bool IsShowing { get => isShowing; set => isShowing = value; }
    }
}