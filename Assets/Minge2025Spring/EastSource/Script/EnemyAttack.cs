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
    
    private float currentAttackCoolTime;               // 攻撃のクールタイム
    private GameObject longRengeCollider;              // 遠距離攻撃の攻撃範囲
    private LongRangeCollider longRangeColliderScript; // 遠距離攻撃の攻撃範囲を管理するクラス
    private EnemyStatus enemyStatus;                   // 敵のステータスを管理するクラス
    public GameObject attackTarget;                    // 攻撃対象
    private SphereCollider sphereCollider;             // 球コライダー
    private Animator animator;                         // アニメーター

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
        // 遠距離攻撃タイプの敵の場合
        if (enemyAttackType == EnemyAttackType.LongRange)
        {
            longRengeCollider = Instantiate(new GameObject("LongRangeCollider"), transform.position, Quaternion.identity, transform);        // 子オブジェクトとして新しいオブジェクトを追加
            longRengeCollider.gameObject.AddComponent<SphereCollider>();                                                                          // 球コライダーをコンポーネントで追加
            longRangeColliderScript = longRengeCollider.AddComponent<LongRangeCollider>();                                                        // 遠距離攻撃を管理するクラスをコンポーネントで追加
            longRangeColliderScript.InitLongRangeCollider(gameObject.GetComponent<EnemyAttack>(), enemyStatus, longRangeAttackRadius); // 遠距離攻撃を管理するクラスを初期化
        }
        
        // 敵のステータスを管理するクラスを取得する
        if (TryGetComponent(out enemyStatus))
            animator = enemyStatus.animator; // アニメーターを取得する
    }

    private void Update()
    {
        // 近接攻撃タイプの敵の場合
        if (enemyAttackType == EnemyAttackType.Melee)
            MeleeAttackObserver();
        
        // 敵が攻撃状態の場合
        if (enemyStatus.enemyState == EnemyStatus.EnemyState.Attacking)
            EnemyAttackSystem();

        // 攻撃対象がいない場合
        if (attackTarget == null)
        {
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving); // 移動状態に遷移
        }
        else
        {
            // 攻撃対象がアクティブではない場合
            if (!attackTarget.gameObject.activeSelf)
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving); // 移動状態に遷移
        }
    }

    // @brief TypeがMeleeのときにUpdateで使う, レイキャストで攻撃範囲内に味方キャラがいるか確認
    private void MeleeAttackObserver()
    {
        // レイキャストで自身が向いている方向をもとに攻撃範囲内に味方キャラがいるか確認
        Physics.Raycast(this.transform.position, enemyStatus.CurrentMoveDirection, out RaycastHit hit, meleeAttackRange, observeTargetLayerMask);
        
        if (hit.collider != null)
        {
            // 味方キャラかつ配置済みで攻撃していない場合
            if (hit.collider.gameObject.CompareTag(GameTagsManager.Player) 
                && hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED
                && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
            {
                attackTarget = hit.collider.gameObject;                         // 攻撃対象を設定
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
            }
        }
    }

    // @brief 敵の攻撃システム
    private void EnemyAttackSystem()
    {
        if (attackTarget != null)
        {
            currentAttackCoolTime += Time.deltaTime; // 攻撃のクールタイムを更新
            
            // 攻撃可能でかつ攻撃対象の情報が取得できた場合
            if (enemyStatus.AttackCoolTime <= currentAttackCoolTime 
                && attackTarget.gameObject.TryGetComponent(out CharacterBehaviourPresenter characterBehaviourPresenter))
            {
                animator.SetTrigger(enemyStatus.attackAnimationParameter);                             // 攻撃アニメーションをトリガー
                int damage = enemyStatus.CurrentAttack - characterBehaviourPresenter.AllyInfo.Defence; // 与えるダメージを計算
                
                if (damage < 0)
                    damage = enemyStatus.CurrentAttack * 5 / 100;                                      // ダメージの最低保障
                
                characterBehaviourPresenter.AllyInfo.Hp -= damage;                                     // ダメージを与える
                currentAttackCoolTime = 0;                                                             // 攻撃クールタイムをリセット
            }
        }
    }
}
