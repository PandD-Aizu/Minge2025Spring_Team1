using System.Collections.Generic;
using FMOD;
using Unity.Collections;

namespace PathFinder
{
    public class Graph
    {
        // グラフの頂点
        private readonly Dictionary<string, IBlock> blocks;
        // グラフの隣接リスト
        private readonly Dictionary<string, List<Edge>> adjancyList;

        public IBlock GetBlock(string name) => blocks.GetValueOrDefault(name);
        public List<Edge> GetNeighbors(string name) => adjancyList.GetValueOrDefault(name);

        public Graph()
        {
            blocks = new Dictionary<string, IBlock>();
            adjancyList = new Dictionary<string, List<Edge>>();
        }

        // @brief ブロックをグラフに追加
        // @param block 追加するブロック
        public void AddBlock(IBlock block)
        {
            if (!blocks.ContainsKey(block.Name))
            {
                blocks[block.Name] = block;
                adjancyList[block.Name] = new List<Edge>();
            }
        }

        // @brief 辺を隣接リストに追加
        // @param from 始点のブロック名, to 終点のブロック名, weight 辺の重み
        public void AddEdge(string from, string to, int weight)
        {
            if (blocks.ContainsKey(from) && blocks.ContainsKey(to))
            {
                adjancyList[from].Add(new Edge(to, weight));
                adjancyList[to].Add(new Edge(from, weight));
            }
        }
    }
}