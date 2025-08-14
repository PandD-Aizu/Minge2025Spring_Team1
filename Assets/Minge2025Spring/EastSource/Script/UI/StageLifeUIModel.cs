using System;
using UnityEngine;
using UniRx;

public class StageLifeUIModel : MonoBehaviour
{
    public ReactiveProperty<int> reachGoalEnemies = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> spawnedEnemeies = new ReactiveProperty<int>(0);

    //オブジェクトが破壊されたとき購読解除
    private void OnDestroy()
    {
        reachGoalEnemies.Dispose();
        spawnedEnemeies.Dispose();
    }
}
