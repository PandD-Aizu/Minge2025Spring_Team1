using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RemakeEnemyMovement : MonoBehaviour
{
    public class TerminalCellData
    {
        public EnemyWalkableCell cellInfo;
        public TerminalCellData nextCell = null;
        public TerminalCellData prevCell = null;

        public TerminalCellData(EnemyWalkableCell cellInfo, TerminalCellData nextCell, TerminalCellData prevCell)
        {
            this.cellInfo = cellInfo;
            this.nextCell = nextCell;
            this.prevCell = prevCell;
        }
    }
    
    private float maxRayDistance = 1f;
    private Vector3 goalPosition;
    private Ray getCurrentPositionCellRay;
    private List<EnemyWalkableCell> passedCellsList = new List<EnemyWalkableCell>();
    private LayerMask searchCurrentPositionCellLayerMask = LayerMask.NameToLayer("EnemyWalkable");
    private EnemyWalkableCell currentPositionCell;
    private List<TerminalCellData> movementRootCellList = new List<TerminalCellData>();
    
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
        GetCurrentPositionCell();
        EnemyWalkableCell terminalCell;
        EnemyWalkableCell operatingCell;
        EnemyWalkableCell connectGoalCell = null;
        List<List<TerminalCellData>> terminalCellDatas = new List<List<TerminalCellData>>();
        List<TerminalCellData> operatingCellDataList = new List<TerminalCellData>();

        //探索するためのループ
        while (connectGoalCell == null)
        {
            //terminalCellDatasにDataがなかった時、初期化処理
            if (terminalCellDatas.Count == 0)
            {
                InitSerchMovementRoot(terminalCellDatas);
            }

            operatingCellDataList = SortTerminalCellDatas(terminalCellDatas);
            
            terminalCellDatas.Remove(operatingCellDataList);//取り出したらセルリストを消す

            operatingCell = operatingCellDataList[operatingCellDataList.Count - 1/*経路の一番深いCellの番号*/].cellInfo.gameObject.GetComponent<EnemyWalkableCell>();
                

            //現在セルの上下左右のセルを調べる。あれば登録する
            if (operatingCell.UpCell != null
                && CheckExistedPassedCellList(operatingCell.UpCell)
                && operatingCell.UpCell.isWalkable)
            {
                RecordCreateTerminalCell(operatingCell.UpCell, operatingCellDataList, terminalCellDatas);
                passedCellsList.Add(operatingCell.UpCell);//通ったセルを記憶する
                if (checkAdjustmentGoal(operatingCell.UpCell))
                {
                    connectGoalCell = operatingCell.UpCell;
                    break;
                }
                isCreatedNewRoot = true;
            }
            if (operatingCell.DownCell != null
                && CheckExistedPassedCellList(operatingCell.DownCell)
                && operatingCell.DownCell.isWalkable)
            {
                RecordCreateTerminalCell(operatingCell.DownCell, operatingCellDataList, terminalCellDatas);
                passedCellsList.Add(operatingCell.DownCell);//通ったセルを記憶する
                if (checkAdjustmentGoal(operatingCell.DownCell))
                {
                    connectGoalCell = operatingCell.DownCell;
                    break;
                }
                isCreatedNewRoot = true;
            }
            if (operatingCell.LeftCell != null
                && CheckExistedPassedCellList(operatingCell.LeftCell)
                && operatingCell.LeftCell.isWalkable)
            {
                RecordCreateTerminalCell(operatingCell.LeftCell, operatingCellDataList, terminalCellDatas);
                passedCellsList.Add(operatingCell.LeftCell);//通ったセルを記憶する
                if (checkAdjustmentGoal(operatingCell.LeftCell))
                {
                    connectGoalCell = operatingCell.LeftCell;
                    break;
                }
                isCreatedNewRoot = true;
            }
            if (operatingCell.RightCell != null
                && CheckExistedPassedCellList(operatingCell.RightCell)
                && operatingCell.RightCell.isWalkable)
            {
                RecordCreateTerminalCell(operatingCell.RightCell, operatingCellDataList, terminalCellDatas);
                passedCellsList.Add(operatingCell.RightCell);//通ったセルを記憶する
                if (checkAdjustmentGoal(operatingCell.RightCell))
                {
                    connectGoalCell = operatingCell.RightCell;
                    break;
                }
                isCreatedNewRoot = true;
            }
            if (!isCreatedNewRoot)
            {
                terminalCellDatas.Add(operatingCellDataList);//取り出したらセルリストを挿入しなおす
            }
            
            stepCount++;
            if (stepCount > maxSteps)
            {
                Debug.LogError("SearchShortestRoot exceeded max steps! Breaking loop.");
                break;
            }
        }
        
        movementRootCellList = terminalCellDatas[terminalCellDatas.Count - 1];

    }
    
    //新しくterminalデータを作る
    private void InitSerchMovementRoot(List<List<TerminalCellData>> terminalCellDatas)
    {
        GetCurrentPositionCell();
        TerminalCellData initTerminalCellData = new TerminalCellData(
            cellInfo: currentPositionCell,
            nextCell: null,
            prevCell: null
        );
        terminalCellDatas.Add(new List<TerminalCellData>(){initTerminalCellData});
        passedCellsList.Add(currentPositionCell);//通ったセルを記憶する
    }

    //経路を記録する
    private void RecordCreateTerminalCell(EnemyWalkableCell registedCell, List<TerminalCellData> operatingCellDataList, List<List<TerminalCellData>> terminalCellDatas)
    {
        List<TerminalCellData> newCellDataList = new List<TerminalCellData>(operatingCellDataList);
        newCellDataList.Add(new TerminalCellData(
            cellInfo: registedCell, 
            nextCell: null,
            prevCell: null
        ));
        terminalCellDatas.Add(newCellDataList);
    }
    
    //Sortしてかつ適当なタイミングで最短経路を返す
    private List<TerminalCellData> SortTerminalCellDatas(List<List<TerminalCellData>> terminalCellDatas)
    {
        List<TerminalCellData> shortTerminalCellRootData = new List<TerminalCellData>(terminalCellDatas[0]);
        foreach (var terminalCellData in terminalCellDatas)
        {
            if (shortTerminalCellRootData.Count < terminalCellData.Count)
            {
                shortTerminalCellRootData = terminalCellData;
            }
        }
        return shortTerminalCellRootData;
    }
    

    //Rayを飛ばしてEnemyWalkableCellを取得
    private void GetCurrentPositionCell()
    {
        Physics.Raycast(getCurrentPositionCellRay, out RaycastHit hit, maxRayDistance, searchCurrentPositionCellLayerMask);
        currentPositionCell = hit.collider.gameObject.GetComponent<EnemyWalkableCell>();
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
    private bool checkAdjustmentGoal(EnemyWalkableCell enemyWalkableCell)
    {
        if ((Mathf.Abs(enemyWalkableCell.gameObject.transform.position.x - goalPosition.x) == 1 
             && Mathf.Abs(enemyWalkableCell.gameObject.transform.position.z - goalPosition.z) == 0)
            || (Mathf.Abs(enemyWalkableCell.gameObject.transform.position.z - goalPosition.z) == 1 
                && Mathf.Abs(enemyWalkableCell.gameObject.transform.position.x - goalPosition.x) == 0))
        {
            return true;
        }

        return false;
    }
    
}