using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class PuzzleUIView : MonoBehaviour
    {
        [Header("画像")]
        [SerializeField] private SpriteRenderer puzzleBackGround; // パズル画面の背景
        [SerializeField] private GameObject puzzleScreen; // パズル画面

        // @brief パズル画面の表示切替
        // @param isShowing 開いているか閉じているか
        public void ShowPuzzleScreen(bool isShowing)
        {
            if (isShowing) // 閉じていたら開く
            {
                puzzleBackGround.DOFade(0.9f, 0.5f);
                puzzleScreen.transform.DOLocalMoveY(0, 0.5f)
                    .SetEase(Ease.InSine);
            }
            else           // 開いていたら閉じる
            {
                puzzleBackGround.DOFade(0, 0.5f);
                puzzleScreen.transform.DOLocalMoveY(800, 0.5f)
                    .SetEase(Ease.OutSine);
            }
        }
    }
}