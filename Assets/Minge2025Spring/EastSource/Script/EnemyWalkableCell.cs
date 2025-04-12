using System;
using UnityEngine;

public class EnemyWalkableCell : MonoBehaviour
{
    [Header("Property")] [SerializeField] private bool isSpawnPointCell = false;
    
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
    
    private EnemyWalkableCell upCell = null;
    private EnemyWalkableCell downCell = null;
    private EnemyWalkableCell leftCell = null;
    private EnemyWalkableCell rightCell = null;
    private EnemyWalkableCell nextCell = null;
    private EnemyWalkableCell previousCell = null;

    //変数のアクセサ
    public bool IsSpawnPointCell{get{return isSpawnPointCell;}}
    public EnemyWalkableCell UpCell {get{return upCell;}}
    public EnemyWalkableCell DownCell {get{return downCell;}}
    public EnemyWalkableCell LeftCell {get{return leftCell;}}
    public EnemyWalkableCell RightCell {get{return rightCell;}}
    public EnemyWalkableCell NextCell {get{return nextCell;} set{nextCell = value;}}
    public EnemyWalkableCell PreviousCell {get{return previousCell;} set{previousCell = value;}}
    
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
        Physics.Raycast(searchCellRayRightside, out RaycastHit raycastHitRight, searchCellRayDistance);
        Debug.LogWarning(this.gameObject.name + ": " + raycastHitUpside.collider);
        Debug.LogWarning(this.gameObject.name + ": " + raycastHitDownside.collider);
        Debug.LogWarning(this.gameObject.name + ": " + raycastHitLeftside.collider);
        Debug.LogWarning(this.gameObject.name + ": " + raycastHitRight.collider);
        if (raycastHitUpside.collider != null 
            && raycastHitUpside.collider.gameObject != this
            && raycastHitUpside.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellUpside))
        {
            upCell = enemyWalkableCellUpside;
        }
        else if (raycastHitUpside.collider != null 
                 && raycastHitUpside.collider.gameObject != this)
        {
            if (raycastHitUpside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellUpsideParent))
            {
                upCell = enemyWalkableCellUpsideParent;
            }
        }

        if (raycastHitDownside.collider != null 
            && raycastHitDownside.collider.gameObject != this
            && raycastHitDownside.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellDownside))
        {
            downCell = enemyWalkableCellDownside;
        }
        else if (raycastHitDownside.collider != null 
                  && raycastHitDownside.collider.gameObject != this)
        {
            if (raycastHitDownside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellDownsideParent))
            {
                downCell = enemyWalkableCellDownsideParent;
            }
        }

        if (raycastHitLeftside.collider != null 
            && raycastHitLeftside.collider.gameObject != this
            && raycastHitLeftside.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellLeftside))
        {
            leftCell = enemyWalkableCellLeftside;
        }
        else if (raycastHitLeftside.collider != null 
                 && raycastHitLeftside.collider.gameObject != this)
        {
            if (raycastHitLeftside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellLeftsideParent))
            {
                leftCell = enemyWalkableCellLeftsideParent;
            }
        }

        if (raycastHitRight.collider != null 
            && raycastHitRight.collider.gameObject != this
            && raycastHitRight.collider.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellRightside))
        {
            rightCell = enemyWalkableCellRightside;
        }
        else if (raycastHitLeftside.collider != null 
                 && raycastHitLeftside.collider.gameObject != this)
        {
            if (raycastHitLeftside.collider.gameObject.transform.parent.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellLeftsideParent))
            {
                leftCell = enemyWalkableCellLeftsideParent;
            }
        }
        
        if (upCell != null)
        {
            Debug.Log(this.gameObject.name + ": upside is " + upCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": upside is null.");
        }

        if (downCell != null)
        {
            Debug.Log(this.gameObject.name + ": downside is " + downCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": downside is null.");
        }

        if (rightCell != null)
        {
            Debug.Log(this.gameObject.name + ": rightside is " + rightCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": rightside is null.");
        }

        if (leftCell)
        {
            Debug.Log(this.gameObject.name + ": leftside is " + leftCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": leftside is null.");
        }
    }
}