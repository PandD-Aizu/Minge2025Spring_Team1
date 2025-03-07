using UniRx;
using UnityEngine;

namespace Puzzle
{
    // サンプルのパズル
    // データを管理するクラス
    [CreateAssetMenu(fileName = "SamplePuzzleModel", menuName = "ScriptableObject/SamplePuzzleModel")]
    public class SamplePuzzleModel : ScriptableObject
    {
        [Header("与えるコスト")] 
        [SerializeField] private int COST = 5; // プレイヤーに与えるコスト
        
        [Header("パズルの状態")]
        [SerializeField] private BoolReactiveProperty isSolved = new BoolReactiveProperty(false); // パズルが解かれたかどうか
        
        /* getter と setter */
        public BoolReactiveProperty IsSolved { get => isSolved; set => isSolved = value; }
        
        // @brief プレイヤーにコストを与える
        public int GiveCost()
        {
            return COST;
        }
    }
}