using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkableCell : MonoBehaviour
{
    [Header("Property")] [SerializeField] private bool isSpawnPointCell = false;
    
    public float weight = 1f;
    public bool isWalkable = true;
    public LayerMask observeTargetLayerMask;
    public LayerMask searchCellLayerMask = new LayerMask();

    private float searchCellRayDistance = 0.7f;
    private float rayDistance = 0.8f;
    private Ray observeTargetRay;
    //以下左右上下のセルを取得するのに用いるRay
    private Ray searchCellRayUpside;
    private Ray searchCellRayDownside;
    private Ray searchCellRayLeftside;
    private Ray searchCellRayRightside;

    private List<EnemyWalkableCell> aroundCells = new List<EnemyWalkableCell>(4);
    private EnemyWalkableCell nextCell = null;
    private EnemyWalkableCell previousCell = null;

    //変数のアクセサ
    public bool IsSpawnPointCell{get => isSpawnPointCell;}
    public List<EnemyWalkableCell> AroundCells{get => aroundCells;}
    // public EnemyWalkableCell NextCell {get{return nextCell;} set{nextCell = value;}}
    // public EnemyWalkableCell PreviousCell {get{return previousCell;} set{previousCell = value;}}
    
    private void Start()
    {
        searchCellLayerMask = LayerMask.GetMask("EnemyWalkable");
        observeTargetRay = new Ray(this.transform.position, this.transform.up);
        searchCellRayUpside = new Ray(this.transform.position, this.transform.forward);
        searchCellRayDownside = new Ray(this.transform.position, -this.transform.forward);
        searchCellRayLeftside = new Ray(this.transform.position, -this.transform.right);
        searchCellRayRightside = new Ray(this.transform.position, this.transform.right);
        InitSurroundingCells();
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

    private void InitSurroundingCells()
    {
        //各Rayで上下左右のセルを取得して格納する
        
        Physics.Raycast(searchCellRayUpside, out RaycastHit raycastHitUpside, searchCellRayDistance);
        Physics.Raycast(searchCellRayDownside, out RaycastHit raycastHitDownside, searchCellRayDistance);
        Physics.Raycast(searchCellRayLeftside, out RaycastHit raycastHitLeftside, searchCellRayDistance);
        Physics.Raycast(searchCellRayRightside, out RaycastHit raycastHitRight, searchCellRayDistance);
        Debug.Log(this.gameObject.name + ": " + raycastHitUpside.collider);
        Debug.Log(this.gameObject.name + ": " + raycastHitDownside.collider);
        Debug.Log(this.gameObject.name + ": " + raycastHitLeftside.collider);
        Debug.Log(this.gameObject.name + ": " + raycastHitRight.collider);
        
        if (raycastHitUpside.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitUpside, out EnemyWalkableCell enemyWalkableCellUpside))
        {
            aroundCells.Add(enemyWalkableCellUpside);
        }

        if (raycastHitDownside.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitDownside, out EnemyWalkableCell enemyWalkableCellDownside))
        {
            aroundCells.Add(enemyWalkableCellDownside);
        }

        if (raycastHitLeftside.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitLeftside, out EnemyWalkableCell enemyWalkableCellLeftside))
        {
            aroundCells.Add(enemyWalkableCellLeftside);
        }

        if (raycastHitRight.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitRight, out EnemyWalkableCell enemyWalkableCellRightside))
        {
            aroundCells.Add(enemyWalkableCellRightside);
        }
    }
    
    private bool TryGetNeighborEnemyWalkableCell(RaycastHit raycastHit, out EnemyWalkableCell resultEnemyWalkableCell)
    {
        resultEnemyWalkableCell = null;
        
        if (raycastHit.collider.gameObject != this 
            && raycastHit.collider.gameObject != this
            && raycastHit.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCell))
        {
            resultEnemyWalkableCell = enemyWalkableCell;
            return true;
        }
        else if (raycastHit.collider.gameObject != this 
                  && raycastHit.collider.gameObject != this)
        {
            if (raycastHit.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellParent))
            {
                resultEnemyWalkableCell = enemyWalkableCellParent;
                return true;
            }
        }

        return false;
    }

}