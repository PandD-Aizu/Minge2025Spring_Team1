using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using PathFinder;

public class Pathfinding : MonoBehaviour
{
    PathGraph graph;
    public List<PathNode> FindPath(EnemyWalkableCell startCell, EnemyGoalPointCell goalCell)
    {
        //初期化
        EnemyWalkableCell currentCell;
        PriorityQueue<string, float> openQueue = new PriorityQueue<string, float>(); //<ID, TotalCost> //現在調査中のノード(ID管理)
        List<string> closedList = new List<string>(); //調査が完了したノード(ID管理)
        
        openQueue.Enqueue(startCell.gameObject.name, 0f);

        while (openQueue.Count > 0)
        {
            //最小のノードのIDを取り出す
            string currentId = openQueue.Dequeue();
            currentCell = graph.GetEnemyWalkableCell(currentId);
            //closedListに移動
            closedList.Add(currentId);
            
            //Goalに隣接していれば終了
            foreach (EnemyWalkableCell connectEnemyWalkableCell in goalCell.ConnectEnemyWalkableCells)
            {
                if (currentCell == connectEnemyWalkableCell)
                {
                    //経路探索を終了してPathリストを返す
                }
            }
            
            OpenNeighborNode(currentId, openQueue, closedList, goalCell);
        }

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
            
            //Totalのコストを計算してみて今までの方がスコアが低ければ
            if (currentNode.UpdateFCost(graph.GetPathNode(parentId), goalCell))
            {
                openQueue.Enqueue(currentNode.NodeId, currentNode.FCost);
            }
        }
    }
}
