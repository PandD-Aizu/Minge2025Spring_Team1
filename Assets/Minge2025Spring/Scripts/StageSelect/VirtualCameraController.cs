using System.Collections.Generic;
using Static;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace StageSelect
{
    public class VirtualCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera virtualCamera;

        [SerializeField] private float maxMoveSpeed = 50.0f;
        [SerializeField] private float minMoveSpeed = 5.0f;
        [SerializeField] private float screenEdgeThreshold = 50.0f;

        private void Update()
        {
            Vector3 cameraPosition = virtualCamera.transform.localPosition;
            Vector3 mousePosition = Input.mousePosition;

            // 画面の上下端からの距離を計算
            float distanceToTop = Mathf.Abs(mousePosition.y - Screen.height);
            float distanceToBottom = Mathf.Abs(mousePosition.y);

            // 上下端に近いほど速度を速く、離れるほど遅くする
            float speedFactor = 0.0f;
            if (mousePosition.y >= Screen.height - screenEdgeThreshold)
            {
                speedFactor = Mathf.Clamp01(1.0f - (distanceToTop / screenEdgeThreshold));
            }
            else if (mousePosition.y <= screenEdgeThreshold)
            {
                speedFactor = Mathf.Clamp01(1.0f - (distanceToBottom / screenEdgeThreshold));
            }

            float cameraMoveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, speedFactor);

            // カメラの移動処理
            if (mousePosition.y >= Screen.height - screenEdgeThreshold && cameraPosition.y <= 350.0f)
            {
                cameraPosition.y += Time.deltaTime * cameraMoveSpeed;
            }
            else if (mousePosition.y <= screenEdgeThreshold && cameraPosition.y >= -350.0f)
            {
                cameraPosition.y -= Time.deltaTime * cameraMoveSpeed;
            }

            virtualCamera.transform.localPosition = cameraPosition;
        }
    }
}