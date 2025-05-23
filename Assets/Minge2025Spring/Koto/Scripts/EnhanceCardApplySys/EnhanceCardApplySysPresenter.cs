using System.Collections.Generic;
using CharacterBehaviour;
using CharacterInfo;
using Cost;
using EnhanceCard;
using UnityEngine;

namespace EnhanceCardApplySys
{
    public class EnhanceCardApplySysPresenter : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private EnhanceCardApplySysModel model;
        [SerializeField] private EnhanceCardApplySysView view;
        [SerializeField] private EnhanceCardApplySysEmitter emitter;
        [SerializeField] private CostControllerModel costControllerModel;

        private void Start()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            EnhanceCard.EnhanceCard card;
            Ray uiRay = view.UIRenderCamera.ScreenPointTORay(Input.mousePosition);
            Ray charaRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit uiHit = new RaycastHit();
            RaycastHit charaHit = new RaycastHit();

            if (Input.GetMouseButton(0) &&
                Physics.Raycast(uiRay, out uiHit, Mathf.Infinity, model.EnhanceCardUILayer) &&
                !model.IsDeployActive)
            {
                card = uiHit.collider.gameObject.GetComponent<EnhanceCard.EnhanceCard>();
                model.IsDeployActive = true;
            }

            if (Input.GetMouseButtonUp(0) &&
                Physics.Raycast(charaRay, out charaHit, Mathf.Infinity, model.CharacterLayer) &&
                model.IsDeployActive)
            {
                AllyInfo allyInfo = charaHit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo;
            }
        }

        private void SubscribeEvents()
        {
            
        }
    }
}