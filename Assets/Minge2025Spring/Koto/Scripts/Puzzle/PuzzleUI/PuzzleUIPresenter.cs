using System;
using CharacterBehaviour;
using NaughtyAttributes;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleUIPresenter : MonoBehaviour, IDisposable
    {
        [SerializeField] private PuzzleUIView view;   // 描画の依存関係
        [SerializeField] private PuzzleUIModel model; // データの依存関係
        [SerializeField] private StageCharacterControllerModel characterControllerModel; // キャラクターの依存関係

        // @brief エントリポイント
        public void Start()
        {
            SubscribeEvents();
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            // パズルUIの表示切替
            view.PuzzleOnOffButton.onClick.AddListener(() =>
            {
                characterControllerModel.IsShowingCharacterStatus = !model.IsShowing;
                model.IsShowing = !model.IsShowing;
                view.ShowPuzzleScreen(model.IsShowing);
            });
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}