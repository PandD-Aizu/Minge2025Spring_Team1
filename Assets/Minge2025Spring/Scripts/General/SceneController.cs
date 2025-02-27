using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    // SceneManagerのラッパークラス
    public static class SceneController
    {
        // @brief シーンを同期で読み込む
        // @param sceneName シーン名
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        // @brief シーンを非同期で読み込む
        // @param sceneName シーン名
        public static void LoadSceneAsync(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
        
        // @brief シーンを同期でアンロードする
        // @param sceneName シーン名
        public static void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        
        // @brief シーンを非同期でアンロードする
        // @param sceneName シーン名
        public static void UnloadSceneAsync(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}