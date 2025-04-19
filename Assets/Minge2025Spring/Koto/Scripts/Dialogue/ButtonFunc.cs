using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dialogue
{
    public class ButtonFunc : MonoBehaviour
    {
        [Header("Optionの依存関係")] 
        [SerializeField] private GameObject optionPanel;
        
        [Header("遷移先のシーン名")]
        [SerializeField] private string nextScene;
        
        [Header("ボタン")]
        [SerializeField] private Button optionButton;
        [SerializeField] private Button optionCloseButton;
        [SerializeField] private Button skipButton;
        
        // @brief ボタンにイベントを登録
        public void SubscribeEvents()
        {
            optionButton.onClick
                .AddListener(() =>
                {
                    optionPanel.SetActive(true);
                    optionPanel.transform.DOLocalMoveY(0, 0.5f)
                        .SetEase(Ease.OutSine);
                });
            
            optionCloseButton.onClick
                .AddListener(() =>
                {
                    optionPanel.transform.DOLocalMoveY(1100, 0.5f)
                        .SetEase(Ease.OutSine)
                        .OnComplete(() => optionPanel.SetActive(false));
                });

            skipButton.onClick
                .AddListener(() =>
                {
                    SceneManager.LoadSceneAsync(nextScene);
                });
        }
    }
}