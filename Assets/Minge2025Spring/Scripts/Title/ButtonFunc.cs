using UnityEngine;
using UnityEngine.Events;

namespace Title
{
    public class ButtonFunc : MonoBehaviour
    {
        [Header("パネル")]
        [SerializeField] private GameObject titlePanel;
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private GameObject creditPanel;

        private void Start()
        {
            titlePanel.SetActive(true);
            optionPanel.SetActive(false);
            creditPanel.SetActive(false);
        }
        
        // @brief 次のシーンへの遷移
        public UnityAction OnStartButtonClicked()
        {
            return () => General.SceneController.LoadSceneAsync("StageSelect");
        }
        
        // @brief ゲーム終了
        public UnityAction OnExitButtonClicked()
        {
            return () =>
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            };
        }
        
        // @brief 設定画面への切り替え
        public UnityAction OnOptionButtonClicked()
        {
            return () =>
            {
                titlePanel.SetActive(false);
                optionPanel.SetActive(true);
            };
        }
        
        // @brief クレジット画面への切り替え
        public UnityAction OnCreditButtonClicked()
        {
            return () =>
            {
                titlePanel.SetActive(false);
                creditPanel.SetActive(true);
            };
        }
        
        //@brief ホーム画面への切り替え
        public UnityAction OnBackButtonClicked()
        {
            return () =>
            {
                titlePanel.SetActive(true);
                optionPanel.SetActive(false);
                creditPanel.SetActive(false);
            };
        }
    }
}