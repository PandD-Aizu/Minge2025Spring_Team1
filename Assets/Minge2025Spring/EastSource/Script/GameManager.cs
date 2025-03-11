using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public struct SearchShortestRootInfo
    {
        public EnemyWalkableCell enemyWalkableCell;
        public int step;
        public int weight;
    }
    
    [FormerlySerializedAs("debugMode")]
    [Header("Debugger Mode")]
    [SerializeField] private bool isdebugMode = false;
    [SerializeField] private int testWalkableCellNumber = 0;
    
    private GameObject[] enemyWalkableCells;
    private GameObject[] enemyGoalPointCells;
    public GameObject[] EnemyWalkableCells {get{return enemyWalkableCells;}}
    
    private void Awake()
    {
        Instance = this;
        UpdateEnemyWalkableCells();
        UpdateEnemyGoalPointCells();
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private void Update()
    {
        if (testWalkableCellNumber >= enemyWalkableCells.Length)
        {
            isdebugMode = false;
            testWalkableCellNumber = 0;
            Debug.Log("testWalkableCellNumber overflow");
        }

        if (Input.GetKeyDown(KeyCode.R) && isdebugMode)
        {
            foreach (GameObject enemyWalkableCell in enemyWalkableCells)
            {
                Vector3 temp = enemyWalkableCell.transform.position;
                temp.y = 0;
                enemyWalkableCell.transform.position = temp;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isdebugMode)
        {
            Debug.Log("get Key space");
            SearchShortestRoot(enemyWalkableCells[testWalkableCellNumber].gameObject.GetComponent<EnemyWalkableCell>(), enemyGoalPointCells[1].transform.position);
            Debug.Log("finish");

            EnemyWalkableCell currentEnemyWalkableCell = enemyWalkableCells[testWalkableCellNumber].GetComponent<EnemyWalkableCell>();
            int number = 0;
            while (true)
            {
                DebugUp1PositionY(currentEnemyWalkableCell);
                if (currentEnemyWalkableCell.NextCell == null)
                {
                    break;
                }
                
                if (currentEnemyWalkableCell.NextCell == currentEnemyWalkableCell)  // 自己ループを防ぐ
                {
                    Debug.LogError("NextCell is pointing to itself! Breaking loop.");
                    break;
                }
                
                currentEnemyWalkableCell = currentEnemyWalkableCell.NextCell;
                number++;
                if (number > 100)
                {
                    Debug.LogWarning("number overflow");
                    break;
                }
            }
        }
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

    private void UpdateEnemyGoalPointCells()
    {
        enemyGoalPointCells = GameObject.FindGameObjectsWithTag(GameTagsManager.EnemyGoalPoint);
        foreach (GameObject enemyGoalPointCell in enemyGoalPointCells)
        {
            Debug.Log("position: " + enemyGoalPointCell.transform.position + "name: " +enemyGoalPointCell.gameObject.name);
        }
    }

    //brief Enemyがゴールにたどり着くまでの最短距離を計算する
    private void SearchShortestRoot(EnemyWalkableCell currentCell, Vector3 goalPosition)
    {
        UpdateEnemyWalkableCells();
        UpdateEnemyGoalPointCells();
        ResetCell();
        List<EnemyWalkableCell> passedCells = new List<EnemyWalkableCell>();
        List<SearchShortestRootInfo> TerminalCellsInfos = new List<SearchShortestRootInfo>(); /*現在調べたセルの中で末端のセルのリスト*/
        EnemyWalkableCell connectGoalCell = null;
        passedCells.Add(currentCell);
        
        //初期のTerminalCellsをつくる
        TerminalCellsInfos.Add(new SearchShortestRootInfo()
        {
            enemyWalkableCell = currentCell,
            step = 0,
            weight = 0,
        });


        int maxSteps = 1000; // 無限ループ防止
        int stepCount = 0;
        while (connectGoalCell == null)
        {
            TerminalCellsInfos.Sort((a, b) => a.weight.CompareTo(b.weight));
            SearchShortestRootInfo currentTerminalCellInfo = TerminalCellsInfos[0];
            TerminalCellsInfos.Remove(TerminalCellsInfos[0]);
            
            //末端のセルの隣接するセルを確認TerminalCellsに格納
            if (currentTerminalCellInfo.enemyWalkableCell.UpCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.UpCell)
                && currentTerminalCellInfo.enemyWalkableCell.UpCell.isWalkable)
            {
                currentTerminalCellInfo.enemyWalkableCell.UpCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell; 
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.UpCell)); 
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.UpCell);
            }

            if (currentTerminalCellInfo.enemyWalkableCell.DownCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.DownCell)
                && currentTerminalCellInfo.enemyWalkableCell.DownCell.isWalkable)
            {
                currentTerminalCellInfo.enemyWalkableCell.DownCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell; 
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.DownCell)); 
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.DownCell);
            }

            if (currentTerminalCellInfo.enemyWalkableCell.RightCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.RightCell)
                && currentTerminalCellInfo.enemyWalkableCell.RightCell.isWalkable)
            {
                currentTerminalCellInfo.enemyWalkableCell.RightCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell;
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.RightCell));
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.RightCell);
            }

            if (currentTerminalCellInfo.enemyWalkableCell.LeftCell != null
                && !PassedCellsExist(passedCells, currentTerminalCellInfo.enemyWalkableCell.LeftCell)
                && currentTerminalCellInfo.enemyWalkableCell.LeftCell.isWalkable)
            { 
                currentTerminalCellInfo.enemyWalkableCell.LeftCell.PreviousCell = currentTerminalCellInfo.enemyWalkableCell; 
                TerminalCellsInfos.Add(CreateTerminalCell(currentTerminalCellInfo, currentTerminalCellInfo.enemyWalkableCell.LeftCell)); 
                passedCells.Add(currentTerminalCellInfo.enemyWalkableCell.LeftCell);
            }
            
            if (TerminalCellsInfos.Count == 0) // 追加の無限ループ防止策
            {
                Debug.LogError("No more terminal cells! Breaking loop.");
                break;
            }
            
            connectGoalCell = CheckadjacentGoal(TerminalCellsInfos, goalPosition);
            
            stepCount++;
            if (stepCount > maxSteps)
            {
                Debug.LogError("SearchShortestRoot exceeded max steps! Breaking loop.");
                break;
            }
            
            Debug.Log(CheckadjacentGoal(TerminalCellsInfos, goalPosition));
            
        }
        
        //connectGoalCellの名前をコーンソールに表示するだけなので無視でおけ
        if (connectGoalCell != null)
        {
            Debug.Log(connectGoalCell.gameObject.name);
        }

        if (connectGoalCell == null)
        {
            Debug.Log("Cannot discover root to goal.");
        }
        else
        {
            ToGoalfromStart(connectGoalCell);
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
    private EnemyWalkableCell CheckadjacentGoal(List<SearchShortestRootInfo> TerminalCellsInfos, Vector3 goalPosition)
    {
        foreach (SearchShortestRootInfo terminalCellsInfo in TerminalCellsInfos)
        {
            if ((Mathf.Abs(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.x - goalPosition.x) == 1 
                 && Mathf.Abs(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.z - goalPosition.z) == 0)
                || (Mathf.Abs(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.z - goalPosition.z) == 1 
                    && Mathf.Abs(terminalCellsInfo.enemyWalkableCell.gameObject.transform.position.x - goalPosition.x) == 0))
            {
                return terminalCellsInfo.enemyWalkableCell;
            }
        }
        return null;
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

    //ルート内のセルのNextCellに適当なセルを格納するセル
    private void ToGoalfromStart(EnemyWalkableCell connectGoalCell)
    {
        EnemyWalkableCell currentConnectGoalCell = connectGoalCell;
        int number = 0;
        while (true)
        {
            if (currentConnectGoalCell.PreviousCell == null)
            {
                break;
            }
            currentConnectGoalCell.PreviousCell.NextCell = currentConnectGoalCell;
            currentConnectGoalCell = currentConnectGoalCell.PreviousCell;
            number++;
        }
        
        Debug.Log("finish connecting to goal from current cell :" + number);
    }

    private void ResetCell()
    {
        foreach (GameObject enemyWalkableCellObject in enemyWalkableCells)
        {
            if (enemyWalkableCellObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCell))
            {
                enemyWalkableCell.NextCell = null;
                enemyWalkableCell.PreviousCell = null;
            }
        }
    }

    public Vector3 CallRandomGoalPosition()
    {
        Random random = new Random();
        return enemyGoalPointCells[random.Next(enemyWalkableCells.Length)].transform.position;
    }

    private void DebugUp1PositionY(EnemyWalkableCell enemyWalkableCell)
    {
        if (isdebugMode)
        {
            Vector3 temp = enemyWalkableCell.gameObject.transform.position;
            temp.y += 1f;
            enemyWalkableCell.gameObject.transform.position = temp;
        }
    }
}