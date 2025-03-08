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
        [SerializeField] private PuzzleUIView puzzleUIView;
        
        [Header("パズルギミックのScriptableObject(データベース)")] 
        [SerializeField] private List<AbstractPuzzleModel> samplePuzzle;
        
        [Header("パズルギミックの親オブジェクト")] 
        [SerializeField] private List<GameObject> puzzleParents;

        // @brief エントリポイント
        private void Start()
        {
            model.Init();
            puzzleParents[model.CurrentPuzzleIndex].SetActive(true);
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
            
            // 右ボタンが押されたときのイベントを登録
            puzzleUIView.RightButton.onClick
                .AddListener(() =>
                {
                    puzzleParents[model.CurrentPuzzleIndex++].SetActive(false); // 現在のパズルを非表示にする
                    
                    if(model.CurrentPuzzleIndex > puzzleParents.Count - 1)
                        model.CurrentPuzzleIndex = 0;                                 // ループできるように処理する
                    
                    puzzleParents[model.CurrentPuzzleIndex].SetActive(true);    // 次のパズルを表示する
                });
            
            // 左ボタンが押されたときのイベントを登録
            puzzleUIView.LeftButton.onClick
                .AddListener(() =>
                {
                    puzzleParents[model.CurrentPuzzleIndex--].SetActive(false); // 現在のパズルを非表示にする

                    if (model.CurrentPuzzleIndex < 0)
                        model.CurrentPuzzleIndex = puzzleParents.Count - 1;     // ループできるように処理する
                    
                    puzzleParents[model.CurrentPuzzleIndex].SetActive(true);    // 前のパズルを表示する
                });
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}
