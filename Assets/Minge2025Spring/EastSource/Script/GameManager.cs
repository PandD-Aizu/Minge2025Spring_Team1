using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public struct SearchShortestRootInfo
    {
        public EnemyWalkableCell enemyWalkableCell;
        public int step;
        public int weight;
    }
    
    private GameObject[] enemyWalkableCells;
    public GameObject[] EnemyWalkableCells {get{return enemyWalkableCells;}}
    
    private void Awake()
    {
        Instance = this;
        UpdateEnemyWalkableCells();
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }
    
    //brief シーン上のエネミーが通れるエリアを取得する
    private void UpdateEnemyWalkableCells()
    {
        enemyWalkableCells = GameObject.FindGameObjectsWithTag(GameTagsManager.EnemyWalkable);
        foreach (GameObject enemyWalkableCell in enemyWalkableCells)
        {
            Debug.Log("position: " + enemyWalkableCell.transform.position + "name: " +enemyWalkableCell.gameObject.name);
        }
    }

    //brief Enemyがゴールにたどり着くまでの最短距離を計算する
    private void SearchShortestRoot(EnemyWalkableCell currentCell, Vector3 goalPosition)
    {
        List<EnemyWalkableCell> passedCells = new List<EnemyWalkableCell>();
        List<SearchShortestRootInfo> TerminalCellsInfos = new List<SearchShortestRootInfo>();　/*現在調べたセルの中で末端のセルのリスト*/
        
        passedCells.Add(currentCell);
        
        //初期のTerminalCellsをつくる
        TerminalCellsInfos.Add(new SearchShortestRootInfo()
        {
            enemyWalkableCell = currentCell,
            step = 0,
            weight = 0,
        });


        while (!CheckadjacentGoal(TerminalCellsInfos, goalPosition))
        {
            TerminalCellsInfos.Sort((a, b) => a.weight.CompareTo(b.weight));
            
            SearchShortestRootInfo currentTerminalCellInfo = TerminalCellsInfos[0];
            TerminalCellsInfos.Remove(TerminalCellsInfos[0]);
            
            //末端のセルの隣接するセルを確認
            if (currentTerminalCellInfo.enemyWalkableCell.UpCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.UpCell))
            {
                currentTerminalCellInfo.enemyWalkableCell.UpCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell; 
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.UpCell)); 
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.UpCell);
            }

            if (currentTerminalCellInfo.enemyWalkableCell.DownCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.DownCell))
            {
                currentTerminalCellInfo.enemyWalkableCell.DownCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell; 
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.DownCell)); 
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.DownCell);
            }

            if (currentTerminalCellInfo.enemyWalkableCell.RightCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.RightCell))
            {
                currentTerminalCellInfo.enemyWalkableCell.RightCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell;
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.RightCell));
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.RightCell);
            }

            if (currentTerminalCellInfo.enemyWalkableCell.LeftCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.LeftCell))
            { 
                currentTerminalCellInfo.enemyWalkableCell.LeftCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell; 
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.LeftCell)); 
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.LeftCell);
            }
            
        }
    }

    //brief 新しい末端セルを作る関数
    private SearchShortestRootInfo CreateTerminalCell(SearchShortestRootInfo currentTerminalCellInfo, EnemyWalkableCell nextCell)
    {
        SearchShortestRootInfo newTerminalCellInfo = new SearchShortestRootInfo()
        {
            enemyWalkableCell = nextCell,
            step = currentTerminalCellInfo.step + 1,
            weight = currentTerminalCellInfo.weight + 1,
        };
        return newTerminalCellInfo;
    }
    
    //brief 末端セルがゴールセルと隣接しているか確認
    private bool CheckadjacentGoal(List<SearchShortestRootInfo> TerminalCellsInfos, Vector3 goalPosition)
    {
        foreach (SearchShortestRootInfo terminalCellsInfo in TerminalCellsInfos)
        {
            if (Mathf.Approximately(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.x - goalPosition.x, 1) 
                || Mathf.Approximately(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.x - goalPosition.x, -1) 
                || Mathf.Approximately(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.z - goalPosition.z, 1) 
                || Mathf.Approximately(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.z - goalPosition.z, -1))
            {
                return true;
            }
        }
        return false;
    }
    
    //過去に検索したセルか確認
    private bool PassedCellsExist(List<EnemyWalkableCell> passedCells, EnemyWalkableCell nextCell)
    {
        foreach (EnemyWalkableCell passedCell in passedCells)
        {
            if (passedCell == nextCell)
            {
                return true;
            }
        }
        return false;
    }
}
