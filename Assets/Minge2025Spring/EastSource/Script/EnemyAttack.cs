using System;
using System.Numerics;
using CharacterBehaviour;
using CharacterInfo;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyAttack : MonoBehaviour
{
    private enum EnemyAttackType
    {
        None,
        Melee,
        LongRange,
    }
    
    private float             currentAttackCoolTime;   // 攻撃のクールタイム 
    private GameObject        longRengeCollider;       // 遠距離攻撃用のコライダー
    private LongRengeCollider longRengeColliderScript; // 遠距離攻撃用のコライダーのスクリプト
    private EnemyStatus       enemyStatus;             // 敵のステータス
    public  GameObject        attackTarget;            // 攻撃対象
    private SphereCollider    sphereCollider;          // 球場コライダー
    private Animator          animator;                // アニメーター

    [Header("EnemyAttackController")] 
    [SerializeField] private EnemyAttackType enemyAttackType = EnemyAttackType.None;
    
    [InspectorName("MeleeAttackRange: When EnemyAttackType is Melee, you can use this param")]
    [SerializeField] private float meleeAttackRange = 2f;

    [InspectorName("LongRangeAttackRadius: When EnemyAttackType is LongRange, you can use this param")]
    [SerializeField] private float longRangeAttackRadius = 4f;

    [InspectorName("ObserveTargetLayerMask")]
    [SerializeField] private LayerMask observeTargetLayerMask;

    // @brief エントリポイント
    private void Start()
    {
        // 敵の攻撃タイプによってコライダーを設定
        if (enemyAttackType == EnemyAttackType.LongRange)
        {
            longRengeCollider = Instantiate(new GameObject("LongRengeCollider"), transform.position, Quaternion.identity, this.transform);
            longRengeCollider.gameObject.AddComponent<SphereCollider>();
            longRengeColliderScript = longRengeCollider.AddComponent<LongRengeCollider>();
            longRengeColliderScript.InitLongRengeCollider(gameObject.GetComponent<EnemyAttack>(), enemyStatus, longRangeAttackRadius);
        }
        
        // アニメータを取得
        if (TryGetComponent<EnemyStatus>(out enemyStatus))
            animator = enemyStatus.animator;
        else
            Debug.LogError("NOT FOUND ENEMYSTATUS COMPONENT : EnemyAttack Start()");
    }

    // @brief メインループ
    private void Update()
    {
        // 敵の攻撃タイプが近接攻撃のとき
        if (enemyAttackType == EnemyAttackType.Melee)
            MeleeAttackObserver();
        
        // 敵が攻撃中のとき
        if (enemyStatus.enemyState == EnemyStatus.EnemyState.Attacking)
            EnemyAttackSystem();

        // 攻撃対象がいないとき
        if (attackTarget == null)
        {
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
        }
        else
        {
            // 攻撃対象のオブジェクトがアクティブでないとき
            if (!attackTarget.gameObject.activeSelf)
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
            // 攻撃範囲内に味方キャラがいる && 味方キャラが配置済み && 敵が攻撃状態でない
            if (hit.collider.gameObject.CompareTag(GameTagsManager.Player)
                && hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED
                && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
            {
                attackTarget = hit.collider.gameObject;                         // 攻撃対象を設定
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
            }
        }
    }

    // @brief 攻撃対象がいるときに
    private void EnemyAttackSystem()
    {
        if (attackTarget != null)
        {
            currentAttackCoolTime += Time.deltaTime; // 攻撃のクールタイムを更新
            
            
            if (enemyStatus.AttackCoolTime <= currentAttackCoolTime
                && attackTarget.gameObject.TryGetComponent(out CharacterBehaviourPresenter characterBehaviourPresenter))
            {
                animator.SetTrigger(enemyStatus.attackAnimationParameter);
                int damage = enemyStatus.CurrentAttack - characterBehaviourPresenter.AllyInfo.Defence;
                
                if (damage < 0)//キャラクターに与えるダメージがマイナスにならないように
                    damage = 0;
                
                characterBehaviourPresenter.AllyInfo.Hp -= damage; // ダメージを与える
                currentAttackCoolTime = 0; // 攻撃クールタイムをリセット
            }
        }
    }
}
