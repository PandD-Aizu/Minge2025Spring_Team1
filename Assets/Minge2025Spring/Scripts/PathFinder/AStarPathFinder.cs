
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinder
{
    public class AStarPathFinder
    {
        private readonly Graph graph;

        public AStarPathFinder(Graph graph)
        {
            this.graph = graph;
        }

        // @brief ヒューリスティック関数(マンハッタン距離)
        // @param from 開始ノードの名前, to 終了ノードの名前
        private float Heuristic(string from, string to)
        {
            var blockFrom = graph.GetBlock(from);
            var blockTo = graph.GetBlock(to);
            
            return Math.Abs(Vector3.Distance(blockFrom.Position, blockTo.Position));
        }

        // @brief A*アルゴリズムを使用して最短経路を見つける
        // @param startName 開始ノードの名前, goalName 終了ノードの名前
        // @return 最短経路のリストとそのコスト
        public (List<string> path, float cost) FindPath(string startName, string goalName)
        {
            var startBlock = graph.GetBlock(startName);
            var goalBlock = graph.GetBlock(goalName);

            if (startBlock == null || goalBlock == null)
            {
                return (null, -1);
            }

            var openSet = new PriorityQueue<string, float>();
            var cameFrom = new Dictionary<string, string>();
            var goalCost = new Dictionary<string, float> { [startName] = 0 };
            var heuristicCost = new Dictionary<string, float> { [startName] = Heuristic(startName, goalName) };

            openSet.Enqueue(startName, heuristicCost[startName]);

            while (openSet.Count > 0)
            {
                string current = openSet.Dequeue();

                if (current == goalName)
                    return ReconstructPath(cameFrom, current, goalCost[current]);

                foreach (var edge in graph.GetNeighbors(current))
                {
                    var neighbor = edge.To;
                    var tentativeGoalCost = goalCost[current] + edge.Weight;

                    if (!goalCost.ContainsKey(neighbor) || tentativeGoalCost < goalCost[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        goalCost[neighbor] = tentativeGoalCost;
                        heuristicCost[neighbor] = tentativeGoalCost + Heuristic(neighbor, goalName);

                        openSet.Enqueue(neighbor, heuristicCost[neighbor]);
                    }
                }
            }

            return (null, -1);
        }

        // @brief 最短経路を再構築する
        // @param cameFrom 各ノードの親ノードの辞書, current 現在のノード, cost 現在のコスト
        // @return 最短経路のリストとそのコスト
        private (List<string> path, float cost) ReconstructPath(Dictionary<string, string> cameFrom, string current, float cost)
        {
            var totalPath = new List<string> { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }

            return (totalPath, cost);
        }
    }
}