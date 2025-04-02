using FMODUnity;
using UnityEngine;

namespace CharacterDeploySys
{
    public class CharacterDeployListen : MonoBehaviour
    {
        [Header("ButtonPush")]
        [SerializeField] private StudioEventEmitter buttonPushEmitter;
        
        [Header("ButtonPushModern")]
        [SerializeField] private StudioEventEmitter buttonPushModernEmitter;
        
        [Header("SetChara")]
        [SerializeField] private StudioEventEmitter setCharaEmitter;
        
        public StudioEventEmitter ButtonPushEmitter => buttonPushEmitter;
        public StudioEventEmitter ButtonPushModernEmitter => buttonPushModernEmitter;
        public StudioEventEmitter SetCharaEmitter => setCharaEmitter;
    }
}