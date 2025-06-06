using UnityEngine;

namespace PathFinder
{
    public interface IBlock
    {
        public string Name { get; }
        public Vector3 Position { get; }
    }
}