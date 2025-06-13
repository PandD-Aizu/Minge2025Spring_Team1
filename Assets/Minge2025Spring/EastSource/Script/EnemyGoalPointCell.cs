using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoalPointCell : MonoBehaviour
{
    public LayerMask searchCellLayerMask = new LayerMask();
    
    private float searchCellRayDistance = 0.7f;
    private float rayDistance = 0.8f;
    private Ray observeTargetRay;
    private List<EnemyWalkableCell> connectEnemyWalkableCells = new List<EnemyWalkableCell>(4);
    //以下左右上下のセルを取得するのに用いるRay
    private Ray searchCellRayUpside;
    private Ray searchCellRayDownside;
    private Ray searchCellRayLeftside;
    private Ray searchCellRayRightside;
    
    //アクセサ
    public List<EnemyWalkableCell> ConnectEnemyWalkableCells {get => connectEnemyWalkableCells;}
    
    private void Start()
    {
        searchCellLayerMask = LayerMask.GetMask("EnemyWalkable");
        observeTargetRay = new Ray(this.transform.position, this.transform.up);
        searchCellRayUpside = new Ray(this.transform.position, this.transform.forward);
        searchCellRayDownside = new Ray(this.transform.position, -this.transform.forward);
        searchCellRayLeftside = new Ray(this.transform.position, -this.transform.right);
        searchCellRayRightside = new Ray(this.transform.position, this.transform.right);
        InitSurroundingCells();
        GameManager.Instance.OnUpdateEnemyWalkableCells += GameManger_OnUpdateEnemyWalkableCells;
    }

    private void GameManger_OnUpdateEnemyWalkableCells(object sender, EventArgs e)
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
        
        if (TryGetNeighborEnemyWalkableCell(raycastHitUpside, out EnemyWalkableCell enemyWalkableCellUpside))
        {
            connectEnemyWalkableCells.Add(enemyWalkableCellUpside);
        }

        if (TryGetNeighborEnemyWalkableCell(raycastHitDownside, out EnemyWalkableCell enemyWalkableCellDownside))
        {
            connectEnemyWalkableCells.Add(enemyWalkableCellDownside);
        }

        if (TryGetNeighborEnemyWalkableCell(raycastHitLeftside, out EnemyWalkableCell enemyWalkableCellLeftside))
        {
            connectEnemyWalkableCells.Add(enemyWalkableCellLeftside);
        }

        if (TryGetNeighborEnemyWalkableCell(raycastHitRight, out EnemyWalkableCell enemyWalkableCellRightside))
        {
            connectEnemyWalkableCells.Add(enemyWalkableCellRightside);
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
