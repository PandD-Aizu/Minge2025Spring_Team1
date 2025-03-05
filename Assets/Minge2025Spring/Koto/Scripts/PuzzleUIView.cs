using DG.Tweening;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleUIView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer puzzleBackGround;
        [SerializeField] private GameObject puzzleScreen;

        public void ShowPuzzleScreen(bool isShowing)
        {
            if (isShowing)
            {
                puzzleBackGround.DOFade(0.9f, 0.5f);
                puzzleScreen.transform.DOLocalMoveY(0, 0.5f)
                    .SetEase(Ease.InSine);
            }
            else
            {
                puzzleBackGround.DOFade(0, 0.5f);
                puzzleScreen.transform.DOLocalMoveY(800, 0.5f)
                    .SetEase(Ease.OutSine);
            }
        }
    }
}