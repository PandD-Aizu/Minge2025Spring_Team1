using TMPro;
using UnityEngine;

public class StageLifeUI : MonoBehaviour
{
    public static StageLifeUI Instance{get; private set;}
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateStageLifeUI(int maxSpawnEnemies, int spawnedEnemies, int reachGoalEnemies, int goalCapacity)
    {
        textMesh.text = (spawnedEnemies + "\n/\n" + maxSpawnEnemies + "\n\n" + reachGoalEnemies + "\n/\n" + goalCapacity).ToString();
    }
}
