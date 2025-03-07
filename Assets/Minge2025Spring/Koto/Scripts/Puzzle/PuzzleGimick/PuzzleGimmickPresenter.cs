using System;
using UniRx;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleGimmickPresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")] 
        [SerializeField] private PuzzleGimmickModel model;
        [SerializeField] private PuzzleStateModel stateModel;
        
        [Header("パズルギミック")] 
        [SerializeField] private SamplePuzzleModel samplePuzzle;

        // @brief エントリポイント
        private void Start()
        {
            SubscribeEvents();
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            samplePuzzle.IsSolved
                .Skip(1) // 初期化時のイベントをスキップ
                .Subscribe((isSolved) =>
                {
                    if(isSolved)
                        model.NotifyCostChange.SetValueAndForceNotify(samplePuzzle.GiveCost());
                });
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}
