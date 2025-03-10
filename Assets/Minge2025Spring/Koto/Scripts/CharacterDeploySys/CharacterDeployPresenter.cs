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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit characterHit = new RaycastHit();
            RaycastHit cellHit = new RaycastHit();
            RaycastHit previewHit = new RaycastHit();
            
            // キャラクターのUIをクリックしたら、プレビューを表示
            if (Input.GetMouseButton(0) && 
                Physics.Raycast(ray, out characterHit, Mathf.Infinity, model.CharacterUILayer) &&
                !model.IsDeployActive)
            {
                allyInfo = characterHit.collider.gameObject.GetComponent<CharacterUIPresenter>().Model; // キャラ情報を取得する
                
                // コストが足りていたら、プレビューを表示する
                if(costControllerModel.Cost.Value >= allyInfo.Cost)
                {
                    //view.ChangeCursorPreviewSprite(allyInfo.CharacterSprite);                                       // プレビューの画像をキャラの画像に変更
                    SetLayer(allyInfo.CharacterLayer);                                                                // プレビューのレイヤーを変更
                    
                    model.Cost = allyInfo.Cost;                                                                       // コストを設定
                    model.SelectedCharacter = characterHit.collider.gameObject;                                       // キャラクターのUIを設定
                    model.IsDeployActive = true;                                                                      // ユニット配置可能フラグを立てる
                }
            }

            // マウスを押している間、3D空間上にプレビューを表示する
            if (Input.GetMouseButton(0) && 
                Physics.Raycast(ray, out cellHit, Mathf.Infinity, model.DeployLayer)
                && model.IsDeployActive)
            {
                Vector3 characterDeployPos = cellHit.collider.gameObject.transform.position + Vector3.up;
                view.SetPreview(characterDeployPos);
                model.IsDeployAvailable = true;
            }
            
            // マウスを押している状態で、配置可能マス外にカーソルがある場合、プレビューをカーソルに追従するようにする
            if (Input.GetMouseButton(0) && 
                !Physics.Raycast(ray, out cellHit, Mathf.Infinity, model.DeployLayer) &&
                model.IsDeployActive)
            {
                Vector3 previewPos = GetMouseWorldPosition(ray);
                view.SetPreview(previewPos);
                model.IsDeployAvailable = false;
            }

            // 設置可能であり、マウスを離したときユニットを配置する
            if (Input.GetMouseButtonUp(0) && model.IsDeployActive && model.IsDeployAvailable)
            {
                costControllerModel.Cost.Value -= model.Cost;           // コストを減らす
                view.DeployAlly(view.CursorPreview.transform.position); // ユニットを配置する
                Destroy(model.SelectedCharacter);                       // 選択中のキャラUIを削除する
                model.Init();
                view.Init();
            }
            
            // 設置可能ではないが、マウスを離したときプレビューを初期化する
            if(Input.GetMouseButtonUp(0) && model.IsDeployActive && !model.IsDeployAvailable)
            {
                model.Init();
                view.Init();
            }
        }
        
        // @brief プレビューのレイヤーを変更
        private void SetLayer(LayerMask layer)
        {
            model.DeployLayer = layer;
        }
        
        // @brief マウスのワールド座標を取得
        // @param ray レイ
        // @return マウスのワールド座標
        private Vector3 GetMouseWorldPosition(Ray ray)
        {
            // カーソル位置のワールド座標を取得
            Plane plane = new Plane(Vector3.up, Vector3.zero); // (0, 0, 0)を基準にする
            
            if (plane.Raycast(ray, out float distance))
                return ray.GetPoint(distance) + Vector3.up;
            
            return Vector3.up;
        }
    }
}