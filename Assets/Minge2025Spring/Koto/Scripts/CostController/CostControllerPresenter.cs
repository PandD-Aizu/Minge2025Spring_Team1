using System;
using Puzzle;
using UniRx;
using UnityEngine;

namespace Cost
{
    public class CostControllerPresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")]
        [SerializeField] private CostControllerModel model;
        [SerializeField] private CostControllerView view;
        [SerializeField] private PuzzleModel puzzleModel;

        // @brief エントリポイント
        private void Start()
        {
            model.Init();
            SubscribeEvents();
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            // パズルを解いたときに得られたコストを通知する
            model.Cost
                .Skip(1)
                .Subscribe((changeVal) =>
                {
                    view.UpdateCostText(changeVal); // コストの表示を更新
                });
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}