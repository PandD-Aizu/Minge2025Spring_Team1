using System;
using System.Collections.Generic;
using UnityEngine;

public class PathGraph : MonoBehaviour
{
    Dictionary<string, PathNode> nodeDict = new Dictionary<string, PathNode>();
    Dictionary<string, List<PathEdge>> edgeDict = new Dictionary<string, List<PathEdge>>();
    
    //コンストラクタ
    public void Start()
    {
        CreateGraph();
    }

    private void CreateGraph()
    {
        foreach (GameObject enemyWalkableCell in GameManager.Instance.EnemyWalkableCells)
        {
            if (TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell currentCell))
            {
                nodeDict.Add(currentCell.gameObject.name, new PathNode(currentCell.gameObject.name, currentCell));
                edgeDict.Add(currentCell.gameObject.name, CreateEdgeList(currentCell));
            }
        }
    }

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

    public void Add(string dictionaryId, PathNode node, List<PathEdge> edge)
    {
        nodeDict.Add(dictionaryId, node);
        edgeDict.Add(dictionaryId, edge);
    }

    public bool TryGetGraph(string dictionaryId, out PathNode node, out List<PathEdge> edge)
    {
        node = nodeDict[dictionaryId];
        edge = edgeDict[dictionaryId];
        if (node == null && edge == null)
        {
            return false;
        }
        return true;
    }
    
    public PathNode GetPathNode(string deictionryId)
    {
        return nodeDict[deictionryId];
    }

    public List<PathEdge> GetPathEdge(string deictionryId)
    {
        return edgeDict[deictionryId];
    }

    public EnemyWalkableCell GetEnemyWalkableCell(string deictionryId)
    {
        return nodeDict[deictionryId].EnemyWalkableCell;
    }

    public void Remove(string dictionaryId)
    {
        nodeDict.Remove(dictionaryId);
        edgeDict.Remove(dictionaryId);
    }

    public void Clear()
    {
        nodeDict.Clear();
        edgeDict.Clear();
    }

    public bool Equel(string deictionryId, Vector3 goalPosition)
    {
        if (nodeDict[deictionryId].Position == goalPosition)
        {
            return true;
        }
        return false;
    }
 }
