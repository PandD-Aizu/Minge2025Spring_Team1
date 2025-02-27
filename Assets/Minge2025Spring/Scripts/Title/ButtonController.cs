using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class ButtonController : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private ButtonFunc buttonFunc;
        
        [Header("ボタン")]
        [SerializeField] private Button startButton;     // ホーム画面への遷移
        [SerializeField] private Button exitButton;      // ゲーム終了
        [SerializeField] private Button optionButton;    // 設定画面への切り替え
        [SerializeField] private Button creditButton;    // クレジット画面への切り替え
        [SerializeField] private List<Button> backButton; // タイトル画面への切り替え 

        private void Start()
        {
            startButton.onClick.AddListener(buttonFunc.OnStartButtonClicked());
            exitButton.onClick.AddListener(buttonFunc.OnExitButtonClicked());
            optionButton.onClick.AddListener(buttonFunc.OnOptionButtonClicked());
            creditButton.onClick.AddListener(buttonFunc.OnCreditButtonClicked());
            
            
            foreach(var button in backButton)
                button.onClick.AddListener(buttonFunc.OnBackButtonClicked());
        }
    }
}