using UnityEngine;

public class BlockExplo : MonoBehaviour
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
                col.SendMessage("Destruct");
                col.SendMessage("EnemyExDel");
            }
        }
    }
}
