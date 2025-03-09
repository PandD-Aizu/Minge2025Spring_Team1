using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleUIModel", menuName = "ScriptableObject/PuzzleUIModel")]
    public class PuzzleUIModel : ScriptableObject
    {
        [Header("パズルが表示されているかどうか")] 
        [SerializeField] private bool isShowing = false;
        
        /* getter と setter */
        public bool IsShowing { get => isShowing; set => isShowing = value; }
    }
}