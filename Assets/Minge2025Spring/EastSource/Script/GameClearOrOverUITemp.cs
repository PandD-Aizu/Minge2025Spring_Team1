using System;
using TMPro;
using UnityEngine;

public class GameClearOrOverUITemp : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI GameCleartext;
    [SerializeField]private TextMeshProUGUI GameOverText;

    private void Start()
    {
        Hide();
        GameManager.Instance.OnGameClear += GameManager_OnGameClear;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameClear -= GameManager_OnGameClear;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        Show();
        GameCleartext.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void GameManager_OnGameClear(object sender, EventArgs e)
    {
        Show();
        GameCleartext.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
