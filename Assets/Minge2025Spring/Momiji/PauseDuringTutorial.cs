using UnityEngine;

public class PauseDuringTutorial : MonoBehaviour
{
    bool isTutorial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
        isTutorial = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void endTutorial()
    {
        isTutorial = false;
    }
}
