using System;
using UnityEngine;

namespace PathFinder
{
    public class PathFinderEntryPoint : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private GraphCreator graphCreator;

        private Graph graph;
        private AStarPathFinder pathFinder;
        private string startBlockName;
        private string goalBlockName;

        private void Start()
        {
            if (graphCreator == null)
                throw new Exception("PathFinderEntryPoint: GraphCreator が設定されていません。");

            graph = graphCreator.CreatedGraph;
            startBlockName = graphCreator.StartBlockName;
            goalBlockName = graphCreator.GoalBlockName;

            if (graph == null)
                throw new Exception("PathFinderEntryPoint: GraphCreator からグラフを取得できませんでした。");

            if (string.IsNullOrEmpty(startBlockName))
                throw new Exception("PathFinderEntryPoint: スタートノード名が GraphCreator から取得できませんでした。");

            if (string.IsNullOrEmpty(goalBlockName))
                throw new Exception("PathFinderEntryPoint: ゴールノード名が GraphCreator から取得できませんでした。");

            pathFinder = new AStarPathFinder(graph);

            // @DEBUG
            FindAndDisplayPath();
        }

        private void FindAndDisplayPath()
        {
            if (pathFinder == null)
                return;
            
            Debug.Log($"'{startBlockName}' から '{goalBlockName}' への経路を探索します。");
            var (pathResult, cost) = pathFinder.FindPath(startBlockName, goalBlockName);

            if (pathResult != null && pathResult.Count > 0)
            {
                Debug.Log($"経路が見つかりました。コスト: {cost}");
                Debug.Log("経路: " + string.Join(" -> ", pathResult));
            }
            else
            {
                Debug.LogWarning($"'{startBlockName}' から '{goalBlockName}' への経路が見つかりませんでした。");
            }
        }
    }
}