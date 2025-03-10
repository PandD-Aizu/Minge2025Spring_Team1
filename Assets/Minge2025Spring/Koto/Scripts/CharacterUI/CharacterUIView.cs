using TMPro;
using UnityEngine;

namespace CharacterUI
{
    public class CharacterUIView : MonoBehaviour
    {
        [Header("コストテキスト")]
        [SerializeField] private TextMeshProUGUI costText;

        [Header("キャラクターの画像")] 
        [SerializeField] private SpriteRenderer spriteRenderer;

        // @brief 初期化処理
        public void Init(string costText, Sprite sprite)
        {
            this.costText.text = "Cost: " + costText;
            spriteRenderer.sprite = sprite;
        }
    }
}