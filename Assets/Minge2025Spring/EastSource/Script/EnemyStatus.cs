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
    [SerializeField] public Animator animator;
    [InspectorName("MaxHealth")]
    [SerializeField] private float maxHealth = 100;
    [InspectorName("InitalAttack")]
    [SerializeField] private int initialAttack = 10;
    [InspectorName("InitialMoveSpeed")]
    [SerializeField] private float initialMoveSpeed = 1f;
    [InspectorName("InitialDefence")]
    [SerializeField] private int initialDefence = 10;
    [InspectorName("SpawnCoolTime")]
    [SerializeField] private float spawnCooltime = 4f;
    [InspectorName("AttackCoolTime")]
    [SerializeField] private float attackCoolTime = 3f;
    [SerializeField] public string movementAnimationParameter = "Moving";
    [SerializeField] public string attackAnimationParameter = "Attacking";
    
    //アクセサ
    public int InitialAttack            { get => initialAttack; }
    public int CurrentAttack            { get => currentAttack; }
    public int CurrentDefence           { get => currentDefence; }
    public float CurrentHealth          { get => currentHealth; set => currentHealth = value; }
    public float CurrentMoveSpeed       { get => currentMoveSpeed; }
    public float MaxHealth              { get => maxHealth; }
    public float InitialMoveSpeed       { get => initialMoveSpeed; }
    public float SpawnCooltime          { get => spawnCooltime; }
    public float AttackCoolTime         { get => attackCoolTime; }
    public bool IsDead                  { get => isDead; set => isDead = value; }
    public Vector3 CurrentMoveDirection { get => currentMoveDirection; set =>currentMoveDirection = value;}
    
    private void Awake()
    {
        // 各種ステータスを初期化
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

    // @brief ゲームオーバー時に敵を削除する
    // @param sender イベントの送信者, e イベントの引数
    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        if (isDead == false)
            Destroy(this.gameObject);
    }

    // @brief ゲームクリア時に敵を削除する
    // @param sender イベントの送信者, e イベントの引数
    private void GameManager_OnGameClear(object sender, EventArgs e)
    {
        if (isDead == false)
            Destroy(this.gameObject);
    }

    private void Update()
    {
        // 敵のHPが0になった場合
        if (currentHealth < 0)
        {
            GameSpawnManager.Instance.EnemyDestroy();
            GameManager.Instance.ClearJudgement();
            isDead = true;
            Destroy(gameObject);
            GameManager.Instance.JudgeGameOver();
        }
    }
    
    // @brief 敵のHPを減少させる
    // @param damage 減少させるHP
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            DestroyImmediate(this.gameObject);
    }

    // @brief 敵の状態を変更する
    // @param state 変更する状態
    public void ChangeEnemyState(EnemyState state)
    {
        enemyState = state;
    }
}