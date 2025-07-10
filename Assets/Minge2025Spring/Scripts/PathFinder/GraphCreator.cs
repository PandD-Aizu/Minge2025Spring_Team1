using System;
using System.Collections.Generic;
using System.Linq;
using FMOD;
using UnityEngine;

namespace PathFinder
{
    public class GraphCreator : MonoBehaviour
    {
        [Header("ここにブロックのGameObjectをドラッグ＆ドロップする")]
        [SerializeField] private List<GameObject> blockGameObjects;
        
        private Graph graph; // グラフのインスタンス
        
        /* プロパティ */
        public Graph CreatedGraph => graph;
        public string StartBlockName { get; private set; }
        public string GoalBlockName { get; private set; }

        private void Awake()
        {
            graph = new Graph();
            
            blockGameObjects.AddRange(GameObject.FindGameObjectsWithTag("START_BLOCK").ToList());
            blockGameObjects.AddRange(GameObject.FindGameObjectsWithTag("NORMAL_BLOCK").ToList());
            blockGameObjects.AddRange(GameObject.FindGameObjectsWithTag("GOAL_BLOCK").ToList());

            if (blockGameObjects == null || blockGameObjects.Count == 0)
                throw new Exception("GraphCreator: blockGameObjectsリストが空またはnullです。");

            // GameObjectからIBlockを作成し、グラフに追加
            foreach (var blockGO in blockGameObjects)
            {
                if (blockGO == null)
                    throw new Exception("GraphCreator: blockGameObjectsリスト内にnullの要素があります。");

                IBlock block;
                var position = blockGO.transform.position;
                var name = blockGO.name;

                switch (blockGO.tag)
                {
                    // スタート地点のブロック
                    case "START_BLOCK":
                        block = new StartBlock(name, position);
                        StartBlockName = name;
                        break;
                    
                    // ゴール地点のブロック
                    case "GOAL_BLOCK":
                        block = new GoalBlock(name, position);
                        GoalBlockName = name;
                        break;
                    
                    // 通常のブロック
                    case "NORMAL_BLOCK":
                        block = new NormalBlock(name, position);
                        break;
                    
                    // それ以外は例外をブン投げる
                    default:
                        throw new Exception($"GraphCreator: GameObject '{name}' には未処理のタグ '{blockGO.tag}' が付いています。");
                        break;
                }
                
                graph.AddBlock(block); // グラフにブロックを追加
            }

            SetupEdges(); // エッジを設定
        }

        // @brief グラフのブロック間のエッジを設定する
        private void SetupEdges()
        {
            if (graph == null)
                return;
            
            List<IBlock> blocksInGraph = new List<IBlock>();
            foreach (var go in blockGameObjects)
            {
                if (go != null)
                {
                    // グラフからブロックを取得
                    var block = graph.GetBlock(go.name);
                    
                    // ブロックがnullでない場合のみリストに追加
                    if(block != null)
                        blocksInGraph.Add(block);
                }
            }

            // ブロックが2つ以上ある場合のみエッジを設定
            if (blocksInGraph.Count < 2)
                return;
            
            for (int i = 0; i < blocksInGraph.Count; i++)
            {
                var blockA = blocksInGraph[i];
                for (int j = i + 1; j < blocksInGraph.Count; j++)
                {
                    var blockB = blocksInGraph[j];

                    // ブロック間の距離を計算
                    float distance = Vector3.Distance(blockA.Position, blockB.Position);
                    
                    // 1.0fの距離でエッジを追加
                    if(Mathf.Approximately(distance, 1.0f))
                        graph.AddEdge(blockA.Name, blockB.Name, 1);
                }
            }
        }
    }
}