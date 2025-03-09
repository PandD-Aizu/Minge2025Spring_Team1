using UniRx;
using UnityEngine;

namespace CharacterDeploySys
{
    [CreateAssetMenu(fileName = "CharacterDeployModel", menuName = "ScriptableObject/CharacterDeployModel")]
    public class CharacterDeployModel : ScriptableObject
    {
        [Header("フラグ")]
        [SerializeField] private bool isDeployActive;

        [Header("レイヤーマスク")]
        [SerializeField] private LayerMask characterLayer;
        [SerializeField] private LayerMask characterUILayer;

        [Header("選択したキャラクター")]
        [SerializeField] private GameObject selectedCharacter;
        
        [Header("減らすコスト")] 
        [SerializeField] private int cost;
         
        public bool IsDeployActive          { get => isDeployActive; set => isDeployActive = value; }
        public LayerMask CharacterLayer     { get => characterLayer; set => characterLayer = value; }
        public LayerMask CharacterUILayer   { get => characterUILayer; }
        public GameObject SelectedCharacter { get => selectedCharacter; set => selectedCharacter = value; }
        public int Cost                     { get => cost; set => cost = value; }

        // @brief 初期化処理
        public void Init()
        {
            isDeployActive = false;
            cost = 0;
            selectedCharacter = null;
        }
    }
}