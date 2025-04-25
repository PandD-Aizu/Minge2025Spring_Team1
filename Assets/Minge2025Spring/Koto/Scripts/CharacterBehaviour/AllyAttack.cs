using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
    public class AllyAttack : MonoBehaviour, ICharacterAttack
    {
        // @brief 単体近接攻撃
        // @param targetEnemy 攻撃対象, @param attackValue 攻撃力
        public void AttackSingleCloseRange(GameObject targetEnemy, float attackValue)
        {
            if (targetEnemy != null)
            {
                EnemyStatus enemyStatus = targetEnemy.GetComponent<EnemyStatus>();
                float damage = attackValue - enemyStatus.CurrentDefence;

                if (damage <= 0)
                    enemyStatus.CurrentHealth = attackValue * 5 / 100;
                else
                    enemyStatus.CurrentHealth -= damage;
            }
        }

        // @brief 複数近接攻撃
        // @param targetEnemies 攻撃対象リスト, @param attackValue 攻撃力
        public void AttackMultipleCloseRange(List<GameObject> targetEnemies, float attackValue)
        {
            foreach (var targetEnemy in targetEnemies)
            {
                EnemyStatus enemyStatus = targetEnemy.GetComponent<EnemyStatus>();
                float damage = attackValue - enemyStatus.CurrentDefence;

                if (damage <= 0)
                    enemyStatus.CurrentHealth = attackValue * 5 / 100;
                else
                    enemyStatus.CurrentHealth -= damage;
            }
        }

        // @brief 単体遠距離攻撃
        // // @param targetEnemy 攻撃対象, @param attackValue 攻撃力
        public void AttackSingleLongRange(GameObject targetEnemy, float attackValue)
        {
            if (targetEnemy != null)
            {
                EnemyStatus enemyStatus = targetEnemy.GetComponent<EnemyStatus>();
                float damage = attackValue - enemyStatus.CurrentDefence;

                if (damage <= 0)
                    enemyStatus.CurrentHealth = attackValue * 5 / 100;
                else
                    enemyStatus.CurrentHealth -= damage;
            }
        }

        // @brief 複数遠距離攻撃
        // @param targetEnemies 攻撃対象リスト, @param attackValue 攻撃力
        public void AttackMultipleLongRange(List<GameObject> targetEnemies, float attackValue)
        {
            foreach (var targetEnemy in targetEnemies)
            {
                EnemyStatus enemyStatus = targetEnemy.GetComponent<EnemyStatus>();
                float damage = attackValue - enemyStatus.CurrentDefence;

                if (damage <= 0)
                    enemyStatus.CurrentHealth = attackValue * 5 / 100;
                else
                    enemyStatus.CurrentHealth -= damage;
            }
        }
    }
}