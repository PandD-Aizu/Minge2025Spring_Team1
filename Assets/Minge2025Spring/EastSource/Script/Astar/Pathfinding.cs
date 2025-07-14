using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using PathFinder;
using Unity.VisualScripting;

public class Pathfinding
{ 
    private PathGraph graph;
    
    //PathListを計算する関数
    //引数: StartCell: 経路探索を開始するときのRootになるCell　goalCell:最終目的地
    public List<PathNode> FindPath(BaseEnemyWalkable startCell, EnemyGoalPointCell goalCell)
    {
        graph = new PathGraph();
        //初期化
        BaseEnemyWalkable currentCell;
        PriorityQueue<string, float> openQueue = new PriorityQueue<string, float>(); //<ID, TotalCost> //現在調査中のノード(ID管理)
        List<string> closedList = new List<string>(); //調査し終えたノード(ID管理)
        
        openQueue.Enqueue(startCell.gameObject.name, Math.Abs(Vector3.Distance(startCell.gameObject.transform.position, goalCell.gameObject.transform.position)));

        //ここからがAstar本体
        while (openQueue.Count > 0)
        {
            //最小のノードのIDを取り出す
            string currentId = openQueue.Dequeue();
            Debug.Log("FromOpenQueue" + currentId);
            currentCell = graph.GetEnemyWalkableCell(currentId);
            //closedListに移動
            closedList.Add(currentId);
            
            //Goalに隣接していれば終了
            foreach (BaseEnemyWalkable connectEnemyWalkableCell in goalCell.ConnectEnemyWalkableCells)
            {
                if (currentCell == connectEnemyWalkableCell)
                {
                    foreach (var pathNodeValue in graph.GetNodeDict().Values)
                    {
                        if (pathNodeValue.parentNode != null)
                        {
                            Debug.Log(pathNodeValue.NodeId + "`aparentNode is" + pathNodeValue.parentNode.NodeId);
                        }
                    }
                    
                    // 経路探索を終了してPathリストを返す
                    return ResultPath(graph.GetPathNode(currentId));
                }
            }
            
            //Goalに隣接していなければ周囲のノードを調べてOpenQueueに追加
            OpenNeighborNode(currentId, openQueue, closedList, goalCell);
        }
        Debug.Log("Stupid");
        return null;
    }
    
    //隣接しているノードを探す
    private void OpenNeighborNode(string parentId, PriorityQueue<string, float> openQueue, List<string> closedList, EnemyGoalPointCell goalCell)
    {
        List<PathEdge> currentEdgeList = graph.GetPathEdge(parentId);
        foreach (PathEdge currentEdge in currentEdgeList)
        {
            //隣接しているノードを取り出す
            PathNode currentNode = graph.GetPathNode(currentEdge.ChildNodeId);

            if (closedList.Contains(currentNode.NodeId))
            {
                continue;
            }
            
            //Totalのコストを計算してみて今までの方がスコアが低ければ
            if (currentNode.UpdateFCost(graph.GetPathNode(parentId), goalCell))
            {
                openQueue.Enqueue(currentNode.NodeId, currentNode.FCost);
            }
        }
    }

    //親ノードをさかのぼる
    //経路のListを返す
    private List<PathNode> ResultPath(PathNode goalNode)
    {
        List<PathNode> result = new List<PathNode>();
        PathNode currentNode = goalNode;
        while (currentNode.parentNode != null)
        {
            result.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        result.Reverse();
        return result;
    }
    
    //Debug
}
