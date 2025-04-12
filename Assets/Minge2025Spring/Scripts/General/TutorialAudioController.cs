using FMODUnity;
using UnityEngine;

namespace General
{
    public class TutorialAudioController : MonoBehaviour
    {
        [Header("BGM")]
        [SerializeField] private StudioEventEmitter bgmEmitter;
        
        [Header("SE")]
        [SerializeField] private StudioEventEmitter buttonPushEmitter;
        [SerializeField] private StudioEventEmitter buttonPushModernEmitter;
        [SerializeField] private StudioEventEmitter setCharaEmitter;
        [SerializeField] private StudioEventEmitter withDrawEmitter;
        
        public StudioEventEmitter BgmEmitter => bgmEmitter;
        public StudioEventEmitter ButtonPushEmitter => buttonPushEmitter;
        public StudioEventEmitter ButtonPushModernEmitter => buttonPushModernEmitter;
        public StudioEventEmitter SetCharaEmitter => setCharaEmitter;
        public StudioEventEmitter WithDrawEmitter => withDrawEmitter;
    }
}