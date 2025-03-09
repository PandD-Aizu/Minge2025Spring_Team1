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

        public void Init(string costText, Sprite sprite)
        {
            this.costText.text = "Cost: " + costText;
            spriteRenderer.sprite = sprite;
        }
    }
}