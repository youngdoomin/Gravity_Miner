using UnityEngine;

public class BlockExplo : DestructTile
{
    private float exploRange = 10;
    GameObject sphere;
    private void Start()
    {
        sphere = transform.GetChild(1).gameObject;
        sphere.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")// && GameManager.Instance.sp  == GameManager.Instance.speedLock)
        {
            sphere.SetActive(true);
            sphere.GetComponent<Animator>().SetTrigger("Boom");
            Collider2D[] explo = Physics2D.OverlapCircleAll(gameObject.transform.position, exploRange);
            foreach (var col in explo)
            {
                col.SendMessage("EnemyExDel");
                col.SendMessage("Destruct");
            }
            
            if (!sphere.GetComponent<Animation>().isPlaying)
            {
                sphere.SetActive(false);
            }

        }
        else if (coll.gameObject.tag == "Player" && tileSp.color.a == 1)
        {
            StartCoroutine(Destruct());
        }
    }
}
