using UnityEngine;

public interface IEnemyMovement
{
    protected internal void UpdateRootPath();
    protected internal void EnemyMove();
    protected internal void StopEnemyMovement();
}
