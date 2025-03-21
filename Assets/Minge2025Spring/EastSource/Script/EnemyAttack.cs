using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private enum EnemyAttackType
    {
        None,
        Melee,
        LongRange,
    }
    
    private EnemyStatus enemyStatus;

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
    }

    //breif TypeがMeleeのときにUpdateで使う
    private void MeleeAttackObserver()
    {
        Physics.Raycast(this.transform.position, enemyStatus.CurrentMoveDirection, out RaycastHit hit, meleeAttackRange, observeTargetLayerMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Idle);
            }
            else
            {
                enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
            }
        }
        else
        {
            enemyStatus.ChangeEnemyState(EnemyStatus.EnemyState.Moving);
        }
    }
}
