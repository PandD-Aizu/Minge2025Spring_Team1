using UnityEngine;

public class PathNode : MonoBehaviour
{
    private string nodeId;
    private float gCost;
    private float hCost;
    private float fCost;
    private EnemyWalkableCell enemyWalkableCell;
    
    //アクセサ
    public string NodeId{get => nodeId;}
    public Vector3 Position{get => enemyWalkableCell.gameObject.transform.position;}
    public EnemyWalkableCell EnemyWalkableCell {get => enemyWalkableCell;}
    
    //コンストラクタ
    //引数　nodeId: EnemyWalkableCellのオブジェクト名, position: EnemyWalkableCellのposition, Node: 親のnode
    public PathNode(string nodeId, EnemyWalkableCell enemyWalkableCell)
    {
        this.enemyWalkableCell = enemyWalkableCell; 
        this.nodeId = nodeId;
    }

    public float GetTotalCost(GoalPositionCell goalCell)
    {
        CalcHuricCost(goalCell);
        fCost = gCost + hCost;
        return fCost;
    }

    public void CalcHuricCost(GoalPositionCell goalCell)
    {
        hCost = Vector3.Distance(goalCell.gameObject.transform.position, Position);
    }
}
