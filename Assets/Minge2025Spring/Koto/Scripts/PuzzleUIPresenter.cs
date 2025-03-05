using UnityEngine;

namespace Puzzle
{
    public class PuzzleUIPresenter : MonoBehaviour
    {
        [SerializeField] private PuzzleUIView view;   // 描画の依存関係
        [SerializeField] private PuzzleUIModel model; // データの依存関係

        public void Start()
        {
            SubscribeEvents();
        }

        public void Update()
        {
            
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            // パズルUIの表示切替
            model.PuzzleOnOffButton.onClick.AddListener(() =>
            {
                model.IsShowing = !model.IsShowing;
                view.ShowPuzzleScreen(model.IsShowing);
            });
        }
    }
}