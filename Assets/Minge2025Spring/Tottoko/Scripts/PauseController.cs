using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject stopButton;
    public GameObject runButton;
    public GameObject pauseOverlay;

    public void OnStopButtonPressed()
    {
        Debug.Log("Stopボタンが押されました");

        Time.timeScale = 0f;
        isPaused = true;

        runButton.SetActive(true);
        stopButton.SetActive(false);
        pauseOverlay.SetActive(true);
    }

    public void OnRunButtonPressed()
    {
        Debug.Log("Runボタンが押されました");

        Time.timeScale = 1f;
        isPaused = false;

        runButton.SetActive(false);
        stopButton.SetActive(true);
        pauseOverlay.SetActive(false);
    }
}

