using System;
using System.Collections.Generic;
using System.Linq;
using CharacterInfo;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterBehaviour
{
    public class CharacterBehaviourModel : MonoBehaviour
    {
        [FormerlySerializedAs("allyAttack")]
        [Header("依存関係")] 
        [SerializeField] private AllyAttackManager allyAttackManager;
        [SerializeField] private AllyTargetManager allyTargetManager;
        [SerializeField] private AllyBlockManager allyBlockManager;
        
        [Header("ゴールのセル")]
        [SerializeField] private GameObject goalCell;
        
        [Header("攻撃範囲のコライダー")]
        [SerializeField] private BoxCollider attackRangeCollider;
        [SerializeField] private List<Vector3> attackRangeSize;
        [SerializeField] private List<Vector3> attackRangeCenter;
        [SerializeField] private List<Vector3> attackRangeSpriteSize;
        [SerializeField] private List<Vector3> attackRangeSpritePosition;
        
        [Header("攻撃範囲内の敵のリスト")] 
        [SerializeField] private ReactiveCollection<GameObject> enemyList = new ReactiveCollection<GameObject>();
        
        [Header("攻撃中の敵")] 
        [SerializeField] private List<GameObject> targetEnemies = new List<GameObject>();

        [Header("攻撃のクールタイム")] 
        [SerializeField] private float attackCoolDownTime;

        [Header("イベント")] 
        [SerializeField] private ReactiveProperty<bool> isAttackEnemy = new ReactiveProperty<bool>(false);
        [SerializeField] private ReactiveProperty<bool> isShowCharacterStatus = new ReactiveProperty<bool>(false);
        [SerializeField] private ReactiveProperty<bool> isWithDraw = new ReactiveProperty<bool>(false);

        /* getter と setter */
        public AllyAttackManager AllyAttackManager                       { get => allyAttackManager; }
        public AllyTargetManager AllyTargetManager                       { get => allyTargetManager; }
        public AllyBlockManager AllyBlockManager                         { get => allyBlockManager; }
        public BoxCollider AttackRangeCollider                           { get => attackRangeCollider; set => attackRangeCollider = value; }
        public List<Vector3> AttackRangeSize                             { get => attackRangeSize; }
        public List<Vector3> AttackRangeCenter                           { get => attackRangeCenter; }
        public List<Vector3> AttackRangeSpriteSize                       { get => attackRangeSpriteSize; }
        public List<Vector3> AttackRangeSpritePosition                   { get => attackRangeSpritePosition; }
        public ReactiveCollection<GameObject> EnemyList                  { get => enemyList; set => enemyList = value; }
        public List<GameObject> EnemyListValue                           { get => enemyList.ToList(); }
        public List<GameObject> TargetEnemies                            { get => targetEnemies; set => targetEnemies = value; }
        public float AttackCoolDownTime                                  { get => attackCoolDownTime; set => attackCoolDownTime = value; }
        public ReactiveProperty<bool> IsAttackEnemy                      { get => isAttackEnemy; set => isAttackEnemy = value; }
        public ReactiveProperty<bool> IsShowCharacterStatus              { get => isShowCharacterStatus; set => isShowCharacterStatus = value; }
        public ReactiveProperty<bool> IsWithDraw                         { get => isWithDraw; set => isWithDraw = value; }
        
        // @brief 攻撃優先度を設定
        // @param allyInfo 味方キャラクターの情報
        public void SetAttackPriority(AllyInfo allyInfo)
        {
            if (allyInfo.AttackType == AttackType.CLOSE_RANGE_SINGLE ||
                allyInfo.AttackType == AttackType.LONG_RANGE_SINGLE)
            {
                targetEnemies = new List<GameObject>(1){allyTargetManager.SetAttackPrioritySingle(EnemyListValue, goalCell.transform.position)};
            }
            else if (allyInfo.AttackType == AttackType.CLOSE_RANGE_MULTIPLE ||
                     allyInfo.AttackType == AttackType.LONG_RANGE_MULTIPLE)
            {
                targetEnemies = allyTargetManager.SetAttackPriorityMultiple(EnemyListValue, goalCell.transform.position, allyInfo.MaxAttackNum);
            }
        }
        
        // @brief 攻撃対象リスト内の敵がnullでないかチェック
        // @param allyInfo 味方キャラクターの情報
        public void CheckEnemyList(AllyInfo allyInfo)
        {
            // TODO: 重くなりそうなんで処理を考える
            enemyList = new ReactiveCollection<GameObject>(enemyList.Where(enemy => enemy != null).ToList());
            
            // 再度サブスクライブ
            enemyList
                .ObserveAdd()
                .Subscribe(_ => SetAttackPriority(allyInfo));
        }

        // @brief 敵を攻撃
        // @param allyInfo 味方キャラクターの情報
        public CharacterState AttackEnemy(AllyInfo allyInfo)
        {
            if (targetEnemies.Count != 0 && allyInfo.AttackCoolDown <= attackCoolDownTime)
            {
                isAttackEnemy.SetValueAndForceNotify(true);
                attackCoolDownTime = 0;
            }
            else if (targetEnemies.Count == 0 && enemyList.Count <= 0)
            {
                return CharacterState.WAIT;
            }
            else if (targetEnemies.Count == 0 && enemyList.Count > 0)
            {
                SetAttackPriority(allyInfo);
            }

            return CharacterState.ATTACK;
        }

        public void CheckCharacterHp(AllyInfo allyInfo)
        {
            if (allyInfo.Hp <= 0)
                allyInfo.CharacterState = CharacterState.DEAD;
        }

        // @brief 味方キャラクターが撤退もしくは死亡した際にScriptableObjectを初期化
        // @param allyInfo 味方キャラクターの情報
        public void InitAllyInfo(AllyInfo allyInfo)
        {
            allyInfo.Init();
            IsWithDraw.Value = false;
            IsAttackEnemy.Value = false;
            IsShowCharacterStatus.Value = false;
        }
    }
}