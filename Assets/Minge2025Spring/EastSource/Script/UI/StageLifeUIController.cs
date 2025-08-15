using CharacterDeploySys;
using UnityEngine;
using UniRx;

public class StageLifeUIController : MonoBehaviour
{
    public static StageLifeUIController Instance{get; private set;}
    
    [SerializeField] private StageLifeUIModel model;
    [SerializeField] private StageLifeUIView view;
    
    //アクセサ
    public int ReachGoalEnemies{get => model.reachGoalEnemies.Value;}
    public int SpawnedEnemies{get => model.spawnedEnemeies.Value;}
    
    private void Awake()
    {
        Instance = this;
    }
    
    //モデルのReactivePropertyを購読
    private void Start()
    {
        model.reachGoalEnemies.Subscribe(x => view.UpdateStageLifeUI(model.spawnedEnemeies.Value, x)).AddTo(this);
        model.spawnedEnemeies.Subscribe(x => view.UpdateStageLifeUI(x, model.reachGoalEnemies.Value)).AddTo(this);
    }

    //reachGoalEnemiesを更新
    public void UpdateReachGoalEnemies(int value)
    {
        model.reachGoalEnemies.Value += value;
    }
    
    //spawnedEnemeiesを更新
    public void UpdateSpawnedEnemies(int value)
    {
        model.spawnedEnemeies.Value += value;
    }
}
