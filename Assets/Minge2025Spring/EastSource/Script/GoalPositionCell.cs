using System;
using System.Collections.Generic;
using UnityEngine;

public class GoalPositionCell : MonoBehaviour
{
    public List<EnemyWalkableCell> aroundEnemyWalkableCells = new List<EnemyWalkableCell>(4);
    
    private float searchCellRayDistance = 0.7f;
    private float rayDistance = 0.8f;
    //以下左右上下のセルを取得するのに用いるRay
    private Ray searchCellRayUpside;
    private Ray searchCellRayDownside;
    private Ray searchCellRayLeftside;
    private Ray searchCellRayRightside;
    
    private void Start()
    {
        searchCellRayUpside = new Ray(this.transform.position, this.transform.forward);
        searchCellRayDownside = new Ray(this.transform.position, -this.transform.forward);
        searchCellRayLeftside = new Ray(this.transform.position, -this.transform.right);
        searchCellRayRightside = new Ray(this.transform.position, this.transform.right);
        InitSurroundingCells();
        GameManager.Instance.OnUpdateEnemyWalkableCells += GameManager_OnUpdateEnemyWalkableCells;
    }

    private void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * searchCellRayDistance, Color.red);
        Debug.DrawRay(this.transform.position, this.transform.right * searchCellRayDistance, Color.green);
        Debug.DrawRay(this.transform.position, -this.transform.forward * searchCellRayDistance, Color.blue);
        Debug.DrawRay(this.transform.position, -this.transform.right * searchCellRayDistance, Color.black);
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
        Physics.Raycast(searchCellRayRightside, out RaycastHit raycastHitRightside, searchCellRayDistance);

        if (raycastHitUpside.collider != null 
            && raycastHitUpside.collider.gameObject != this//Rayが自身に当たっても無視
            && raycastHitUpside.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellUpside))
        {
            aroundEnemyWalkableCells.Add(enemyWalkableCellUpside);
        }
        else if (raycastHitUpside.collider != null 
                 && raycastHitUpside.collider.gameObject != this)
        {
            if (raycastHitUpside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellUpsideParent))
            {
                aroundEnemyWalkableCells.Add(enemyWalkableCellUpsideParent);
            }
        }

        if (raycastHitDownside.collider != null 
            && raycastHitDownside.collider.gameObject != this
            && raycastHitDownside.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellDownside))
        {
            aroundEnemyWalkableCells.Add(enemyWalkableCellDownside);
        }
        else if (raycastHitDownside.collider != null 
                  && raycastHitDownside.collider.gameObject != this)
        {
            if (raycastHitDownside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellDownsideParent))
            {
                aroundEnemyWalkableCells.Add(enemyWalkableCellDownsideParent);
            }
        }

        if (raycastHitLeftside.collider != null 
            && raycastHitLeftside.collider.gameObject != this
            && raycastHitLeftside.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellLeftside))
        {
            aroundEnemyWalkableCells.Add(enemyWalkableCellLeftside);
        }
        else if (raycastHitLeftside.collider != null 
                 && raycastHitLeftside.collider.gameObject != this)
        {
            if (raycastHitLeftside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellLeftsideParent))
            {
                aroundEnemyWalkableCells.Add(enemyWalkableCellLeftsideParent);
            }
        }

        if (raycastHitRightside.collider != null 
            && raycastHitRightside.collider.gameObject != this
            && raycastHitRightside.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellRightside))
        {
            aroundEnemyWalkableCells.Add(enemyWalkableCellRightside);
        }
        else if (raycastHitLeftside.collider != null 
                 && raycastHitLeftside.collider.gameObject != this)
        {
            if (raycastHitLeftside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellRightsideParent))
            {
                aroundEnemyWalkableCells.Add(enemyWalkableCellRightsideParent);
            }
        }

        foreach (EnemyWalkableCell aroundEnemyWalkableCell in aroundEnemyWalkableCells)
        {
            Debug.Log(aroundEnemyWalkableCell.gameObject.name);
        }
    }
}
