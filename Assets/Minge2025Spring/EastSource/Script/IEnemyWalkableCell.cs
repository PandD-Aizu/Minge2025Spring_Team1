using System.Collections.Generic;
using UnityEngine;


public abstract class IEnemyWalkableCell : MonoBehaviour
{
    public List<EnemyWalkableCell> AroundCells { get; }
    
    public abstract void Initialize();//Awake関数で実行する
    public abstract void InitSurroundingCells();
    public abstract bool TryGetNeighborEnemyWalkableCell(RaycastHit raycastHit, out EnemyWalkableCell resultEnemyWalkableCell);
}