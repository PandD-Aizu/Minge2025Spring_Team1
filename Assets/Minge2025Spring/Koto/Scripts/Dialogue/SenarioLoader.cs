using UnityEngine;
using System.IO;

namespace Dialogue
{
    public class ScenarioLoader
    {
        // @brief シナリオ読み込み
        public ScenarioData LoadScenario(string filePath)
        {
            ScenarioData scenarioData = null;
            string path = Path.Combine(Application.streamingAssetsPath, filePath);
            string jsonText = "";
            
            if (File.Exists(path))
            {
                jsonText = File.ReadAllText(path);
                scenarioData = JsonUtility.FromJson<ScenarioData>(jsonText);
                Debug.Log("シナリオをロードしました: " + scenarioData.scenes.Length + " 件");
            }
            else
            {
                Debug.LogError("JSONファイルが見つかりません: " + path);
            }

            return scenarioData;
        }
    }
}