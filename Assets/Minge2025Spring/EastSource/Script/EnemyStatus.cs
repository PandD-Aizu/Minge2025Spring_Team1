using System;
using NaughtyAttributes;
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
    
    private int currentAttack;
    private int currentDefence;
    private float currentHealth;
    private float currentMoveSpeed;
    private bool isDead = false;
    private Vector3 currentMoveDirection = Vector3.zero;
    
    [Header("EnemyStatusController")]
    [SerializeField]public Animator animator;
    [InspectorName("MaxHealth")][SerializeField] private float maxHealth = 100;
    [InspectorName("InitalAttack")][SerializeField] private int initialAttack = 10;
    [InspectorName("InitialMoveSpeed")][SerializeField] private float initialMoveSpeed = 1f;
    [InspectorName("InitialDefence")][SerializeField] private int initialDefence = 10;
    [InspectorName("SpawnCoolTime")][SerializeField] private float spawnCooltime = 4f;
    [InspectorName("AttackCoolTime")] [SerializeField] private float attackCoolTime = 3f;
    [SerializeField] public string movementAnimationParameter = "Moving";
    [SerializeField] public string attackAnimationParameter = "Attacking";
    
    //アクセサ
    public int InitialAttack{get { return initialAttack; }}
    public int CurrentAttack{get { return currentAttack; }}
    public int CurrentDefence{get { return currentDefence; }}
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float CurrentMoveSpeed{get{return currentMoveSpeed;}}
    public float MaxHealth{get{return maxHealth;}}
    public float InitialMoveSpeed{get{return initialMoveSpeed;}}
    public float SpawnCooltime{get{return spawnCooltime;}}
    public float AttackCoolTime{get {return attackCoolTime;}}
    
    public bool IsDead { get => isDead; set => isDead = value; }
    public Vector3 CurrentMoveDirection{get{return currentMoveDirection;} set{currentMoveDirection = value;}}

    private void Awake()
    {
        currentHealth = maxHealth;
        currentMoveSpeed = initialMoveSpeed;
        currentAttack = initialAttack;
        currentDefence = initialDefence;
    }

    private void Start()
    {
        GameManager.Instance.OnGameClear += GameManager_OnGameClear;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        if (isDead == false)
        {
            Destroy(this.gameObject);
        }
    }

    private void GameManager_OnGameClear(object sender, EventArgs e)
    {
        if (isDead == false)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameSpawnManager.Instance.EnemyDestroy();
            GameManager.Instance.ClearJudgement();
            isDead = true;
            DestroyImmediate(this.gameObject);
        }
    }

    public void ChangeEnemyState(EnemyState state)
    {
        enemyState = state;
    }
}