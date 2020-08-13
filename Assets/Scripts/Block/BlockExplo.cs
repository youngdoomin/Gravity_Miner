using UnityEngine;

public class BlockExplo : MonoBehaviour
{
    private float exploRange = 10;
    public GameObject exploObj;

    private void Start()
    {
        exploObj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && SubGravity.sp == SubGravity.speedLock)
        {
            exploObj.SetActive(true);
            this.gameObject.BroadcastMessage("SizeCon");
            Collider2D[] explo = Physics2D.OverlapCircleAll(gameObject.transform.position, exploRange);
            foreach (var col in explo)
            {

                col.SendMessage("Destruct");
                //col.SendMessage("BrEf");
                col.SendMessage("EnemyExDel"); 

                //Destroy(col.gameObject);
            }
        }
    }
}
