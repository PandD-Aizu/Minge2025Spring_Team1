using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class VolumeSliderController : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private VolConFunc volConFunc;
        
        [Header("スライダー")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider bgmVolumeSlider;
        [SerializeField] private Slider seVolumeSlider;

        private void Start()
        { 
            masterVolumeSlider.onValueChanged.AddListener((value) => volConFunc.SetMasterVolume(value));
            bgmVolumeSlider.onValueChanged.AddListener((value) => volConFunc.SetBgmVolume(value));
            seVolumeSlider.onValueChanged.AddListener((value) => volConFunc.SetSeVolume(value));
        }
    }
}