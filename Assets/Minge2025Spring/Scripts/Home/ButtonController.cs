using System.Collections.Generic;
using Title;
using UnityEngine;
using UnityEngine.UI;

namespace Home
{
    public class ButtonController : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private ButtonFunc buttonFunc;

        [Header("ボタン")] 
        [SerializeField] private Button stageSelectButton;
        [SerializeField] private Button optionButton;
        [SerializeField] private Button backToTitleButton;
        [SerializeField] private Button Stage01Button;
        [SerializeField] private Button Stage02Button;
        [SerializeField] private Button Stage03Button;
        [SerializeField] private Button Stage04Button;
        [SerializeField] private Button Stage05Button;
        [SerializeField] private List<Button> backButton;

        private void Start()
        {
            stageSelectButton.onClick.AddListener(buttonFunc.OnStageSelectButtonClicked());
            optionButton.onClick.AddListener(buttonFunc.OnOptionButtonClicked());
            backToTitleButton.onClick.AddListener(buttonFunc.OnBackToTitleButtonClicked());
            Stage01Button.onClick.AddListener(buttonFunc.OnStage01ButtonClicked());
            Stage02Button.onClick.AddListener(buttonFunc.OnStage02ButtonClicked());
            Stage03Button.onClick.AddListener(buttonFunc.OnStage03ButtonClicked());
            Stage04Button.onClick.AddListener(buttonFunc.OnStage04ButtonClicked());
            Stage05Button.onClick.AddListener(buttonFunc.OnStage05ButtonClicked());
            
            foreach(var button in backButton)
                button.onClick.AddListener(buttonFunc.OnBackButtonClicked());
        }
    }
}
