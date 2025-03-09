using UnityEngine;
using UnityEngine.UI;

namespace CharacterDeploySys
{
    public class CharacterDeployView : MonoBehaviour
    {
        [Header("親オブジェクト")] 
        [SerializeField] private GameObject parent;
        
        [Header("味方ユニットのプレハブ")]
        [SerializeField] private GameObject characterPrefab;

        [Header("プレビュー")] 
        [SerializeField] private GameObject cursorPreview;
        
        public GameObject CharacterPrefab { get => characterPrefab; }
        public GameObject CursorPreview { get => cursorPreview; }
        
        // @brief プレビューの画像を変更
        // @param sprite 画像
        public void ChangeCursorPreviewSprite(Sprite sprite)
        {
            cursorPreview.transform.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        
        // @brief カーソル位置にキャラクターを配置
        // @param pos カーソルの位置
        public void DeployAlly(Vector3 pos)
        {
            GameObject newGameObject = Instantiate(characterPrefab, pos, Quaternion.identity);
            newGameObject.transform.SetParent(parent.transform, false);
        }
    }
}