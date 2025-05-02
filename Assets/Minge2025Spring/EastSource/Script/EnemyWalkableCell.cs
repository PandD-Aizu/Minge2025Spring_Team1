using System;
using UnityEngine;

public class EnemyWalkableCell : MonoBehaviour
{
    [Header("Property")] 
    [SerializeField] private bool isSpawnPointCell = false;
    
    public bool isWalkable = true;
    public LayerMask observeTargetLayerMask;
    public LayerMask searchCellLayerMask = new LayerMask();

    private float searchCellRayDistance = 0.7f;
    private float rayDistance = 0.8f;
    private Ray observeTargetRay;
    
    // 以下左右上下のセルを取得するのに用いるRay
    private Ray searchCellRayUpside;
    private Ray searchCellRayDownside;
    private Ray searchCellRayLeftside;
    private Ray searchCellRayRightside;
    
    // 上下左右のセル
    private EnemyWalkableCell upCell = null;
    private EnemyWalkableCell downCell = null;
    private EnemyWalkableCell leftCell = null;
    private EnemyWalkableCell rightCell = null;
    
    // 次のセルと前のセル
    private EnemyWalkableCell nextCell = null;
    private EnemyWalkableCell previousCell = null;

    //変数のアクセサ
    public bool IsSpawnPointCell          { get => isSpawnPointCell; }
    public EnemyWalkableCell UpCell       { get => upCell; }
    public EnemyWalkableCell DownCell     { get => downCell; }
    public EnemyWalkableCell LeftCell     { get => leftCell; }
    public EnemyWalkableCell RightCell    { get => rightCell; }
    public EnemyWalkableCell NextCell     { get => nextCell; set => nextCell = value; }
    public EnemyWalkableCell PreviousCell { get => previousCell; set => previousCell = value; }
    
    private void Start()
    {
        searchCellLayerMask = LayerMask.GetMask("EnemyWalkable");                      // 探索するレイヤーのマスク
        observeTargetRay = new Ray(this.transform.position, this.transform.up);            // 
        
        searchCellRayUpside = new Ray(this.transform.position, this.transform.forward);    // 上方向のレイ
        searchCellRayDownside = new Ray(this.transform.position, -this.transform.forward); // 下方向のレイ
        searchCellRayLeftside = new Ray(this.transform.position, -this.transform.right);   // 左方向のレイ
        searchCellRayRightside = new Ray(this.transform.position, this.transform.right);   // 右方向のレイ
        
        InitSurroundingCells();
        
        GameManager.Instance.OnUpdateEnemyWalkableCells += GameManager_OnUpdateEnemyWalkableCells;
    }

    private void Update()
    {
        // レイキャストを描画
        Debug.DrawRay(transform.position, transform.forward * searchCellRayDistance, Color.red);
        Debug.DrawRay(transform.position, transform.right * searchCellRayDistance, Color.green);
        Debug.DrawRay(transform.position, -transform.forward * searchCellRayDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * searchCellRayDistance, Color.black);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUpdateEnemyWalkableCells -= GameManager_OnUpdateEnemyWalkableCells;
    }
    
    private void GameManager_OnUpdateEnemyWalkableCells(object sender, EventArgs e)
    {
        InitSurroundingCells();
    }

    // @brief 敵の歩行可能なセルを構築する
    private void InitSurroundingCells()
    {
        //各Rayで上下左右のセルを取得して格納する
        Physics.Raycast(searchCellRayUpside, out RaycastHit raycastHitUpside, searchCellRayDistance);
        Physics.Raycast(searchCellRayDownside, out RaycastHit raycastHitDownside, searchCellRayDistance);
        Physics.Raycast(searchCellRayLeftside, out RaycastHit raycastHitLeftside, searchCellRayDistance);
        Physics.Raycast(searchCellRayRightside, out RaycastHit raycastHitRight, searchCellRayDistance);
        
        // 何かに衝突していて、かつ衝突したオブジェクトが自身ではなく、かつそのオブジェクトがEnemyWalkableCellを持っている場合
        if (raycastHitUpside.collider != null 
            && raycastHitUpside.collider.gameObject != this
            && raycastHitUpside.collider.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellUpside))
        {
            upCell = enemyWalkableCellUpside;
        }
        // 何かに衝突していて、かつ衝突したオブジェクトが自身ではなく、かつ衝突したオブジェクトの親がEnemyWalkableCellを持っている場合
        else if (raycastHitUpside.collider != null 
                 && raycastHitUpside.collider.gameObject != this)
        {
            if (raycastHitUpside.collider.gameObject.transform.parent.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellUpsideParent))
                upCell = enemyWalkableCellUpsideParent;
        }

        if (raycastHitDownside.collider != null 
            && raycastHitDownside.collider.gameObject != this
            && raycastHitDownside.collider.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellDownside))
        {
            downCell = enemyWalkableCellDownside;
        }
        else if (raycastHitDownside.collider != null 
                  && raycastHitDownside.collider.gameObject != this)
        {
            if (raycastHitDownside.collider.gameObject.transform.parent.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellDownsideParent))
                downCell = enemyWalkableCellDownsideParent;
        }

        if (raycastHitLeftside.collider != null 
            && raycastHitLeftside.collider.gameObject != this
            && raycastHitLeftside.collider.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellLeftside))
        {
            leftCell = enemyWalkableCellLeftside;
        }
        else if (raycastHitLeftside.collider != null 
                 && raycastHitLeftside.collider.gameObject != this)
        {
            if (raycastHitLeftside.collider.gameObject.transform.parent.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellLeftsideParent))
                leftCell = enemyWalkableCellLeftsideParent;
        }

        if (raycastHitRight.collider != null 
            && raycastHitRight.collider.gameObject != this
            && raycastHitRight.collider.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellRightside))
        {
            rightCell = enemyWalkableCellRightside;
        }
        else if (raycastHitLeftside.collider != null 
                 && raycastHitLeftside.collider.gameObject != this)
        {
            if (raycastHitLeftside.collider.gameObject.transform.parent.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCellLeftsideParent))
                leftCell = enemyWalkableCellLeftsideParent;
        }
    }
}