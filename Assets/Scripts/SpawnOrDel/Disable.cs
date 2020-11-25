using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)  // 다른 오브젝트가 나가면
    {
        if (other.gameObject.tag == "SpawnBox_Jam" && GameManager.Instance.jamSpawnCt > 0)
        {
            GameManager.Instance.jamSpawnCt--;
        }
        else if (other.gameObject.tag == "SpawnBox_Item" && GameManager.Instance.itemSpawnCt > 0)
        {
            GameManager.Instance.itemSpawnCt--;
        }
        other.gameObject.SetActive(false);

    }
}
