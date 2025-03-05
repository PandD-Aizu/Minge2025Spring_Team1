using System;
using Cost;
using NaughtyAttributes;
using UnityEngine;

namespace Puzzle
{
    public class PuzzlePresenter : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private PuzzleModel puzzleModel;
        [SerializeField] private CostControllerModel costControllerModel;
        [SerializeField] private PuzzleGimmickModel puzzleGimmickModel;

        private void Update()
        {
            // TODO: パズルを解いたときにコストの増減を通知する
        }
        
        // @brief コストの増減を通知する
        private void NotifyCostInfo()
        {
            int costValue = puzzleGimmickModel.Cost;
            puzzleModel.OnSolvePuzzle?.Invoke(costValue);
        }
    }
}