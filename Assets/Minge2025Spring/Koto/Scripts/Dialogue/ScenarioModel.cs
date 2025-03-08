using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "ScenarioModel", menuName = "ScriptableObject/ScenarioModel")]
    public class ScenarioModel : ScriptableObject
    {
        [Header("シナリオファイルへのパス")] 
        [SerializeField] private string[] filePath;
        [SerializeField] private int currentFilePathIndex;
        
        [Header("シナリオデータ")]
        [SerializeField] private ScenarioData scenarioData;
        [SerializeField] private int currentSceneIndex;

        /* getter と setter */
        public string[] FilePath => filePath;
        public int CurrentFilePathIndex { get => currentFilePathIndex; set => currentFilePathIndex = value; }
        public ScenarioData ScenarioData { get => scenarioData; set => scenarioData = value; }
        public int CurrentSceneIndex { get => currentSceneIndex; set => currentSceneIndex = value; }

        public void Init()
        {
            currentSceneIndex = 0;
            currentFilePathIndex = 0;
        }
    }
}