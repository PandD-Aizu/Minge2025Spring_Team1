using UnityEngine;

public class PathEdge
{
    private float weight;
    private string destinationId;
    private string _childNodeIdId;
    
    //アクセサ
    public float Weight{get => weight;}
    public string DestinationId{get => destinationId;}
    public string ChildNodeId{get => _childNodeIdId;}

    //コンストラクタ
    //destinationId: EnemyWalkableCellのオブジェクト名, weight: 子と親をノーマライズした値, childNodeIdId: 子ノードのEnemyWalkableCellのオブジェクト名
    public PathEdge(string destinationId, float weight, string childNodeIdId)
    {
        this.destinationId = destinationId;
        this.weight = weight;
        this._childNodeIdId = childNodeIdId;
    }
}
