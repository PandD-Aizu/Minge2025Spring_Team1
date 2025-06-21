using System;
using System.Collections.Generic;
using UnityEngine;

public class PathGraph
{
    Dictionary<string, PathNode> nodeDict = new Dictionary<string, PathNode>();
    Dictionary<string, List<PathEdge>> edgeDict = new Dictionary<string, List<PathEdge>>();
    
    //コンストラクタ
    public PathGraph()
    {
        Debug.LogWarning("CreateGraph");
        CreateGraph();
    }

    //Graphを更新できる関数
    private void CreateGraph()
    {
        foreach (GameObject enemyWalkableCell in GameManager.Instance.EnemyWalkableCells)
        {
            if (enemyWalkableCell.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell currentCell))
            {
                if (currentCell == null)
                {
                    continue;
                }
                nodeDict.Add(currentCell.gameObject.name, new PathNode(currentCell.gameObject.name, currentCell));//Dictionaryに登録する
                edgeDict.Add(currentCell.gameObject.name, CreateEdgeList(currentCell));
                Debug.Log(currentCell.gameObject.name);
            }
        } 
    }

    //edgeを新しく作る関数
    private List<PathEdge> CreateEdgeList(EnemyWalkableCell currentCell)
    {
        List<PathEdge> edgeList = new List<PathEdge>(4);
        foreach (EnemyWalkableCell aroundCell in currentCell.AroundCells)
        {
            float weight = Vector3.Distance(aroundCell.gameObject.transform.position, currentCell.gameObject.transform.position);
            edgeList.Add(new PathEdge(currentCell.gameObject.name, weight, aroundCell.gameObject.name));
        }
        return edgeList;
    }
    
    //引数のIdのノードのみを返す
    public PathNode GetPathNode(string deictionryId)
    {
        return nodeDict[deictionryId];
    }
    
    //引数のIdのEdgeListのみを返す
    public List<PathEdge> GetPathEdge(string deictionryId)
    {
        return edgeDict[deictionryId];
    }

    //引数のIdのEnemyWalkableCellを返す
    public EnemyWalkableCell GetEnemyWalkableCell(string deictionryId)
    {
        return nodeDict[deictionryId].EnemyWalkableCell;
    }

    //このGraphのNodeDictを返す
    public Dictionary<string, PathNode> GetNodeDict()
    {
        return nodeDict;
    }
 }
