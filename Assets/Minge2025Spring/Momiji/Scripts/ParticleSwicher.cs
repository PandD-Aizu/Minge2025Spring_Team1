using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Static;

public class ParticleSwicher : MonoBehaviour
{
    public TextMeshProUGUI AmbientSwitchButton;
    public TextMeshProUGUI FogSwitchButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AmbientSwitchButton.text = "ON";
        FogSwitchButton.text = "ON";
        Particle.ambientFlag = true;
        Particle.fogFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AmbientOnOff()
    {
        if (Particle.ambientFlag)
        {
            AmbientSwitchButton.text = "OFF";
            Particle.ambientFlag = false;
        }
        else
        {
            AmbientSwitchButton.text = "ON";
            Particle.ambientFlag = true;
        }
    }

    public void FogOnOff()
    {
        if (Particle.fogFlag)
        {
            FogSwitchButton.text = "OFF";
            Particle.fogFlag = false;
        }
        else
        {
            FogSwitchButton.text = "ON";
            Particle.fogFlag = true;
        }
    }
}
