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
    
    private float currentAttackCoolTime = 0f;
    private GameObject longRengeCollider;
    private LongRengeCollider longRengeColliderScript;
    private EnemyStatus enemyStatus;
    public GameObject attackTarget;
    private SphereCollider sphereCollider;
    private Animator animator;
    private CharacterType _attackerType;
    private CharacterType _defenderType;
    [Header("TypeChartData")]
    [SerializeField] private TypeChartData typeChartData;

    [Header("EnemyAttackController")] 
    [SerializeField] private EnemyAttackType enemyAttackType = EnemyAttackType.None;
    
    [InspectorName("MeleeAttackRange: When EnemyAttackType is Melee, you can use this param")]
    [SerializeField] private float meleeAttackRange = 2f;

    [InspectorName("LongRangeAttackRadius: When EnemyAttackType is LongRange, you can use this param")]
    [SerializeField] private float longRangeAttackRadius = 4f;

    [InspectorName("ObserveTargetLayerMask")]
    [SerializeField] private LayerMask observeTargetLayerMask;

    [Header("アニメーション(パーティクルシステムの場合)")] 
    [SerializeField] private ParticleSystem attackAnimParticleSys;

    private void Start()
    {
        if (enemyAttackType == EnemyAttackType.LongRange)  // 遠距離攻撃タイプの敵の場合
        {
            longRengeCollider = Instantiate(new GameObject("LongRengeCollider"), this.transform.position, Quaternion.identity, this.transform);
            longRengeCollider.gameObject.AddComponent<SphereCollider>();
            longRengeColliderScript = longRengeCollider.AddComponent<LongRengeCollider>();
            longRengeColliderScript.InitLongRengeCollider(this.gameObject.GetComponent<EnemyAttack>(), enemyStatus, longRangeAttackRadius);
        }
        if (TryGetComponent<EnemyStatus>(out enemyStatus))
        {
            animator = enemyStatus.animator;
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
            EnemyAttackSystem(enemyStatus.CharacterType);
        }

        // 攻撃対象がいない場合、移動を再開する
        if (attackTarget == null)
        {
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
        }
        else
        {
            // 攻撃対象が非アクティブの場合、無視して移動を再開する
            if (!attackTarget.gameObject.activeSelf)
            {
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
            }
        }
    }

    // @brief TypeがMeleeのときにUpdateで使う, レイキャストで攻撃範囲内に味方キャラがいるか確認
    private void MeleeAttackObserver()
    {
        // レイキャストで自身が向いている方向をもとに攻撃範囲内に味方キャラがいるか確認
        Physics.Raycast(transform.position, enemyStatus.CurrentMoveDirection, out RaycastHit hit, meleeAttackRange, observeTargetLayerMask);
        Debug.DrawRay(transform.position, enemyStatus.CurrentMoveDirection, Color.red, 1.0f); // レイキャストの可視化
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag(GameTagsManager.Player) // 味方キャラかつ配置済みで攻撃していないかつ対象が自身をブロックできる場合
                && hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED
                && hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.BlockCount < hit.collider.GetComponent<CharacterBehaviourPresenter>().AllyInfo.MaxBlockCount
                && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
            {
                attackTarget = hit.collider.gameObject;                         // 攻撃対象を設定
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
                
            }
            else
            {
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
            }
        }
    }

    private void EnemyAttackSystem(CharacterType characterType)
    {
        if (attackTarget != null)
        {
            currentAttackCoolTime += Time.deltaTime; // 攻撃のクールタイムを更新
            
            if (enemyStatus.AttackCoolTime <= currentAttackCoolTime //　攻撃できてかつ攻撃対象の情報が取得できた場合 
                && attackTarget.gameObject.TryGetComponent<CharacterBehaviourPresenter>(out CharacterBehaviourPresenter characterBehaviourPresenter))
            {
                if (attackAnimParticleSys != null)
                    attackAnimParticleSys.Play();
                    
                animator.SetTrigger(enemyStatus.attackAnimationParameter);
                //Todo ターゲットにダメージを与える処理を正しく書き直す2024/03/22時点まだ
                int damage = enemyStatus.CurrentAttack - characterBehaviourPresenter.AllyInfo.Defence;
                
                // 属性の相性を考慮してダメージを計算
                _defenderType = characterBehaviourPresenter.AllyInfo.CharacterType;
                float multiplier = typeChartData.GetMultiplier(_attackerType, _defenderType);
                if (multiplier > 1.0f)
                {
                    damage *= (int)multiplier; // 効果倍率を適用
                }
                
                if (damage < 0)//キャラクターに与えるダメージがマイナスにならないように
                {
                    damage = 0;
                }
                characterBehaviourPresenter.AllyInfo.Hp -= damage; // ダメージを与える
                Debug.LogWarning(characterBehaviourPresenter.AllyInfo.Hp.ToString());
                
                currentAttackCoolTime = 0; // 攻撃クールタイムをリセット
            }
        }
    }
}
