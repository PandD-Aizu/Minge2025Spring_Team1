using System.Collections.Generic;
using System.Linq;
using CharacterInfo;
using UniRx;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterBehaviourModel : MonoBehaviour
    {
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
        [SerializeField] private GameObject targetEnemy;

        [Header("攻撃のクールタイム")] 
        [SerializeField] private float attackCoolDownTime;

        [Header("イベント")] 
        [SerializeField] private ReactiveProperty<bool> isAttackEnemy = new ReactiveProperty<bool>(false);
        [SerializeField] private ReactiveProperty<bool> isShowCharacterStatus = new ReactiveProperty<bool>(false);
        [SerializeField] private ReactiveProperty<bool> isWithDraw = new ReactiveProperty<bool>(false);

        /* getter と setter */
        public BoxCollider AttackRangeCollider                           { get => attackRangeCollider; set => attackRangeCollider = value; }
        public List<Vector3> AttackRangeSize                             { get => attackRangeSize; }
        public List<Vector3> AttackRangeCenter                           { get => attackRangeCenter; }
        public List<Vector3> AttackRangeSpriteSize                       { get => attackRangeSpriteSize; }
        public List<Vector3> AttackRangeSpritePosition                   { get => attackRangeSpritePosition; }
        public ReactiveCollection<GameObject> EnemyList                  { get => enemyList; set => enemyList = value; }
        public List<GameObject> EnemyListValue                           { get => enemyList.ToList(); }
        public GameObject TargetEnemy                                    { get => targetEnemy; set => targetEnemy = value; }
        public float AttackCoolDownTime                                  { get => attackCoolDownTime; set => attackCoolDownTime = value; }
        public ReactiveProperty<bool> IsAttackEnemy                      { get => isAttackEnemy; set => isAttackEnemy = value; }
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
        
        // @brief 攻撃目標を設定
        public void SetAttackPriority()
        {
            float minDistance = float.MaxValue;

            foreach (var enemy in enemyList)
            {
                // TODO: 敵とゴールの距離を計算して、最も近い敵を攻撃対象にする
                if(enemy != null && minDistance > Vector3.Distance(goalCell.transform.position, enemy.transform.position))
                {
                    minDistance = Vector3.Distance(transform.position, enemy.transform.position);
                    targetEnemy = enemy;
                }
            }
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
            else if (targetEnemy == null)
            {
                return CharacterState.WAIT;
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
                enemyStatus.CurrentHealth -= attackValue - enemyStatus.CurrentDefence;
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
        }
    }
}