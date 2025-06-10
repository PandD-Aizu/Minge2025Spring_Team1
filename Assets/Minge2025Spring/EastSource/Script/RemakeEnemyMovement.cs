using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


public class RemakeEnemyMovement : MonoBehaviour
{
    private float maxRayDistance = 1f;
    private Ray getCurrentPositionCellRay;
    private LayerMask searchCurrentPositionCellLayerMask = LayerMask.NameToLayer("EnemyWalkable");
    private List<EnemyWalkableCell> movementRootCellList = new List<EnemyWalkableCell>();
    private List<EnemyWalkableCell> passedCellsList = new List<EnemyWalkableCell>();
    private EnemyWalkableCell currentPositionCell;
    private GoalPositionCell goalPosition;
    
    //searchMovementRoot()で使うメモリたち
    private void Start()
    {
        goalPosition = GameManager.Instance.CallRandomGoalPosition();
        getCurrentPositionCellRay = new Ray(this.gameObject.transform.position, Vector3.down);
    }
    
    private void SearchMovementRoot()
    {
        //初期化
        int maxSteps = 1000; // 無限ループ防止
        int stepCount = 0;
        bool isCreatedNewRoot = false;//一つでも上下左右に経路が広がったかどうか
        GameManager.Instance.UpdateEnemyGoalPointCells();
        GameManager.Instance.UpdateEnemyWalkableCells();
        GetCurrentPositionCell();//Rayを飛ばしてEnemyWalkableCellを取得
        EnemyWalkableCell operatingCell = currentPositionCell;//現在検索しているCell
        EnemyWalkableCell connectGoalCell = null;
        Dictionary<List<EnemyWalkableCell>, float> terminalCellDatas = new Dictionary<List<EnemyWalkableCell>, float>(64);//調べた経路を収納
        List<EnemyWalkableCell> operatingCellDataList = new List<EnemyWalkableCell>(32);//経路本体

        //探索するためのループ
        while (connectGoalCell == null)
        {
            //terminalCellDatasにDataがなかった時、初期化処理
            if (terminalCellDatas.Count == 0)
            {
                InitTerminalCells(terminalCellDatas, operatingCell);
            }

            //距離が一番短い経路をソートして取り出す。
            
            
            //取り出したらセルリストを消す

            //経路で末端のCellを取り出す
            
            //現在セルの上下左右のセルを調べる。あれば登録する
            
            stepCount++;
            if (stepCount > maxSteps)
            {
                Debug.LogError("SearchShortestRoot exceeded max steps! Breaking loop.");
                break;
            }
        }
        
        //ループから抜けたら最短経路をmovementRootCellListに格納
        movementRootCellList = terminalCellDatas[terminalCellDatas.Count - 1];

    }
    
    /*ここからはSearchMovement関数内で使用する関数*/
    
    //新しくterminalデータを作る
    private void InitTerminalCells(Dictionary<List<EnemyWalkableCell>, float> terminalCellDatas, EnemyWalkableCell operatingCell)
    {
        
    }

    //経路を記録する
    
    //Sortしてかつ適当なタイミングで最短経路を返す

    //Rayを飛ばしてEnemyWalkableCellを取得
    private void GetCurrentPositionCell()
    {
        Physics.Raycast(getCurrentPositionCellRay, out RaycastHit hit, maxRayDistance, searchCurrentPositionCellLayerMask);
        currentPositionCell = hit.collider.gameObject.GetComponent<EnemyWalkableCell>();
    }
    
    //周りのCellの情報を開いて適当に情報を処理する
    private void OpenArroudCells()
    {
        
    }

    //通ったことのあるCellかどうか確認
    //通ったことがあればTrueなければFalseを返す
    private bool CheckExistedPassedCellList(EnemyWalkableCell confirmationCell)
    {
        foreach (EnemyWalkableCell passedCell in passedCellsList)
        {
            if (passedCell == confirmationCell)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    //末端セルがゴールセルと隣接しているか確認
    private bool CheckAdjustmentGoal(EnemyWalkableCell enemyWalkableCell)
    {
        if (condition_CheckAdjustmentGoal(enemyWalkableCell))
        {
            return true;
        }
        return false;
    }

    //CheckAdjustmentGoalのIf文の条件式
    private bool condition_CheckAdjustmentGoal(EnemyWalkableCell enemyWalkableCell)
    {
        foreach (EnemyWalkableCell aroundEnemyWalkableCell in goalPosition.aroundEnemyWalkableCells)
        {
            if (aroundEnemyWalkableCell == enemyWalkableCell)
            {
                return true;
            }
        }
        return false;
    }
}