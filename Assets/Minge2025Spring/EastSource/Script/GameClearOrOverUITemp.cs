using System;
using Cysharp.Threading.Tasks;
using FMODUnity;
using Static;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = FMOD.Debug;

public class GameClearOrOverUITemp : MonoBehaviour
{
    [Header("Binding")] [SerializeField] private GameObject panel;
    [SerializeField]private TextMeshProUGUI GameCleartext;
    [SerializeField]private TextMeshProUGUI GameOverText;
    [SerializeField] private StudioEventEmitter gameOverSE;
    [SerializeField] private StudioEventEmitter bgm;

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

    private async void GameManager_OnGameOver(object sender, EventArgs e)
    {
        Show();
        GameCleartext.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true);
        gameOverSE.Play();
        bgm.Stop();
        // Time.timeScale = 0f;
        await UniTask.WaitForSeconds(5.0f)
            .ContinueWith(() =>
            {
                SceneManager.LoadSceneAsync("Title");
            });
    }

    private async void GameManager_OnGameClear(object sender, EventArgs e)
    {
        Show();
        GameCleartext.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(false);
        bgm.Stop();
        // Time.timeScale = 0f;
        await UniTask.WaitForSeconds(5.0f)
            .ContinueWith(() =>
            {
                SceneManager.LoadSceneAsync("Title");
                StageRelease.stage_num++;
            });
    }

    private void Show()
    {
        panel.gameObject.SetActive(true);
    }

    private void Hide()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
