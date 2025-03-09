using UniRx;
using UnityEngine;

namespace Puzzle
{
    // サンプルのパズル
    // データを管理するクラス(Buttonなどは使えない)
    [CreateAssetMenu(fileName = "SamplePuzzleModel", menuName = "ScriptableObject/SamplePuzzleModel")]
    public class SamplePuzzleModel : AbstractPuzzleModel
    {
        [Header("プレイヤーに与えるコスト")]
        [SerializeField] private int GIVECOST = 10; // プレイヤーに与えるコスト
        
        /* getter と setter */
        public override BoolReactiveProperty IsSolved { get => isSolved; set => isSolved = value; }
        
        // @brief プレイヤーにコストを与える
        // @return プレイヤーに与えるコスト
        public override int GiveCost()
        {
            return GIVECOST;
        }
    }
}