using FMODUnity;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterBehaviourListen : MonoBehaviour
    {
        [Header("WithDraw")]
        [SerializeField] private StudioEventEmitter withDrawEmitter;
        
        public StudioEventEmitter WithDrawEmitter => withDrawEmitter;
    }
}