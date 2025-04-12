using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class PuzzleUIView : MonoBehaviour
    {
        [Header("ボタン")]
        [SerializeField] private Button puzzleOnOffButton; // パズルの表示切替ボタン
        [SerializeField] private Button rightButton;       // パズル切り替えボタン(右)
        [SerializeField] private Button leftButton;        // パズル切り替えボタン(左)
        
        [Header("画像")]
        [SerializeField] private SpriteRenderer puzzleBackGround; // パズル画面の背景(SampleScene要参照)
        [SerializeField] private GameObject puzzleScreen;         // パズル画面
        
        /* getter と setter */
        public Button PuzzleOnOffButton { get => puzzleOnOffButton; set => puzzleOnOffButton = value; }
        public Button RightButton       { get => rightButton;       set => rightButton       = value; }
        public Button LeftButton        { get => leftButton;        set => leftButton        = value; }

        // @brief パズル画面の表示切替
        // @param isShowing 開いているか閉じているか
        public void ShowPuzzleScreen(bool isShowing)
        {
            if (isShowing) // 閉じていたら開く
            {
                puzzleBackGround.DOFade(0.9f, 0.5f);
                puzzleScreen.transform.DOLocalMoveY(0, 0.5f)
                    .SetEase(Ease.InSine);
                
                // Tutorialの矢印を消すために勝手に追加します.byEastSource
                if(TutorialManager.Instance != null)
                    TutorialManager.Instance.HidePuzzleOnOffButtonArrow();
            }
            else           // 開いていたら閉じる
            {
                puzzleBackGround.DOFade(0, 0.5f);
                puzzleScreen.transform.DOLocalMoveY(800, 0.5f)
                    .SetEase(Ease.OutSine);
                
                // Tutorialの矢印をコントロールに勝手に追加します.byEastSource
                if(TutorialManager.Instance != null)
                    TutorialManager.Instance.ShowCharacterUIButtonArrow();
            }
        }
    }
}