using UniRx;
using UnityEngine;

namespace Puzzle
{
    // パズルの基底クラス
    public abstract class AbstractPuzzleModel : ScriptableObject
    {
        [Header("パズルが解かれたかどうか")]
        [SerializeField] protected BoolReactiveProperty isSolved = new BoolReactiveProperty(false);
        
        /* getter と setter */
        public abstract BoolReactiveProperty IsSolved { get ; set ; }
        
        // @brief プレイヤーにコストを与える
        public abstract int GiveCost();
    }
}