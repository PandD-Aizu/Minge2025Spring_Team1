using System;
using NaughtyAttributes;
using UnityEngine;

namespace Dialogue
{
    public class ScenarioPresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")] 
        [SerializeField] private ScenarioModel model;
        [SerializeField] private ScenarioView view;
        [SerializeField] private LogWindowView logWindowView;
        [ReadOnly] private ScenarioLoader scenarioLoader;

        // @brief エントリポイント
        private void Start()
        {
            scenarioLoader = new ScenarioLoader();
            model.Init();
            LoadScenario();
            SubscribeEvents();
        }

        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            // 会話ウィンドウの表示更新
            view.DialogueButton.onClick
                .AddListener(() =>
                {
                    Debug.Log("ボタンが押されました");
                    
                    if (model.CurrentSceneIndex < model.ScenarioData.scenes.Length)
                    {
                        view.UpdateText(model.ScenarioData, model.CurrentSceneIndex++);
                        logWindowView.AddScenario(model.ScenarioData.scenes[model.CurrentSceneIndex - 1]);
                    }
                    else if (model.CurrentFilePathIndex < model.FilePath.Length)
                    {
                        model.CurrentSceneIndex = 0;
                        logWindowView.ClearScenarioData();
                        LoadScenario();
                    }
                });
            
            // メニューパネルの表示切替
            view.MenuButton.onClick
                .AddListener(() =>
                {
                    view.ToggleMenuPanel();
                });
            
            // ログパネルの表示切替
            view.LogButton.onClick
                .AddListener(() =>
                {
                    view.ToggleDialoguePanel();
                });
        }

        // @brief シナリオ読み込み
        private void LoadScenario()
        {
            model.ScenarioData = scenarioLoader.LoadScenario(model.FilePath[model.CurrentFilePathIndex++]); // シナリオ読み込み
            
            view.UpdateText(model.ScenarioData, model.CurrentSceneIndex++);
            logWindowView.AddScenario(model.ScenarioData.scenes[model.CurrentSceneIndex - 1]);
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            scenarioLoader = null;
        }
    }
}