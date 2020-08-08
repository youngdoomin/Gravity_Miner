﻿using System.Collections;
using UnityEngine;
public class DestructTile : MonoBehaviour
{
    public GameObject particle;
    SpriteRenderer tileSp;
    public static bool tileBreak = false;
    void Start()
    {
        particle.SetActive(false);
        tileSp = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //(ColliderDetect.platformDetect == false && collision.gameObject.tag == "Player")
        {
            StartCoroutine(Destruct());
            //Destroy(gameObject);
        }
    }


    IEnumerator Destruct()
    {
        tileBreak = true;
        if (this.gameObject.tag == "Platform_jam")
        {
            this.gameObject.SendMessage("SpawnJam");

        }
        particle.SetActive(true);
        this.gameObject.BroadcastMessage("ParticlePlay");
        tileSp.color = new Color(1,1,1,0);
        GetComponent<BoxCollider2D>().enabled = false;
        
        //tileSp.sprite = null;

        yield return null;
    }
}
