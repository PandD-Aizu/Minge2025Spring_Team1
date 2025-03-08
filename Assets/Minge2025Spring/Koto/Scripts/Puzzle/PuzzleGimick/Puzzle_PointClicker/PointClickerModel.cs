using UniRx;
using UnityEngine;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "PointClickerModel", menuName = "ScriptableObject/PointClickerModel")]
    public class PointClickerModel : AbstractPuzzleModel
    {
        [Header("プレイヤーに与えるコスト")]
        [SerializeField] private int GIVECOST = 2; // プレイヤーに与えるコスト
        
        [Header("現在の魔法陣の数")]
        [SerializeField] private int currentMagicCircle = 0; // 現在の魔法陣の数
        
        [Header("初期魔法陣の数")]
        [SerializeField] private int INIT_MAGICCIRCLE = 1; // 初期魔法陣の数
        
        [Header("魔法陣の最大数")]
        [SerializeField] private int MAX_MAGICCIRCLE = 5; // 魔法陣の最大数
        
        /* getter と setter */
        public override BoolReactiveProperty IsSolved { get => isSolved; set => isSolved = value; }
        public int CurrentMagicCircle { get => currentMagicCircle; set => currentMagicCircle = value; }
        public int InitMagicCircle { get => INIT_MAGICCIRCLE; }
        public int MaxMagicCircle { get => MAX_MAGICCIRCLE; }

        // @brief 初期化関数
        public void Init()
        {
            currentMagicCircle = 0;
        }
        
        // @brief プレイヤーにコストを与える
        // @return プレイヤーに与えるコスト
        public override int GiveCost()
        {
            return GIVECOST;
        }
    }
}