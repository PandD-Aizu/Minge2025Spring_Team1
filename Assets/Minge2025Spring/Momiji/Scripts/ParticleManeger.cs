using UnityEngine;
using Static;

public class ParticleManeger : MonoBehaviour
{
    public GameObject AmbientParticle;
    public GameObject FogParticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Particle.ambientFlag) AmbientParticle.SetActive(true);
        else AmbientParticle.SetActive(false);

        if (Particle.fogFlag) FogParticle.SetActive(true);
        else FogParticle.SetActive(false);
    }
}
