using System;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class ScenarioView : MonoBehaviour
    {
        [Header("パネル")] 
        [SerializeField] private GameObject menuPanel;     // メニューパネル
        [SerializeField] private GameObject dialoguePanel; // ログパネル
         
        [Header("テキスト")] 
        [SerializeField] private TextMeshProUGUI nameText;     // キャラクタ名
        [SerializeField] private TextMeshProUGUI sentenceText; // セリフ

        [Header("ボタン")] 
        [SerializeField] private Button dialogueButton; // ダイアログ進行ボタン
        [SerializeField] private Button menuButton;     // メニューボタン
        [SerializeField] private Button logButton;      // ログボタン

        [Header("背景")] 
        [SerializeField] private SpriteRenderer background;
        
        /* getter と setter */
        public TextMeshProUGUI NameText     { get => nameText; set => nameText = value; }
        public TextMeshProUGUI SentenceText { get => sentenceText; set => sentenceText = value; }
        public Button DialogueButton        { get => dialogueButton; }
        public Button MenuButton            { get => menuButton; }
        public Button LogButton             { get => logButton; }

        // @brief 会話ウィンドウの表示更新
        // @param scenarioData シナリオデータ, currentIndex シナリオのインデックス
        public void UpdateText(ScenarioData scenarioData, int currentIndex)
        {
            NameText.text = scenarioData.scenes[currentIndex].name;
            SentenceText.text = scenarioData.scenes[currentIndex].sentence;
        }
        
        // @brief メニューパネルの表示切替
        public void ToggleMenuPanel()
        {
            if (!menuPanel.activeSelf) // メニューパネルを上から表示 
            {
                menuPanel.SetActive(!menuPanel.activeSelf);
                menuPanel.transform.DOLocalMoveY(0, 0.5f)
                    .SetEase(Ease.OutSine);
            }
            else                       // メニューパネルを上に隠す
            {
                menuPanel.transform.DOLocalMoveY(850, 0.5f)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() => menuPanel.SetActive(!menuPanel.activeSelf));
            }
        }
        
        public void ToggleDialoguePanel()
        {
            if (!dialoguePanel.activeSelf) // ダイアログパネルを上から表示 
            {
                dialoguePanel.SetActive(!dialoguePanel.activeSelf);
                dialoguePanel.transform.DOLocalMoveY(0, 0.5f)
                    .SetEase(Ease.OutSine);
            }
            else                       // ダイアログパネルを上に隠す
            {
                dialoguePanel.transform.DOLocalMoveY(1100, 0.5f)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() => dialoguePanel.SetActive(!dialoguePanel.activeSelf));
            }
        }
    }
}