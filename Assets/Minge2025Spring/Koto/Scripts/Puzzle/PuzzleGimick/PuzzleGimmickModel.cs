using UniRx;
using UnityEngine;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleGimmickModel", menuName = "ScriptableObject/PuzzleGimmickModel")]
    public class PuzzleGimmickModel : ScriptableObject
    {
        [Header("得られたコスト")]
        [SerializeField] private ReactiveProperty<int> notifyCostChange = new ReactiveProperty<int>(0); // パズルを解いたときに得られたコスト
        
        /* getter と setter */
        public ReactiveProperty<int> NotifyCostChange { get => notifyCostChange; set => notifyCostChange = value; }
    }
}