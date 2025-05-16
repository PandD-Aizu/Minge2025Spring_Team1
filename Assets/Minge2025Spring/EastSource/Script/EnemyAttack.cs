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
            longRengeCollider = Instantiate(new GameObject("LongRengeCollider"), this.transform.position, Quaternion.identity, this.transform);
            longRengeCollider.gameObject.AddComponent<SphereCollider>();
            longRengeColliderScript = longRengeCollider.AddComponent<LongRengeCollider>();
            longRengeColliderScript.InitLongRengeCollider(this.gameObject.GetComponent<EnemyAttack>(), enemyStatus, longRangeAttackRadius);
        }
        
        if (TryGetComponent(out enemyStatus))
            animator = enemyStatus.animator;
        else
            Debug.LogError("NOT FOUND ENEMYSTATUS COMPONENT : EnemyAttack Start()");
    }

    private void Update()
    {
        if (enemyAttackType == EnemyAttackType.Melee) // 近接攻撃タイプの敵の場合
            MeleeAttackObserver();
        
        if (enemyStatus.enemyState == EnemyStatus.EnemyState.Attacking) // 攻撃状態の場合
            EnemyAttackSystem();

        if (attackTarget == null)
        {
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
        }
        else
        {
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
            if (hit.collider.gameObject.CompareTag(GameTagsManager.Player) // 味方キャラかつ配置済みで攻撃していない場合
                && hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED
                && hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.CurrentBlockCount <= hit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo.MaxBlockCount
                && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
            {
                attackTarget = hit.collider.gameObject;                         // 攻撃対象を設定
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking); // 攻撃状態に変更
            }
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
                animator.SetTrigger(enemyStatus.attackAnimationParameter);
                int damage = enemyStatus.CurrentAttack - characterBehaviourPresenter.AllyInfo.Defence;
                
                if (damage < 0)//キャラクターに与えるダメージがマイナスにならないように
                    damage = 0;
                
                characterBehaviourPresenter.AllyInfo.Hp -= damage; // ダメージを与える
                Debug.LogWarning(characterBehaviourPresenter.AllyInfo.Hp.ToString());
                currentAttackCoolTime = 0; // 攻撃クールタイムをリセット
            }
        }
    }
}
