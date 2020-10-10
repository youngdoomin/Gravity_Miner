using System.Collections;
using UnityEngine;

public class ParticleEfManager : MonoBehaviour
{
    public ParticleSystem particle;
    private ParticleSystemRenderer psr;
    ParticleSystem.ForceOverLifetimeModule forceMod;

    void Start()
    {
        psr = GetComponent<ParticleSystemRenderer>();
    }

    void Update()
    {
        forceMod = particle.forceOverLifetime;

        /*
        var main = particle.main;
        
        if(particle.startLifetime > 2.0f)
        {
            
            while(psr.maxParticleSize > 0)
            {
                psr.maxParticleSize -= 0.1f;

            }
            particle.Stop();
        }
        else
        {
            psr.maxParticleSize = 0.5f;
        }
        
        if (particle.isPlaying)
        {
            
            main.simulationSpace = ParticleSystemSimulationSpace.World;
        }
        else
        {
            //gameObject.SetActive(false);
            main.simulationSpace = ParticleSystemSimulationSpace.Local;
        }
        */
        particle.gravityModifier = GameManager.Instance.gravityVal;
        if (Input.GetKey(KeyCode.S) && GameManager.Instance.energy >= 0 || GameManager.Instance.sp == GameManager.Instance.speedLock)
        { GameManager.Instance.gravityVal = -PGravity.power * 7; }
        else if (Input.GetKey(KeyCode.W) && GameManager.Instance.energy >= 0)
        { GameManager.Instance.gravityVal = PGravity.power * 3; }
        else if (Input.GetKey(KeyCode.A) && GameManager.Instance.energy >= 0)
        { forceMod.x = -180; }
        else if (Input.GetKey(KeyCode.D) && GameManager.Instance.energy >= 0)
        { forceMod.x = 180; }
        else
        {
            GameManager.Instance.gravityVal = 2;
            forceMod.x = 0;
        }
    }

    IEnumerator ParticlePlay()
    {
        particle.Play();
        //particle.startLifetime = 0;
        yield return null;
    }

}
