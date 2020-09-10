using UnityEngine;

public class BlockExplo : DestructTile
{
    private float exploRange = 10;

    private void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && SubGravity.sp == SubGravity.speedLock)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.BroadcastMessage("SizeCon");

            Collider2D[] explo = Physics2D.OverlapCircleAll(gameObject.transform.position, exploRange);
            foreach (var col in explo)
            {
                col.SendMessage("EnemyExDel");
                col.SendMessage("Destruct");
            }
        }
        else if (coll.gameObject.tag == "Player" && tileSp.color.a == 1)
        {
            StartCoroutine(Destruct());
        }
    }
}
