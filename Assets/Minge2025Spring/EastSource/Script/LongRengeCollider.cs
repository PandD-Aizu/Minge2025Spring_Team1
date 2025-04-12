using System;
using CharacterBehaviour;
using CharacterInfo;
using UnityEngine;

public class LongRengeCollider : MonoBehaviour
{
    private EnemyAttack enemyAttack;
    private EnemyStatus enemyStatus; //親オブジェクトのEnemyAttackに依存
    private SphereCollider sphereCollider;

    private void Start()
    {
    }

    public void InitLongRengeCollider(EnemyAttack enemyAttack, EnemyStatus enemyStatus, float longRangeAttackRadius)
    {
        sphereCollider = this.gameObject.GetComponent<SphereCollider>();
        sphereCollider.radius = longRangeAttackRadius;
        sphereCollider.isTrigger = true;
        this.enemyStatus = enemyStatus;
        this.enemyAttack = enemyAttack;
    }
    
    // @brief 当たり判定(Enter)
    // @param other 衝突したオブジェクト
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameTagsManager.Player) // 味方キャラかつ配置済みで攻撃状態でない場合
            && other.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED
            && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
        {
            Debug.Log("Discover");
            enemyAttack.attackTarget = other.gameObject;                                // 攻撃対象を設定
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
        }
    }
}
