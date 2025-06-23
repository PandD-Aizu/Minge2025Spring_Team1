using General;
using UnityEngine;
using UnityEngine.UI;

namespace StageSelect
{
    public class ButtonController : MonoBehaviour
    {
        [Header("ボタン")] 
        [SerializeField] private Button returnButton;

        private void Start()
        {
            returnButton.onClick.AddListener(OnReturnButtonClick);
        }

        private void OnReturnButtonClick()
        {
            SceneController.LoadSceneAsync("Title");
        }
    }
}