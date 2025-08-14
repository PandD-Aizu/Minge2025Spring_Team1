using TMPro;
using UnityEngine;

public class StageLifeUIView : MonoBehaviour
{
    private int maxSpawnEnemies;
    private int goalCapasity;
    
    [SerializeField] public TextMeshProUGUI textMesh;
    
    //変数を初期化
    private void Start()
    {
        maxSpawnEnemies = GameManager.Instance.MaxSpawnEnemies;
        goalCapasity = GameManager.Instance.GoalCapasity;
    }
    
    //UIをアップデートする関数
    public void UpdateStageLifeUI( int spawnedEnemies , int reachGoalEnemies)
    {
        textMesh.text = (spawnedEnemies + "\n/\n" + maxSpawnEnemies + "\n\n" + reachGoalEnemies + "\n/\n" + goalCapasity).ToString();
    }
}
