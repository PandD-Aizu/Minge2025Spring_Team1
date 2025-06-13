using UnityEngine;

public class PathEdge : MonoBehaviour
{
    private float weight;
    private string destinationId;
    private string childNodeId;
    
    //アクセサ
    public float Weight{get => weight;}
    public string DestinationId{get => destinationId;}
    public string ChildNode{get => childNodeId;}

    //コンストラクタ
    //destinationId: EnemyWalkableCellのオブジェクト名, position: EnemyWalkableCellのposition, gCost: 今までに掛かってきたコスト
    public PathEdge(string destinationId, float weight, string childNodeId)
    {
        this.destinationId = destinationId;
        this.weight = weight;
        this.childNodeId = childNodeId;
    }
}
