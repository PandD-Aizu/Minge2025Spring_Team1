using NaughtyAttributes;
using UnityEngine;

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
    private int currentAttack;
    private int currentDefence;
    private Vector3 currentMoveDirection = Vector3.zero;
    
    [Header("EnemyStatusController")]
    [InspectorName("MaxHealth")][SerializeField] private float maxHealth = 1000;
    [InspectorName("InitalAttack")][SerializeField] private int initialAttack = 200;
    [InspectorName("InitialMoveSpeed")][SerializeField] private float initialMoveSpeed = 1f;
    [InspectorName("InitialDefence")][SerializeField] private int initialDefence = 10;
    [InspectorName("SpawnCoolTime")][SerializeField] private float spawnCooltime = 4f;
    [InspectorName("AttackCoolTime")] [SerializeField] private float attackCoolTime = 3f;
    
    //アクセサ
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float CurrentMoveSpeed{get{return currentMoveSpeed;}}
    public float MaxHealth{get{return maxHealth;}}
    public float InitialMoveSpeed{get{return initialMoveSpeed;}}
    public float SpawnCooltime{get{return spawnCooltime;}}
    public int InitialAttack{get { return initialAttack; }}
    public int CurrentAttack{get { return currentAttack; }}
    public int CurrentDefence{get { return currentDefence; }}
    public float AttackCoolTime{get {return attackCoolTime;}}
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
        
    }

    private void Update()
    {
        if(currentHealth < 0)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameSpawnManager.Instance.EnemyDestroy();
    }

    public void TakeDamage(int damage)
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