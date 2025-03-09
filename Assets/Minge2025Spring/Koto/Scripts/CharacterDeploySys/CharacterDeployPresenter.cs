using CharacterInfo;
using CharacterUI;
using Cost;
using UnityEngine;

namespace CharacterDeploySys
{
    public class CharacterDeployPresenter : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private CharacterDeployModel model;
        [SerializeField] private CharacterDeployView view;
        [SerializeField] private CostControllerModel costControllerModel;
        
        

        // @brief エントリポイント
        private void Start()
        {
            model.Init();
            SubscribeEvents();
        }
        
        // @brief エントリポイント
        private void Update()
        {
            SetCharacterPos();
        }

        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            
        }
        
        // @brief キャラクターの位置を設定
        private void SetCharacterPos()
        {
            AllyInfo allyInfo = null;
            Ray characterRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray cellRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit characterHit = new RaycastHit();
            RaycastHit cellHit = new RaycastHit();
            
            // キャラクターのUIをクリックしたら、プレビューを表示
            if (Input.GetMouseButton(0) && 
                Physics.Raycast(characterRay, out characterHit, Mathf.Infinity, model.CharacterUILayer) &&
                !model.IsDeployActive)
            {
                Debug.Log("キャラクターのUIをクリックしました");
                allyInfo = characterHit.collider.gameObject.GetComponent<CharacterUIPresenter>().Model; // キャラ情報を取得する
                
                // コストが足りていたら、プレビューを表示する
                if(costControllerModel.Cost.Value >= allyInfo.Cost)
                {
                    characterHit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray; // 選択中のキャラUIはちょっと黒くする
                    //view.ChangeCursorPreviewSprite(allyInfo.CharacterSprite);                                       // プレビューの画像をキャラの画像に変更
                    SetLayer(allyInfo.CharacterLayer);                                                              // プレビューのレイヤーを変更
                    
                    model.Cost = allyInfo.Cost;                                                                     // コストを設定
                    model.SelectedCharacter = characterHit.collider.gameObject;                                     // キャラクターのUIを設定
                    model.IsDeployActive = true;                                                                    // ユニット配置可能フラグを立てる
                }
            }

            // マウスを押している間、3D空間上にプレビューを表示する
            if (Input.GetMouseButton(0) && 
                Physics.Raycast(cellRay, out cellHit, Mathf.Infinity, model.CharacterLayer) &&
                model.IsDeployActive)
            {
                Vector3 characterDeployPos = cellHit.collider.gameObject.transform.position + Vector3.up;
                view.CursorPreview.transform.position = characterDeployPos;
            }

            // マウスを離したら、キャラクターを配置する
            if (Input.GetMouseButtonUp(0) && model.IsDeployActive)
            {
                costControllerModel.Cost.Value -= model.Cost;                                          // コストを減らす
                view.DeployAlly(view.CursorPreview.transform.position);                          // ユニットを配置する
                Destroy(model.SelectedCharacter);                                                // 選択中のキャラUIを削除する
                model.Init();
            }
        }
        
        // @brief プレビューのレイヤーを変更
        private void SetLayer(LayerMask layer)
        {
            model.CharacterLayer = layer;
        }
    }
}