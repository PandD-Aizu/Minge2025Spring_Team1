using System;
using System.Collections.Generic;
using UnityEngine;
public class CharacterPlacementCell : IEnemyWalkableCell
{
    private float searchCellRayDistance = 0.7f;
    private float rayDistance = 0.8f;
    //以下左右上下のセルを取得するのに用いるRay
    private Ray searchCellRayUpside;
    private Ray searchCellRayDownside;
    private Ray searchCellRayLeftside;
    private Ray searchCellRayRightside;
    
    [SerializeField]private List<EnemyWalkableCell> aroundCells = new List<EnemyWalkableCell>(4);
    private EnemyWalkableCell nextCell = null;
    private EnemyWalkableCell previousCell = null;
    
    //アクセサ
    public List<EnemyWalkableCell> AroundCells { get => aroundCells; }

    public override void Initialize()
    {
        searchCellRayUpside = new Ray(this.transform.position, this.transform.forward);
        searchCellRayDownside = new Ray(this.transform.position, -this.transform.forward);
        searchCellRayLeftside = new Ray(this.transform.position, -this.transform.right);
        searchCellRayRightside = new Ray(this.transform.position, this.transform.right);
        InitSurroundingCells();
    }
    
    

    public override void InitSurroundingCells()
    {
        //各Rayで上下左右のセルを取得して格納する
        
        Physics.Raycast(searchCellRayUpside, out RaycastHit raycastHitUpside, searchCellRayDistance);
        Physics.Raycast(searchCellRayDownside, out RaycastHit raycastHitDownside, searchCellRayDistance);
        Physics.Raycast(searchCellRayLeftside, out RaycastHit raycastHitLeftside, searchCellRayDistance);
        Physics.Raycast(searchCellRayRightside, out RaycastHit raycastHitRight, searchCellRayDistance);
        // Debug.LogWarning(this.gameObject.name + ": " + raycastHitUpside.collider);
        // Debug.LogWarning(this.gameObject.name + ": " + raycastHitDownside.collider);
        // Debug.LogWarning(this.gameObject.name + ": " + raycastHitLeftside.collider);
        // Debug.LogWarning(this.gameObject.name + ": " + raycastHitRight.collider);
        
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

        foreach (var aroundCell in aroundCells)
        {
            Debug.Log(this.gameObject.name + ": connect" + aroundCell.gameObject.name);
        }
    }

    public override bool TryGetNeighborEnemyWalkableCell(RaycastHit raycastHit, out EnemyWalkableCell resultEnemyWalkableCell)
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
