using UnityEngine;

using UnityEngine.SceneManagement;

    public class StageSelectManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageSelect(int stage)
    {
        switch (stage)
        {
            case 0:
                SceneManager.LoadSceneAsync("Tutorial");
                break;
            case 1:
                SceneManager.LoadSceneAsync("BeforeStage01");
                break;
            case 2:
                SceneManager.LoadScene("BeforeStage02");
                break;
            case 3:
                SceneManager.LoadScene("BeforeStage03");
                break;
            case 4:
                SceneManager.LoadScene("BeforeStage04");
                break;
            case 5:
                SceneManager.LoadScene("BeforeStage05");
                break;
        }
    }
}
