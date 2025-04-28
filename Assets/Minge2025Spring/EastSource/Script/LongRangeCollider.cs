using CharacterBehaviour;
using CharacterInfo;
using UnityEngine;

public class LongRangeCollider : MonoBehaviour
{
    private EnemyAttack enemyAttack;       // 敵の攻撃を管理するクラス
    private EnemyStatus enemyStatus;       // 敵のステータスを管理するクラス
    private SphereCollider sphereCollider; // 球コライダー

    // @brief 初期化処理
    // @param enemyAttack 敵の攻撃を管理するクラス, enemyStatus 敵のステータスを管理するクラス, longRangeAttackRadius 攻撃範囲の半径
    public void InitLongRangeCollider(EnemyAttack enemyAttack, EnemyStatus enemyStatus, float longRangeAttackRadius)
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>(); // 球コライダーを取得
        sphereCollider.radius = longRangeAttackRadius;              // 球コライダーの半径を設定
        sphereCollider.isTrigger = true;                            // isTriggerを有効にする
        this.enemyStatus = enemyStatus;                             // 敵の攻撃を管理するクラス
        this.enemyAttack = enemyAttack;                             // 敵のステータスを管理するクラス
    }
    
    // @brief 当たり判定(Enter)
    // @param other 衝突したオブジェクト
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameTagsManager.Player) // 味方キャラかつ配置済みで攻撃状態でない場合
            && other.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED
            && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
        {
            enemyAttack.attackTarget = other.gameObject;                    // 攻撃対象を設定
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
        }
    }
}
