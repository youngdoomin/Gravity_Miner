using UnityEngine;

public class ColliderDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Platform_jam")
        {
            if (GameManager.Instance.sp >= GameManager.Instance.speedLock && GameManager.Instance.untouchable == false && col.gameObject.layer != 11
                && col.gameObject.name != "BombBlock") //낙하 데미지
            { GameManager.Instance.GroundDam = true; }
            else { GameManager.Instance.GroundDam = false; }
            GameManager.Instance.sp = 0;
            SoundManager.instance.PlaySE(SoundManager.instance.blockEf);
        }
    }
}
