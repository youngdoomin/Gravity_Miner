using System.Collections;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    public float range = 6;
    public float delay;
    private float i = 0;

    void Update()
    {
        this.gameObject.transform.localScale = new Vector3(i, i, i);
    }

    IEnumerator SizeCon()
    {
        while(i <= range)
        {
            i += 2f;
            yield return new WaitForSeconds(delay);
        }
        gameObject.SetActive(false);
        i = 0;
    }
}
