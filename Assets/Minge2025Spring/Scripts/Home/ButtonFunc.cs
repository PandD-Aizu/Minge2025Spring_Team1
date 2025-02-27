using General;
using UnityEngine;
using UnityEngine.Events;

namespace Home
{
    public class ButtonFunc : MonoBehaviour
    {
        [Header("パネル")]
        [SerializeField] private GameObject homePanel;
        [SerializeField] private GameObject stageSelectPanel;
        [SerializeField] private GameObject optionPanel;

        private void Start()
        {
            homePanel.SetActive(true);
            stageSelectPanel.SetActive(false);
            optionPanel.SetActive(false);
        }
        
        // @brief ステージ選択画面への切り替え
        public UnityAction OnStageSelectButtonClicked()
        {
            return () =>
            {
                homePanel.SetActive(false);
                stageSelectPanel.SetActive(true);
            };
        }

        // @brief 設定画面への切り替え
        public UnityAction OnOptionButtonClicked()
        {
            return () =>
            {
                homePanel.SetActive(false);
                optionPanel.SetActive(true);
            };
        }

        // @brief タイトル画面への遷移
        public UnityAction OnBackToTitleButtonClicked()
        {
            return () =>
            {
                SceneController.LoadSceneAsync("Title");
            };
        }
        
        // @brief ステージ01への遷移
        public UnityAction OnStage01ButtonClicked()
        {
            return () =>
            {
                SceneController.LoadSceneAsync("Stage01");
            };
        }
        
        // @brief ステージ02への遷移
        public UnityAction OnStage02ButtonClicked()
        {
            return () =>
            {
                SceneController.LoadSceneAsync("Stage02");
            };
        }
        
        // @brief ステージ03への遷移
        public UnityAction OnStage03ButtonClicked()
        {
            return () =>
            {
                SceneController.LoadSceneAsync("Stage03");
            };
        }
        
        // @brief ステージ04への遷移
        public UnityAction OnStage04ButtonClicked()
        {
            return () =>
            {
                SceneController.LoadSceneAsync("Stage04");
            };
        }
        
        // @brief ステージ05への遷移
        public UnityAction OnStage05ButtonClicked()
        {
            return () =>
            {
                SceneController.LoadSceneAsync("Stage05");
            };
        }
        
        // @brief ホーム画面への切り替え
        public UnityAction OnBackButtonClicked()
        {
            return () =>
            {
                homePanel.SetActive(true);
                stageSelectPanel.SetActive(false);
                optionPanel.SetActive(false);
            };
        }
    }
}