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
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                SceneManager.LoadScene("Stage01");
                break;
            case 2:
                SceneManager.LoadScene("Stage02");
                break;
            case 3:
                SceneManager.LoadScene("Stage03");
                break;
            case 4:
                SceneManager.LoadScene("Stage04");
                break;
            case 5:
                SceneManager.LoadScene("Stage05");
                break;
        }
    }
}
