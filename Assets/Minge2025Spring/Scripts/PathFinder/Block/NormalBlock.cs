using UnityEngine;

namespace PathFinder
{
    public class NormalBlock : IBlock
    {
        private string name;
        private Vector3 position;
        
        /* プロパティ */
        public string Name => name;
        public Vector3 Position => position;

        public NormalBlock(string name, Vector3 position)
        {
            this.name = name;
            this.position = position;
        }
    }
}