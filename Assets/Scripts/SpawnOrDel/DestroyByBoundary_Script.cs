using UnityEngine;

public class DestroyByBoundary_Script : MonoBehaviour 
{
    
    //Called when the Trigger Exit
    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && EnemySpawner.EnemyCt < 5)
        this.gameObject.BroadcastMessage("EnemySpawn", col.gameObject.transform.position.y);
            //EnemySpawner.EnemySpawn(col.gameObject.transform.position.y);
        if (col.gameObject.tag == "TileMap")// && TilemapSpawner.tileCt < 4)
            this.gameObject.BroadcastMessage("AfterTilemapSpawn", col.gameObject.transform.position.y + 10);
    }
    */
    void OnTriggerExit2D(Collider2D other)  // 다른 오브젝트가 나가면
    {
        
        if (other.gameObject.tag == "TileMap")
        {
            GameManager.Instance.TileDestroy();
            //other.gameObject.SetActive(false);
        }  
        else if(other.gameObject.tag == "SpawnBox_Enemy")
        {
            other.transform.parent.gameObject.SetActive(false);
            GameManager.Instance.ObjDestroy("enemy");
        }
        else if (other.gameObject.tag == "SpawnBox_Jam")
        {

            GameManager.Instance.jamSpawnCt = 0;
            for (int i = 0; i < other.transform.parent.childCount; i++)
            {
                if (GameManager.Instance.jamSpawnPos.GetChild(i).gameObject.activeInHierarchy)
                { GameManager.Instance.jamSpawnCt++; }

            }

            other.transform.parent.gameObject.transform.GetChild(other.transform.parent.transform.childCount - 2).gameObject.SetActive(false);
            other.transform.parent.gameObject.SetActive(false);

            if(GameManager.Instance.jamSpawnCt < 3)
            {
                GameManager.Instance.ObjDestroy("jam");

            }

        }
        else if (other.gameObject.tag == "SpawnBox_Item")
        {
            GameManager.Instance.itemSpawnCt = 0;
            for (int i = 0; i < other.transform.parent.childCount; i++)
            {
                if (GameManager.Instance.itemSpawnPos.GetChild(i).gameObject.activeInHierarchy)
                { GameManager.Instance.itemSpawnCt++; }

            }
            other.transform.parent.gameObject.transform.GetChild(other.transform.parent.transform.childCount - 2).gameObject.SetActive(false);
            other.transform.parent.gameObject.SetActive(false);
            if (GameManager.Instance.itemSpawnCt < 3)
            {
                GameManager.Instance.ObjDestroy("item");
            }
        }

        Debug.Log(GameManager.Instance.itemSpawnCt);
    }

}
