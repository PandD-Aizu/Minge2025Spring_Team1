using UnityEngine;

public class StageOpenManager : MonoBehaviour
{
    public int stage_num;
    public GameObject ButtonTutorial;
    public GameObject ButtonStage1;
    public GameObject ButtonStage2;
    public GameObject ButtonStage3;
    public GameObject ButtonStage4;
    public GameObject ButtonStage5;

    void Start()
    {
        ButtonStage1.SetActive(false);
        ButtonStage2.SetActive(false);
        ButtonStage3.SetActive(false);
        ButtonStage4.SetActive(false);
        ButtonStage5.SetActive(false);
        stage_num = 0;
    }
    
    void Update()
    {
        if (stage_num == 0)
        {
            Debug.Log("Current Stage Number: " + stage_num);
            ButtonTutorial.SetActive(true);
        }
        
        if (stage_num >= 1)
        {
            Debug.Log(stage_num);
            ButtonStage1.SetActive(true);
        }

        if (stage_num >= 2)
        {
            Debug.Log(stage_num);
            ButtonStage2.SetActive(true);
        }

        if (stage_num >= 3)
        {
            Debug.Log(stage_num);
            ButtonStage3.SetActive(true);
        }

        if (stage_num >= 4)
        {
            Debug.Log(stage_num);
            ButtonStage4.SetActive(true);
        }

        if (stage_num >= 5)
        {
            Debug.Log(stage_num);
            ButtonStage5.SetActive(true);
        }
    }
}
