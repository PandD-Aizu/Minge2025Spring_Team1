using Static;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering;

namespace General
{
    public class VolumeController : SingletonWithMonoBehaviour<VolumeController>
    { 
        [Header("Volume")] 
        [SerializeField] private float masterVolume;
        [SerializeField] private float bgmVolume;
        [SerializeField] private float sfxVolume;

        private readonly string masterVCAPath = "vca:/Master";
        private readonly string bgmVCAPath = "vca:/BGM";
        private readonly string seVCAPath = "vca:/SE";
        
        private FMOD.Studio.VCA masterVCA; 
        private FMOD.Studio.VCA bgmVCA;
        private FMOD.Studio.VCA seVCA;
        
        // @brief 初期化処理
        protected override void OnAwakeProcess()
        {
            DontDestroyOnLoad(this);
        }

        // @brief 更新処理
        public void Update()
        {
            if (!masterVCA.isValid() || !bgmVCA.isValid() || !seVCA.isValid()) 
            {
                masterVCA = RuntimeManager.GetVCA(masterVCAPath);
                bgmVCA = RuntimeManager.GetVCA(bgmVCAPath);
                seVCA = RuntimeManager.GetVCA(seVCAPath);
            }

            masterVCA.setVolume(Static.Volume.masterVolume);
            bgmVCA.setVolume(Static.Volume.bgmVolume); 
            seVCA.setVolume(Static.Volume.seVolume);
        }
    }
}