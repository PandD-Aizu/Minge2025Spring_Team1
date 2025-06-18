using System;
using System.Collections.Generic;
using FMODUnity;
using General;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 goalPosition;
    private Vector3 directionUnderY = new Vector3(0, -1, 0);
    private Vector3 currentMoveDirection;
    private Vector3 prevPosition = Vector3.zero;
    private Vector3 prevTargetCellPosition = Vector3.zero;
    private RaycastHit currentPositionCellHit;
    private Animator animator;
    private GameObject currentPositionCell;
    private EnemyStatus enemyStatus;
    private EnemyWalkableCell prevEnemyWalkableCell;

    private const float MAX_RAYCAST_DISTANCE = 1f;
    
    // private const float MOVE_SPEED_COEFFICIENT = 0.01f;　ほんとはだめだけど念のため速度の定数をコメントアウトで残しておく
    
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
    
    [Header("SE")]
    [SerializeField] private StudioEventEmitter goalEmitter;
    
    public bool IsMoving {get => isMoving; set => isMoving = value;}
    private void Start()
    {
        
    }

    private void Update()
    {
        // pause中に敵を止める
        if (Time.timeScale == 0f) return;
        
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        Debug.DrawRay(transform.position, directionUnderY, Color.red, 1f);
        
        UpdateMoveDirection();
        if (enemyStatus.enemyState == EnemyStatus.EnemyState.Moving)
        {
            animator.SetBool(enemyStatus.movementAnimationParameter, true);
        }
        else
        {
            animator.SetBool(enemyStatus.movementAnimationParameter, false);
        }
    }

    //brief エネミーを移動させる関数
    private void EnemyMove(EnemyWalkableCell enemyWalkableCell)
    {
        
    }

    private void UpdateMoveDirection()
    {
        Vector3 position = transform.position;
        currentMoveDirection = position - prevPosition;
        prevPosition = position;
        enemyStatus.CurrentMoveDirection = currentMoveDirection;
    }
    
}
