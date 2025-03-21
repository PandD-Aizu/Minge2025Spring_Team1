using System;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStatus : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Moving,
        Attacking,
    }

    public EnemyState enemyState = EnemyState.Moving;
    
    private float currentHealth;
    private float currentMoveSpeed;
    private float currentAttack;
    private Vector3 currentMoveDirection = Vector3.zero;
    
    [Header("EnemyStatusController")]
    [InspectorName("MaxHealth")][SerializeField] private float maxHealth = 100f;
    [InspectorName("InitalAttack")][SerializeField] private float initialAttack = 10f;
    [InspectorName("InitialMoveSpeed")][SerializeField] private float initialMoveSpeed = 1f;
    [InspectorName("SpawnCoolTime")][SerializeField] private float spawnCooltime = 4f;
    
    //アクセサ
    public float CurrentHealth{get{return currentHealth;}}
    public float CurrentMoveSpeed{get{return currentMoveSpeed;}}
    public float MaxHealth{get{return maxHealth;}}
    public float InitialMoveSpeed{get{return initialMoveSpeed;}}
    public float SpawnCooltime{get{return spawnCooltime;}}
    public float InitialAttack{get { return initialAttack; }}
    public float CurrentAttack{get { return currentAttack; }}
    public Vector3 CurrentMoveDirection{get{return currentMoveDirection;} set{currentMoveDirection = value;}}

    private void Awake()
    {
        currentHealth = maxHealth;
        currentMoveSpeed = initialMoveSpeed;
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void ChangeEnemyState(EnemyState state)
    {
        enemyState = state;
    }
}
