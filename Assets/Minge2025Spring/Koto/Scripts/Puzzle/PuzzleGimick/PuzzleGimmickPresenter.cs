using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Puzzle
{
    // すべてのパズルを管理するクラス
    public class PuzzleGimmickPresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")] 
        [SerializeField] private PuzzleGimmickModel model;
        [SerializeField] private PuzzleStateModel stateModel;
        
        [Header("パズルギミック")] 
        [SerializeField] private List<AbstractPuzzleModel> samplePuzzle;

        // @brief エントリポイント
        private void Start()
        {
            SubscribeEvents();
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            // ステージに存在するすべてのパズルに対してイベントを登録
            foreach(var puzzle in samplePuzzle)
            {
                puzzle.IsSolved
                    .Skip(1) // 初期化時の通知をスキップ
                    .Subscribe((isSolved) =>
                    {
                        if(isSolved)
                            model.NotifyCostChange.SetValueAndForceNotify(puzzle.GiveCost());
                    });
            }
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}
