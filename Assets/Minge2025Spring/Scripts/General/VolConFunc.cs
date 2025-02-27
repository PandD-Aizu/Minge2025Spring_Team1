using UnityEngine;

namespace General
{
    public class VolConFunc : MonoBehaviour
    {
        // @brief マスターボリュームの設定
        public void SetMasterVolume(float value)
        {
            Static.Volume.masterVolume = value;
        }
        
        // @brief BGMボリュームの設定
        public void SetBgmVolume(float value)
        {
            Static.Volume.bgmVolume = value;
        }
        
        // @brief SEボリュームの設定
        public void SetSeVolume(float value)
        {
            Static.Volume.seVolume = value;
        }
    }
}