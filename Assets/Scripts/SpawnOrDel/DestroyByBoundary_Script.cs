using UnityEngine;

public class DestroyByBoundary_Script : MonoBehaviour 
{
    
    //Called when the Trigger Exit
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && EnemySpawner.EnemyCt < 5)
        this.gameObject.BroadcastMessage("EnemySpawn", col.gameObject.transform.position.y);
            //EnemySpawner.EnemySpawn(col.gameObject.transform.position.y);
        if (col.gameObject.tag == "TileMap")// && TilemapSpawner.tileCt < 4)
            this.gameObject.BroadcastMessage("AfterTilemapSpawn", col.gameObject.transform.position.y + 10);
    }
    void OnTriggerExit2D(Collider2D other)  // 다른 오브젝트가 나가면
    {
        if (other.gameObject.tag == "TileMap")
        {
            TilemapSpawner.tileCt--;
            //Destroy(other.gameObject); // 다른 오브젝트 파괴
            GameManager.Instance.Destroyed();
        }  
	}

}
