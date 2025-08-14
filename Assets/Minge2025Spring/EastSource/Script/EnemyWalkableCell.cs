 using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkableCell : BaseEnemyWalkable
{
    [Header("Property")] [SerializeField] private bool isSpawnPointCell = false;

    //変数のアクセサ
    public bool IsSpawnPointCell{get => isSpawnPointCell;}

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameManager.Instance.OnUpdateEnemyWalkableCells += GameManager_OnUpdateEnemyWalkableCells;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUpdateEnemyWalkableCells -= GameManager_OnUpdateEnemyWalkableCells;
    }

    private void GameManager_OnUpdateEnemyWalkableCells(object sender, EventArgs e)
    {
        InitSurroundingCells();
    }
}