using System;
using CharacterBehaviour;
using CharacterInfo;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyAttack : MonoBehaviour
{
    private enum EnemyAttackType
    {
        None,
        Melee,
        LongRange,
    }
    
    private float currentAttackCoolTime = 0f;
    private EnemyStatus enemyStatus;
    private GameObject attackTarget;
    private SphereCollider sphereCollider;

    [Header("EnemyAttackController")] 
    [SerializeField] private EnemyAttackType enemyAttackType = EnemyAttackType.None;
    
    [InspectorName("MeleeAttackRange: When EnemyAttackType is Melee, you can use this param")]
    [SerializeField] private float meleeAttackRange = 2f;

    [InspectorName("LongRangeAttackRadius: When EnemyAttackType is LongRange, you can use this param")]
    [SerializeField] private float longRangeAttackRadius = 4f;

    [InspectorName("ObserveTargetLayerMask")]
    [SerializeField] private LayerMask observeTargetLayerMask;

    private void Start()
    {
        if (enemyAttackType == EnemyAttackType.LongRange)  // 遠距離攻撃タイプの敵の場合
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>(); // スフィアコライダーを追加
            sphereCollider.radius = longRangeAttackRadius;              // スフィアコライダーの半径を設定
            sphereCollider.isTrigger = true;                            // スフィアコライダーをトリガーに設定
        }
        if (TryGetComponent<EnemyStatus>(out enemyStatus))
        {
            
        }
        else
        {
            Debug.LogError("NOT FOUND ENEMYSTATUS COMPONENT : EnemyAttack Start()");
        }
    }

    private void Update()
    {
        if (enemyAttackType == EnemyAttackType.Melee) // 近接攻撃タイプの敵の場合
        {
            MeleeAttackObserver();
        }
        
        if (enemyStatus.enemyState == EnemyStatus.EnemyState.Attacking) // 攻撃状態の場合
        {
            EnemyAttackSystem();
        }

        if (attackTarget == null)
        {
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
        }
    }

    // @brief TypeがMeleeのときにUpdateで使う, レイキャストで攻撃範囲内に味方キャラがいるか確認
    private void MeleeAttackObserver()
    {
        // レイキャストで自身が向いている方向をもとに攻撃範囲内に味方キャラがいるか確認
        Physics.Raycast(this.transform.position, enemyStatus.CurrentMoveDirection, out RaycastHit hit, meleeAttackRange, observeTargetLayerMask);
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag(GameTagsManager.Player) // 味方キャラかつ配置済みで攻撃していない場合
                && hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED
                && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
            {
                attackTarget = hit.collider.gameObject;                         // 攻撃対象を設定
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
            }
            else
            {
                // enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
            }
        }
        else
        {
            // enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving );
        }
    }

    private void EnemyAttackSystem()
    {
        if (attackTarget != null)
        {
            currentAttackCoolTime += Time.deltaTime; // 攻撃のクールタイムを更新
            
            if (enemyStatus.AttackCoolTime <= currentAttackCoolTime //　攻撃できてかつ攻撃対象の情報が取得できた場合 
                && attackTarget.gameObject.TryGetComponent<CharacterBehaviourPresenter>(out CharacterBehaviourPresenter characterBehaviourPresenter))
            {
                Debug.LogWarning("TakeDamage");
                
                //Todo ターゲットにダメージを与える処理を正しく書き直す2024/03/22時点まだ
                
                characterBehaviourPresenter.AllyInfo.Hp -= enemyStatus.CurrentAttack - characterBehaviourPresenter.AllyInfo.Defence; // ダメージを与える
                Debug.LogWarning(characterBehaviourPresenter.AllyInfo.Hp.ToString());
                
                currentAttackCoolTime = 0; // 攻撃クールタイムをリセット
            }
        }
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
            attackTarget = other.gameObject;                                // 攻撃対象を設定
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
        }
    }
}
