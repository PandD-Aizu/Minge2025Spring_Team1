using UnityEngine;

public class PathNode
{
    public PathNode parentNode;

    private string nodeId;
    private float gCost;
    private float hCost;
    private float fCost;
    
    private Vector3 surfacePosition;
    private BaseEnemyWalkable baseEnemyWalkable;

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

    public Vector3 SurfacePosition
    {
        get => surfacePosition;
    }

    public BaseEnemyWalkable BaseEnemyWalkable
    {
        get => baseEnemyWalkable;
    }

    //コンストラクタ
    //引数　nodeId: EnemyWalkableCellのオブジェクト名, position: EnemyWalkableCellのposition, Node: 親のnode
    public PathNode(string nodeId, BaseEnemyWalkable baseEnemyWalkable)
    {
        this.baseEnemyWalkable = baseEnemyWalkable;
        this.nodeId = nodeId;
        Vector3 tempPos = baseEnemyWalkable.gameObject.transform.position;
        Vector3 tempScale = baseEnemyWalkable.gameObject.transform.localScale;
        tempPos.y += tempScale.y / 2;
        this.surfacePosition = tempPos;
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

    //ヒューリスティックコストを計算しhCostを更新する
    public void CalcHuricCost(EnemyGoalPointCell goalCell)
    {
        hCost = Vector3.Distance(goalCell.gameObject.transform.position, SurfacePosition);
    }

    //新しい経路のCostと古いコストを計算し適切に更新する
    //新しい経路のコストのほうが小さければＴｒｕｅ、古いコストのほうが小さければＦａｌｓｅを返す
    public bool TryUpdateGCost(PathNode parentNode)
    {
        //トータル移動コストが既存ノード以上なら差し替え不要
        float totalCost = Vector3.Distance(SurfacePosition, parentNode.SurfacePosition) + parentNode.GCost;
        if (GCost == 0 || totalCost < GCost)//トータルコストが既存コスト以下の時
        {
            gCost = totalCost;  
            this.parentNode = parentNode;
            return true;
        }

        return false;
    }
}


