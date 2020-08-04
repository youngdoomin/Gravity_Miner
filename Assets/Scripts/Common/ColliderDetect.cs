using UnityEngine;

public class ColliderDetect : MonoBehaviour
{
    //public static bool platformDetect = true;  // 바닥과 닿을시 감지하는 변수
    //public static bool waited = false;  // 딜레이 주는 변수
    //public static bool fallWaited = false;
    //int i;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Platform_jam")
        {

            //platformDetect = false;
            if ((SubGravity.sp >= SubGravity.speedLock && Playercontroller.untouchable == false) && collision.gameObject.layer != 11) //낙하 데미지
            {
                if (collision.gameObject.name == "BombBlock")
                { Playercontroller.GroundDam = false; }
                else
                    Playercontroller.GroundDam = true;

            }
            SubGravity.sp = Gravity.k;
            SoundManager.instance.PlaySE(SoundManager.instance.blockEf);
            /*
            SubGravity.sp = Gravity.k;
            waited = false;
            if(fallWaited == false)
            StartCoroutine(FallWait());
            */

        }

    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        platformDetect = true;
        fallWaited = false;
        if(platformDetect == true)
        {
            SubGravity.sp = Gravity.l;
        }
    }*/
    /*
    IEnumerator FallWait()
    {

        for (i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        SubGravity.sp = Gravity.l;
        fallWaited = true;
        

    }*/
}
