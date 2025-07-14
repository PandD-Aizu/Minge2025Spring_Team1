using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyWalkable : MonoBehaviour
{
    public bool isWalkable = true;
    public LayerMask searchCellLayerMask = new LayerMask();
    
    protected float searchCellRayDistance = 0.7f;
    //以下左右上下のセルを取得するのに用いるRay
    protected Ray searchCellRayUpside;
    protected Ray searchCellRayDownside;
    protected Ray searchCellRayLeftside;
    protected Ray searchCellRayRightside;
    
    
    [SerializeField]protected List<BaseEnemyWalkable> aroundCells = new List<BaseEnemyWalkable>(4);
    
    public List<BaseEnemyWalkable> AroundCells{get => aroundCells;}
    
    protected void Awake()
    {
        searchCellLayerMask = LayerMask.GetMask("EnemyWalkable", "CharacterPlacement");
        searchCellRayUpside = new Ray(this.transform.position, this.transform.forward);
        searchCellRayDownside = new Ray(this.transform.position, -this.transform.forward);
        searchCellRayLeftside = new Ray(this.transform.position, -this.transform.right);
        searchCellRayRightside = new Ray(this.transform.position, this.transform.right);
        InitSurroundingCells();
    }
    
    private void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.red, 1f);
        Debug.DrawRay(this.transform.position, -this.transform.forward, Color.green, 1f);
    }
    
    protected void InitSurroundingCells()
    {
        //各Rayで上下左右のセルを取得して格納する
        
        Physics.Raycast(searchCellRayUpside, out RaycastHit raycastHitUpside, searchCellRayDistance);
        Physics.Raycast(searchCellRayDownside, out RaycastHit raycastHitDownside, searchCellRayDistance);
        Physics.Raycast(searchCellRayLeftside, out RaycastHit raycastHitLeftside, searchCellRayDistance);
        Physics.Raycast(searchCellRayRightside, out RaycastHit raycastHitRight, searchCellRayDistance);
        Debug.LogWarning(this.gameObject.name + " Up: " + raycastHitUpside.collider);
        Debug.LogWarning(this.gameObject.name + " Down: " + raycastHitDownside.collider);
        Debug.LogWarning(this.gameObject.name + " Left: " + raycastHitLeftside.collider);
        Debug.LogWarning(this.gameObject.name + " Right: " + raycastHitRight.collider);
        
        if (raycastHitUpside.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitUpside, out BaseEnemyWalkable baseEnemyWalkableCellUpside)
            && baseEnemyWalkableCellUpside.isWalkable)
        {
            aroundCells.Add(baseEnemyWalkableCellUpside);
        }

        if (raycastHitDownside.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitDownside, out BaseEnemyWalkable baseEnemyWalkableCellDownside)
            && baseEnemyWalkableCellDownside.isWalkable)
        {
            aroundCells.Add(baseEnemyWalkableCellDownside);
        }

        if (raycastHitLeftside.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitLeftside, out BaseEnemyWalkable baseEnemyWalkableCellLeftside)
            && baseEnemyWalkableCellLeftside.isWalkable)
        {
            aroundCells.Add(baseEnemyWalkableCellLeftside);
        }

        if (raycastHitRight.collider != null && TryGetNeighborEnemyWalkableCell(raycastHitRight, out BaseEnemyWalkable baseEnemyWalkableCellRightside)
            && baseEnemyWalkableCellRightside.isWalkable)
        {
            aroundCells.Add(baseEnemyWalkableCellRightside);
        }

        foreach (var aroundCell in aroundCells)
        {
            Debug.Log(this.gameObject.name + ": connect" + aroundCell.gameObject.name);
        }
    }

    public void UpdateSurroundingCells()
    {
        InitSurroundingCells();
    }

    protected bool TryGetNeighborEnemyWalkableCell(RaycastHit raycastHit, out BaseEnemyWalkable resultEnemyWalkableCell)
    {
        resultEnemyWalkableCell = null;
        
        if (raycastHit.collider.gameObject != this 
            && raycastHit.collider.gameObject != this
            && raycastHit.collider.gameObject.TryGetComponent<BaseEnemyWalkable>(out BaseEnemyWalkable baseEnemyWalkable))
        {
            resultEnemyWalkableCell = baseEnemyWalkable;
            return true;
        }
        else if (raycastHit.collider.gameObject != this 
                 && raycastHit.collider.gameObject != this)
        {
            if (raycastHit.collider.gameObject.transform.parent.gameObject.TryGetComponent<BaseEnemyWalkable>(out BaseEnemyWalkable enemyWalkableCellParent))
            {
                resultEnemyWalkableCell = enemyWalkableCellParent;
                return true;
            }
        }

        return false;
    }
}
