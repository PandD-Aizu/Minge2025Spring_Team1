using System;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStatus : MonoBehaviour
{
    private float currentHealth;
    private float currentMoveSpeed;
    
    [Header("EnemyStatusController")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float initialMoveSpeed = 1f;
    [SerializeField] private float spawnCooltime = 4f;
    
    //アクセサ
    public float CurrentHealth{get{return currentHealth;}}
    public float CurrentMoveSpeed{get{return currentMoveSpeed;}}
    public float MaxHealth{get{return maxHealth;}}
    public float InitialMoveSpeed{get{return initialMoveSpeed;}}
    public float SpawnCooltime{get{return spawnCooltime;}}

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
    
}
