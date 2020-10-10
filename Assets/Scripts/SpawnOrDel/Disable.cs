using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)  // 다른 오브젝트가 나가면
    {
        other.gameObject.SetActive(false);
    }
}
