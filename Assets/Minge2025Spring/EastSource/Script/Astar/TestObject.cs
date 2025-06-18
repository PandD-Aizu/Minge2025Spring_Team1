using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [SerializeField]private Pathfinding pathfinding;
    public EnemyWalkableCell startCell;
    public EnemyGoalPointCell goalCell;

    public void StartPath()
    {
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
