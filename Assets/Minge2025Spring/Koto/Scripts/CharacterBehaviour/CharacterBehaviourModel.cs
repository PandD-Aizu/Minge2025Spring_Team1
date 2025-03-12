using System;
using System.Collections.Generic;
using System.Linq;
using CharacterInfo;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterBehaviourModel : MonoBehaviour
    {
        [Header("攻撃範囲内の敵のリスト")]
        [SerializeField] private ReactiveCollection<GameObject> enemyList = new ReactiveCollection<GameObject>();
        

        [Header("攻撃中の敵")] 
        [SerializeField] private GameObject targetEnemy;

        [Header("攻撃のクールタイム")] 
        [SerializeField] private float attackCoolDownTime;

        /* getter と setter */
        public ReactiveCollection<GameObject> EnemyList                  { get => enemyList; set => enemyList = value; }
        public List<GameObject> EnemyListValue                           { get => enemyList.ToList(); }
        public GameObject TargetEnemy                                    { get => targetEnemy; set => targetEnemy = value; }
        public float AttackCoolDownTime                                  { get => attackCoolDownTime; set => attackCoolDownTime = value; }
        
        // @brief 攻撃目標を設定
        public void SetAttackPriority()
        {
            float minDistance = float.MaxValue;

            foreach (var enemy in enemyList)
            {
                // TODO: 敵とゴールの距離を計算して、最も近い敵を攻撃対象にする
                if(minDistance > Vector3.Distance(transform.position, enemy.transform.position))
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
            if (enemyList.Count != 0 && attackCoolDown >= attackCoolDownTime)
            {
                // TODO: 敵の体力を減らす
                attackCoolDownTime = 0;
            }
            else if (enemyList.Count == 0)
            {
                return CharacterState.WAIT;
            }

            return CharacterState.ATTACK;
        }
    }
}