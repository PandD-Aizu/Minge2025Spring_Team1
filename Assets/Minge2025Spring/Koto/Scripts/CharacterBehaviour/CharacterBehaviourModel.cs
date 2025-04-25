using System;
using System.Collections.Generic;
using System.Linq;
using CharacterInfo;
using UniRx;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterBehaviourModel : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private AllyAttack allyAttack;
        [SerializeField] private AllyTargetManager allyTargetManager;
        
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
        [SerializeField] private List<GameObject> targetEnemies;

        [Header("攻撃のクールタイム")] 
        [SerializeField] private float attackCoolDownTime;

        [Header("イベント")] 
        [SerializeField] private ReactiveProperty<bool> isAttackEnemy = new ReactiveProperty<bool>(false);
        [SerializeField] private ReactiveProperty<bool> isUpdateEnemyList = new ReactiveProperty<bool>(false);
        [SerializeField] private ReactiveProperty<bool> isShowCharacterStatus = new ReactiveProperty<bool>(false);
        [SerializeField] private ReactiveProperty<bool> isWithDraw = new ReactiveProperty<bool>(false);

        /* getter と setter */
        public AllyAttack AllyAttack                                     { get => allyAttack; }
        public AllyTargetManager AllyTargetManager                       { get => allyTargetManager; }
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
        public IObservable<bool> IsUpdateEnemyList                       { get => isUpdateEnemyList; }
        public ReactiveProperty<bool> IsShowCharacterStatus              { get => isShowCharacterStatus; set => isShowCharacterStatus = value; }
        public ReactiveProperty<bool> IsWithDraw                         { get => isWithDraw; set => isWithDraw = value; }
        
        // @brief 攻撃対象リスト内の敵がnullでないかチェック
        public void CheckEnemyList()
        {
            // TODO: 重くなりそうなんで処理を考える
            enemyList = new ReactiveCollection<GameObject>(enemyList.Where(enemy => enemy != null).ToList());
            
            // 再度サブスクライブ
            enemyList
                .ObserveAdd()
                .Subscribe(_ => SetAttackPriority());
        }

        // @brief 敵を攻撃
        // @param attackCoolDown　味方の攻撃クールタイム
        public CharacterState AttackEnemy(float attackCoolDown)
        {
            if (targetEnemy != null && attackCoolDown <= attackCoolDownTime)
            {
                isAttackEnemy.SetValueAndForceNotify(true);
                attackCoolDownTime = 0;
            }
            else if (targetEnemy == null && enemyList.Count <= 0)
            {
                return CharacterState.WAIT;
            }
            else if (targetEnemy == null && enemyList.Count > 0)
            {
                SetAttackPriority();
            }

            return CharacterState.ATTACK;
        }

        // @brief 敵にダメージを与える
        // @param attackValue 攻撃力
        public void GiveDamageToEnemy(float attackValue)
        {
            if (targetEnemy != null)
            {
                EnemyStatus enemyStatus = targetEnemy.GetComponent<EnemyStatus>();
                
                float damage = attackValue - enemyStatus.CurrentDefence;
                
                if(damage <= 0)
                    enemyStatus.CurrentHealth = attackValue * 5 / 100;
                else
                    enemyStatus.CurrentHealth -= damage;
            }
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