using UnityEngine;

public class ColliderDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Platform_jam")
        {
            if ((SubGravity.sp >= SubGravity.speedLock && Playercontroller.untouchable == false) && col.gameObject.layer != 11
                && col.gameObject.name != "BombBlock") //낙하 데미지
            { Playercontroller.GroundDam = true; }
            else { Playercontroller.GroundDam = false; }
            SubGravity.sp = 0;
            SoundManager.instance.PlaySE(SoundManager.instance.blockEf);
        }
    }
}
