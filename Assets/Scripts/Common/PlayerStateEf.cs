using System.Collections;
using UnityEngine;

public class PlayerStateEf : MonoBehaviour
{       
    public Sprite ShieldEf;
    SpriteRenderer PState;

    void Start()
    {
        PState = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (Block_special.shieldOn == true)
        {
            PState.sprite = ShieldEf;
            StartCoroutine(SparkEffect());
        }
        else
        {
            PState.sprite = null;
        }
    }
    IEnumerator SparkEffect()
    {
        float trans = (Time.time % 2);
        yield return new WaitForSeconds(1f);

        PState.color = new Color(1, 1, 1, trans);
        
    }
}
