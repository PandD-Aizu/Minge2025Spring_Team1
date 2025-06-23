using UnityEngine;
using UnityEngine.UI;

public class PuzzleTimer : MonoBehaviour
{
    [Header("(Debug用)タイマーを有効にするかどうか")]
    [SerializeField] private bool useTimer = true;

    [Header("TimerTextオブジェクト")]
    [SerializeField] private Text TimerText;

    [Header("制限時間(s)")]
    [SerializeField] private float LimitTime;

    private bool isCompleted = false;
    private PhaseManager phaseManager;

    private void Start()
    {
        if (!useTimer)
        {
            TimerText.text = "∞";
            return;
        }

        phaseManager = FindFirstObjectByType<PhaseManager>();
        if (phaseManager == null )
        {
            Debug.LogError("Not Found PhaseManager!");
        }
    }

    void Update()
    {
        if (isCompleted || !useTimer) return;

        LimitTime -= Time.deltaTime;

        if (LimitTime <= 0f )
        {
            LimitTime = 0f;
            isCompleted = true;

            Debug.Log("Finish PuzzleTimer");
            phaseManager.PuzzleCompleted();
        }

        TimerText.text = LimitTime.ToString("F0");
    }
}
