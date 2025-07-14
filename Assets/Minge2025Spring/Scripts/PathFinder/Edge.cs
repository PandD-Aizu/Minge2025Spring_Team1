namespace PathFinder
{
    public class Edge
    {
        private string to;  // 辺の行先
        private int weight; // 辺の重み
        
        /* プロパティ */
        public string To => to;
        public int Weight => weight;

        public Edge(string to, int weight)
        {
            this.to = to;
            this.weight = weight;
        }
    }
}