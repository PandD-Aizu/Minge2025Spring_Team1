using System;
using Puzzle;
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
            puzzleModel.OnSolvePuzzle += UpdateCost; // パズルを解いたときに得られたコストを通知する
            model.Init();
        }
        
        // @brief コストを増減させる
        // @param costValue コストの増減値
        private void UpdateCost(int costValue)
        {
            model.Cost += costValue;
            view.UpdateCostText(model.Cost);
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            model.Cost = 0;
        }
    }
}