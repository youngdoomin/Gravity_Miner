using System.Collections;
using UnityEngine;

public class ParticleEfManager : MonoBehaviour
{
    public ParticleSystem particle;
    private ParticleSystemRenderer psr;
    public static float gravityVal;
    ParticleSystem.ForceOverLifetimeModule forceMod;
    private void Awake()
    {
    }
    void Start()
    {
        particle.Stop();
        psr = GetComponent<ParticleSystemRenderer>();
       // gravityVal = -SubGravity.sp;
    }

    void Update()
    {
        forceMod = particle.forceOverLifetime;
        
        var main = particle.main;
        
        if(particle.startLifetime > 2.0f)
        {
            
            while(psr.maxParticleSize > 0)
            {
                psr.maxParticleSize -= 0.1f;

            }
            //particle.Stop();
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
            main.simulationSpace = ParticleSystemSimulationSpace.Local;
        }
        particle.gravityModifier = gravityVal;
        if (Input.GetKey(KeyCode.S) && Playercontroller.energy >= 0 || SubGravity.sp == SubGravity.speedLock)
        {
            gravityVal = -PGravity.power * 7;
        }
        else if (Input.GetKey(KeyCode.W) && Playercontroller.energy >= 0)
        {
            gravityVal = PGravity.power * 3;
        }
        else if (Input.GetKey(KeyCode.A) && Playercontroller.energy >= 0)
        {
            forceMod.x = -180;
        }
        else if (Input.GetKey(KeyCode.D) && Playercontroller.energy >= 0)
        {
            forceMod.x = 180;
        }
        else
        {
            gravityVal = 2;
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
