using System.Collections.Generic;
using CharacterBehaviour;
using CharacterInfo;
using Cost;
using EnhanceCard;
using EnhanceCard.Result;
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
            EnhanceCard.EnhanceCard card = null;
            List<ICardEffect> cardEffects = null;
            Ray uiRay = view.UIRenderCamera.ScreenPointToRay(Input.mousePosition);
            Ray charaRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit uiHit = new RaycastHit();
            RaycastHit charaHit = new RaycastHit();

            if (Input.GetMouseButton(0) &&
                Physics.Raycast(uiRay, out uiHit, Mathf.Infinity, model.EnhanceCardUILayer) &&
                !model.IsDeployActiveValue)
            {
                card = uiHit.collider.gameObject.GetComponent<EnhanceCard.EnhanceCard>();
                cardEffects = card.Enhances.GetValueOrDefault(EnhanceTrigger.ON_USE);
                model.IsDeployActiveValue = true;
            }

            if (Input.GetMouseButtonUp(0) &&
                Physics.Raycast(charaRay, out charaHit, Mathf.Infinity, model.CharacterLayer) &&
                model.IsDeployActiveValue)
            {
                AllyInfo allyInfo = charaHit.collider.gameObject.GetComponent<CharacterBehaviourPresenter>().AllyInfo;
                if (allyInfo != null && cardEffects != null)
                {
                    foreach (var effect in cardEffects)
                    {
                        IEnhanceResult result = effect.GiveEffect(new EnhanceParam(allyInfo));
                        if (result is IEnhanceResult enhanceResult)
                        {
                            // 効果を適用する
                            enhanceResult.;
                        }
                    }

                    // コストを消費する
                    costControllerModel.Cost.Value -= card.GetCost;
                }
            }
        }

        private void SubscribeEvents()
        {

        }
    }
}