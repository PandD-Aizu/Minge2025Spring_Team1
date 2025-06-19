using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public EnemyWalkableCell startCell;
    public EnemyGoalPointCell goalCell;

    public void Start()
    {
        Pathfinding pathfinding = new Pathfinding();
        Debug.Log("StartPath");
        // Debug.Log("maybe: return null" + pathfinding.FindPath(startCell, goalCell));
        List<PathNode> RootPathes = pathfinding.FindPath(startCell, goalCell);
        int Counter = 0;
        foreach (PathNode node in RootPathes)
        {
            Debug.Log("Root " + Counter + ": " + node.NodeId);
            Counter++;
        }
    }
}
