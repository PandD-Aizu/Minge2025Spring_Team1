using UnityEngine;

public class PathNode : MonoBehaviour
{
    public string parentNodeId;

    private string nodeId;
    private float gCost;
    private float hCost;
    private float fCost;
    private EnemyWalkableCell enemyWalkableCell;

    //アクセサ
    public string NodeId
    {
        get => nodeId;
    }

    public float GCost
    {
        get => gCost;
    }

    public float FCost
    {
        get => fCost;
    }

    public Vector3 Position
    {
        get => enemyWalkableCell.gameObject.transform.position;
    }

    public EnemyWalkableCell EnemyWalkableCell
    {
        get => enemyWalkableCell;
    }

    //コンストラクタ
    //引数　nodeId: EnemyWalkableCellのオブジェクト名, position: EnemyWalkableCellのposition, Node: 親のnode
    public PathNode(string nodeId, EnemyWalkableCell enemyWalkableCell)
    {
        this.enemyWalkableCell = enemyWalkableCell;
        this.nodeId = nodeId;
    }
    
    //引数: 親ノード
    public bool UpdateFCost(PathNode parentNode, EnemyGoalPointCell goalCell)
    {
        if (TryUpdateGCost(parentNode))
        {
            CalcHuricCost(goalCell);
            fCost = gCost + hCost;
            return true; //トータルコストが既存以下の時
        }

        return false; //トータルコストが既存のコスト以上の時
    }

    public void CalcHuricCost(EnemyGoalPointCell goalCell)
    {
        hCost = Vector3.Distance(goalCell.gameObject.transform.position, Position);
    }

    public bool TryUpdateGCost(PathNode parentNode)
    {
        //トータル移動コストが既存ノード以上なら差し替え不要
        float totalCost = Vector3.Distance(Position, parentNode.Position) + parentNode.GCost;
        if (GCost == 0 && totalCost < GCost)//トータルコストが既存コスト以下の時
        {
            gCost = totalCost;  
            return true;
        }

        return false;
    }
}


