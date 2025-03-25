using System;
using CharacterBehaviour;
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
        if (enemyAttackType == EnemyAttackType.LongRange)
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = longRangeAttackRadius;
            sphereCollider.isTrigger = true;
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
        if (enemyAttackType == EnemyAttackType.Melee)
        {
            MeleeAttackObserver();
        }
        if (enemyStatus.enemyState == EnemyStatus.EnemyState.Attacking)
        {
            EnemyAttackSystem();
        }

        if (attackTarget == null)
        {
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
        }
    }

    //breif TypeがMeleeのときにUpdateで使う
    private void MeleeAttackObserver()
    {
        Physics.Raycast(this.transform.position, enemyStatus.CurrentMoveDirection, out RaycastHit hit, meleeAttackRange, observeTargetLayerMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag(GameTagsManager.Player)
                && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
            {
                attackTarget = hit.collider.gameObject;
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking);
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
            currentAttackCoolTime += Time.deltaTime;
            if (enemyStatus.AttackCoolTime <= currentAttackCoolTime 
                && attackTarget.gameObject.TryGetComponent<CharacterBehaviourPresenter>(out CharacterBehaviourPresenter characterBehaviourPresenter))
            {
                Debug.LogWarning("TakeDamage");
                
                //Todo ターゲットにダメージを与える処理を正しく書き直す2024/03/22時点まだ
                
                characterBehaviourPresenter.AllyInfo.Hp -= (int)(enemyStatus.CurrentAttack - characterBehaviourPresenter.AllyInfo.Defence * 0.2);
                Debug.LogWarning(characterBehaviourPresenter.AllyInfo.Hp.ToString());
                
                currentAttackCoolTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameTagsManager.Player)
            && enemyStatus.enemyState != EnemyStatus.EnemyState.Attacking)
        {
            Debug.Log("Discover");
            attackTarget = other.gameObject;
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Attacking);
        }
    }
}
