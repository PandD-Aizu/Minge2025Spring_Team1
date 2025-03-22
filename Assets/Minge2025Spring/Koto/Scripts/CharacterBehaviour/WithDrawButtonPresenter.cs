using CharacterInfo;
using UnityEngine;

namespace CharacterBehaviour
{
    public class WithDrawButtonPresenter : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private CharacterBehaviourModel model;
        [SerializeField] private AllyInfo allyInfo;
        [SerializeField] private CharacterBehaviourView view;

        // @brief メインループ
        private void Update()
        {
            CheckRaycast();
        }

        private void CheckRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Input.GetMouseButtonUp(0) &&
                Physics.Raycast(ray, out hit, Mathf.Infinity, allyInfo.CharacterRaycastLayer) &&
                !model.IsWithDraw.Value)
            {
                if(hit.collider.gameObject == gameObject)
                    model.IsWithDraw.Value = true;
            }
        }
    }
}