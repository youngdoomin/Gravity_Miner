using System.Collections;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    public Transform player;
    public ParticleSystem particle;
    public Transform gridObj;
    void Update()
    {
        var main = particle.main;
        if (particle.isPlaying)
        {
            //gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.gameObject.transform.position.z);
            this.gameObject.transform.SetParent(gridObj);
            //main.simulationSpace = ParticleSystemSimulationSpace.World;
        }
        else
        {
            this.gameObject.transform.SetParent(player);
            //main.simulationSpace = ParticleSystemSimulationSpace.Local;
        }

    }
    IEnumerator ParticlePlay()
    {
        particle.Play();
        //particle.startLifetime = 0;
        yield return null;
    }
}
