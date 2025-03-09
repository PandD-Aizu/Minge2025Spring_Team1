using System;
using Cost;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace Puzzle
{
    // パズル機能を管理するクラス
    public class PuzzlePresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")] 
        [SerializeField] private PuzzleModel model;
        [SerializeField] private CostControllerModel costControllerModel;
        [SerializeField] private PuzzleGimmickModel puzzleGimmickModel;

        // @brief エントリポイント  
        private void Start()
        {
            SubScribeEvents();
        }
        
        // @brief イベント群の登録
        private void SubScribeEvents()
        {
            // パズルを解いたときに得られたコストを通知する
            puzzleGimmickModel.NotifyCostChange
                .Skip(1) // 初期化時のイベントをスキップ
                .Subscribe((changeValue) =>
                {
                    costControllerModel.Cost.Value += changeValue;
                });
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}