using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public Transform player;
    public ParticleSystem particle;

    // Update is called once per frame
    void Update()
    { var main = particle.main;
        if (particle.isPlaying)
        { 
            main.simulationSpace = ParticleSystemSimulationSpace.World;
        }
        else
        {
            transform.position = new Vector3(0, 0, 0);
            main.simulationSpace = ParticleSystemSimulationSpace.Local;
        }
        
    }
}
