using Live2D.Cubism.Framework.Json;
using Static;
using UnityEngine;

public class ReleaseStage : MonoBehaviour
{
    [SerializeField] private StageOpenManager stageOpenManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageOpenManager.stage_num = StageRelease.stage_num;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
