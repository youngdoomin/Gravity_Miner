using System.Collections;
using UnityEngine;
public class DestructTile : MonoBehaviour
{
    SpriteRenderer tileSp;
    public static bool tileBreak = false;
    void Start()
    {
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
        this.gameObject.BroadcastMessage("ParticlePlay");
        tileSp.sprite = null;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return null;
    }
}
