using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleGimmickModel", menuName = "ScriptableObject/PuzzleGimmickModel")]
    public class PuzzleGimmickModel : ScriptableObject
    {
        [Header("得られたコスト")]
        [SerializeField] private ReactiveProperty<int> notifyCostChange = new ReactiveProperty<int>(0);

        [Header("現在のパズルギミックのインデックス")] 
        [SerializeField] private int currentPuzzleIndex; 
        
        /* getter と setter */
        public ReactiveProperty<int> NotifyCostChange { get => notifyCostChange; set => notifyCostChange = value; }
        public int CurrentPuzzleIndex { get => currentPuzzleIndex; set => currentPuzzleIndex = value; }

        // @brief 初期化関数
        public void Init()
        {
            currentPuzzleIndex = 0;
        }
    }
}