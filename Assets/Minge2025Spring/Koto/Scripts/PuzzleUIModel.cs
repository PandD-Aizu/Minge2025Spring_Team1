using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class PuzzleUIModel : MonoBehaviour
    {
        [Header("ボタン")] 
        [SerializeField] private Button puzzleOnOffButton;

        [Header("フラグ")] 
        [ShowNonSerializedField] private bool isShowing;
        
        /* getter と setter */
        public Button PuzzleOnOffButton { get => puzzleOnOffButton; set => puzzleOnOffButton = value; }
        
        public bool IsShowing { get => isShowing; set => isShowing = value; }
    }
}