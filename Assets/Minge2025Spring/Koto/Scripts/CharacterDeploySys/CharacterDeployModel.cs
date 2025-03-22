using UniRx;
using UnityEngine;

namespace CharacterDeploySys
{
    [CreateAssetMenu(fileName = "CharacterDeployModel", menuName = "ScriptableObject/CharacterDeployModel")]
    public class CharacterDeployModel : ScriptableObject
    {
        [Header("フラグ")]
        [SerializeField] private bool isDeployActive;    // ユニットがドラッグアンドドロップ中か
        [SerializeField] private bool isDeployAvailable; // ユニットが配置可能か

        [Header("レイヤーマスク")]
        [SerializeField] private LayerMask deployLayer;      // ユニットが配置可能なレイヤーマスク
        [SerializeField] private LayerMask characterUILayer; // キャラクターUIのレイヤーマスク

        [Header("選択したキャラクター")]
        [SerializeField] private GameObject selectedCharacter;
        [SerializeField] private string selectedCharacterName;
        
        [Header("減らすコスト")] 
        [SerializeField] private int cost;
         
        /* getter と setter */
        public bool IsDeployActive          { get => isDeployActive; set => isDeployActive = value; }
        public bool IsDeployAvailable       { get => isDeployAvailable; set => isDeployAvailable = value; }
        public LayerMask DeployLayer        { get => deployLayer; set => deployLayer = value; }
        public LayerMask CharacterUILayer   { get => characterUILayer; }
        public GameObject SelectedCharacter { get => selectedCharacter; set => selectedCharacter = value; }
        public string SelectedCharacterName { get => selectedCharacterName; set => selectedCharacterName = value; }
        public int Cost                     { get => cost; set => cost = value; }

        // @brief 初期化処理
        public void Init()
        {
            isDeployActive = false;
            isDeployAvailable = false;
            cost = 0;
            selectedCharacter = null;
        }
    }
}