using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterDeploySys
{
    public class CharacterDeployView : MonoBehaviour
    {
        [Header("味方のモデルを置く親オブジェクト")] 
        [SerializeField] private GameObject parent;
        
        [Header("味方ユニットのプレハブ")]
        [SerializeField] private List<GameObject> allyPrefabs;

        [Header("プレビュー")] 
        [SerializeField] private GameObject cursorPreview;
        
        /* getter と setter */
        public List<GameObject> AllyPrefabs { get => allyPrefabs; }
        public GameObject CursorPreview     { get => cursorPreview; }

        // @brief 初期化処理
        public void Init()
        {
            cursorPreview.SetActive(false);
        }
        
        // @brief プレビューの画像を変更
        // @param sprite 画像
        public void ChangeCursorPreviewSprite(Sprite sprite)
        {
            cursorPreview.transform.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        
        // @brief キャラクターのプレビューを表示する
        public void SetPreview(Vector3 previewPos)
        {
            cursorPreview.SetActive(true);
            cursorPreview.transform.position = previewPos;
        }
        
        // @brief カーソル位置にキャラクターを配置
        // @param pos カーソルの位置
        public void DeployAlly(string prefabName, Vector3 pos)
        {
            GameObject newGameObject;
            newGameObject = Instantiate(allyPrefabs.Find(x => x.name == prefabName), pos, Quaternion.identity);
            newGameObject.transform.SetParent(parent.transform, false);
        }
    }
}