using System.Collections.Generic;
using CharacterInfo;
using UniRx;
using UnityEngine;

namespace CharacterDeploySys
{
    [CreateAssetMenu(fileName = "CharacterDeployModel", menuName = "ScriptableObject/CharacterDeployModel")]
    public class CharacterDeployModel : ScriptableObject
    {
        [Header("フラグ")]
        [SerializeField] private bool isDeployActive;         // ユニットがドラッグアンドドロップ中か
        [SerializeField] private bool isDeployAvailable;      // ユニットが配置可能か
        [SerializeField] private bool isSelectCharaAttackRange; // ユニットの攻撃方向を選択できるか

        [Header("味方の情報")] 
        [SerializeField] private List<AllyInfo> allyInfos;    // 味方ユニットの情報
        
        [Header("レイヤーマスク")]
        [SerializeField] private LayerMask deployLayer;      // ユニットが配置可能なレイヤーマスク
        [SerializeField] private LayerMask characterUILayer; // キャラクターUIのレイヤーマスク
        [SerializeField] private LayerMask cellLayer;        // セルのレイヤーマスク

        [Header("選択したキャラクター")]
        [SerializeField] private GameObject selectedCharacter;
        [SerializeField] private string selectedCharacterName;
        
        [Header("減らすコスト")] 
        [SerializeField] private int cost;
         
        /* getter と setter */
        public bool IsDeployActive            { get => isDeployActive; set => isDeployActive = value; }
        public bool IsDeployAvailable         { get => isDeployAvailable; set => isDeployAvailable = value; }
        public bool IsSelectCharaAttackRange  { get => isSelectCharaAttackRange; set => isSelectCharaAttackRange = value; }
        public List<AllyInfo> AllyInfos       { get => allyInfos; }
        public LayerMask DeployLayer          { get => deployLayer; set => deployLayer = value; }
        public LayerMask CharacterUILayer     { get => characterUILayer; }
        public LayerMask CellLayer            { get => cellLayer; }
        public GameObject SelectedCharacter   { get => selectedCharacter; set => selectedCharacter = value; }
        public string SelectedCharacterName   { get => selectedCharacterName; set => selectedCharacterName = value; }
        public int Cost                       { get => cost; set => cost = value; }

        // @brief 初期化処理
        public void Init()
        {
            isDeployActive = false;
            isDeployAvailable = false;
            isSelectCharaAttackRange = false;
            cost = 0;
            selectedCharacter = null;
        }

        // @brief キャラクターの配置情報を配置済みに設定
        public void SetCharacterDeployInfo()
        {
            Debug.Log($"[SetCharacterDeployInfo] 選択キャラ名: {selectedCharacterName}");

            foreach (var ally in allyInfos)
            {
                if (ally == null)
                {
                    Debug.LogWarning("[SetCharacterDeployInfo] nullな要素があります");
                    continue;
                }

                Debug.Log($"[SetCharacterDeployInfo] 登録キャラ名: {ally.CharacterName}");
            }

            var info = allyInfos.Find(x =>
                x != null && x.CharacterName == selectedCharacterName);

            if (info == null)
            {
                Debug.LogWarning($"[SetCharacterDeployInfo] '{selectedCharacterName}' に一致する AllyInfo が見つかりません");
                return;
            }

            info.CharacterDeployInfo = CharacterDeployInfo.DEPLOYED;
        }

    }
}