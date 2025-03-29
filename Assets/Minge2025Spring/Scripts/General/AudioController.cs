using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace General
{
    public class AudioController : MonoBehaviour
    {
        [Header("BGM")]
        [SerializeField] private List<StudioEventEmitter> bgmEmitterList;
        
        [Header("SE")]
        [SerializeField] private List<StudioEventEmitter> seEmitterList;

        public List<StudioEventEmitter> BGMEmitterList => BGMEmitterList;
        public List<StudioEventEmitter> SEEmitterList => SEEmitterList;
    }
}