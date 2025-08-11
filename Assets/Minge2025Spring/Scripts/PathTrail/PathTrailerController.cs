using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PathTrail
{
    public class PathTrailerController : MonoBehaviour
    {
        [Header("経路上を走らせるオブジェクト")]
        [SerializeField] private GameObject pathTrailParticle;

        [Header("スタートとゴールの位置")] 
        [SerializeField] private List<BaseEnemyWalkable> startCells;
        [SerializeField] private List<EnemyGoalPointCell> goalCells;

        private Pathfinding pathfinder;
        private List<List<PathNode>> shortestPath = new ();

        private void Start()
        {
            pathfinder = new Pathfinding();
            for (int i = 0; i < startCells.Count; i++)
            {
                shortestPath.Add(pathfinder.FindPath(startCells[i], goalCells[i]));
            }
            
            // 各径路ごとにパーティクルを生成し、経路上を走らせる
            for (int i = 0; i < shortestPath.Count; i++)
            {
                var currentPath = shortestPath[i];
                if (currentPath == null || currentPath.Count == 0) continue;

                // PathNodeのリストをVector3の配列に変換
                var waypoints = new Vector3[currentPath.Count + 1];
                for (int j = 0; j < currentPath.Count; j++)
                {
                    // 地面との差分を考慮してY座標を調整
                    var pos = currentPath[j].SurfacePosition;
                    pos.y += 1.0f; 
                    waypoints[j] = pos;
                }
                
                // 最終的なゴール地点を追加
                var goalPos = goalCells[i].transform.position;
                goalPos.y += 1.0f;
                waypoints[currentPath.Count] = goalPos;

                // パーティクルを生成し、開始位置に配置
                GameObject pathTrail = Instantiate(pathTrailParticle, waypoints[0], Quaternion.identity);
                pathTrail.SetActive(true);

                // 経路に沿って移動するアニメーションを設定
                // 1セグメントあたり0.2秒で移動し、ループさせる
                pathTrail.transform.DOPath(waypoints, 0.2f * waypoints.Length, PathType.Linear)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => pathTrail.transform.GetComponent<ParticleSystem>().Stop());
            }
        }
    }
}
