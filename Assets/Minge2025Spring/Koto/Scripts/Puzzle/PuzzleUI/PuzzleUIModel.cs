using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    [Serializable]
    public class PuzzleUIModel
    {
        [Header("ボタン")] 
        [SerializeField] private Button puzzleOnOffButton; // パズルの表示切替ボタン
        
        [Header("フラグ")] 
        [SerializeField] private bool isShowing; // パズルが表示されているかどうか
        
        /* getter と setter */
        public Button PuzzleOnOffButton { get => puzzleOnOffButton; set => puzzleOnOffButton = value; }
        
        public bool IsShowing { get => isShowing; set => isShowing = value; }
    }
}