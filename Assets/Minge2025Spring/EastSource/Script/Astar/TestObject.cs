using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    Pathfinding pathfinding;
    public EnemyWalkableCell startCell;
    public EnemyGoalPointCell goalCell;

    private void Start()
    {
        List<PathNode> 
        pathfinding.FindPath(startCell, goalCell);
    }
}
