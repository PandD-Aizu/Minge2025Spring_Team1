using UnityEngine;

namespace PathFinder
{
    public class StartBlock : IBlock
    {
        private string name;
        private Vector3 position;
        
        /* プロパティ */
        public string Name => name;
        public Vector3 Position => position;

        public StartBlock(string name, Vector3 position)
        {
            this.name = name;
            this.position = position;
        }
    }
}