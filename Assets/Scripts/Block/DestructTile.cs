using System.Collections;
using UnityEngine;
public class DestructTile : MonoBehaviour
{
    //public GameObject particle;
    SpriteRenderer tileSp;
    public static bool tileBreak = false;
    void Start()
    {
        //particle.SetActive(false);
        tileSp = GetComponent<SpriteRenderer>();
        //gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && tileSp.color.a == 1 )//|| collision.gameObject == gameObject.transform.GetChild(1)) //(ColliderDetect.platformDetect == false && collision.gameObject.tag == "Player")
        {
            StartCoroutine(Destruct());
            //Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator Destruct()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        tileBreak = true;
        Debug.Log("파괴");
        if (this.gameObject.tag == "Platform_jam")
        {
            this.gameObject.SendMessage("SpawnJam");

        }


        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.transform.GetChild(0).SendMessage("ParticlePlay");
        tileSp.color = new Color(1, 1, 1, 0);
        yield return null;
    }

}
