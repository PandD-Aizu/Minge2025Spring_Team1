using UnityEngine;

public class BackToTitle : MonoBehaviour
{
    public void OnClickBackToTitle()
    {
        // タイトルシーンに戻る処理を実行
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
