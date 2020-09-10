﻿using System.Collections;
using UnityEngine;

public class SubGravity : MonoBehaviour
{
    public const float speedLock = 40.0f; //최대 속력
    public static float sp;
    public static bool reaction = false;
    public int divide;
    bool dontLoop;

    void Update()
    {
        if (sp > speedLock)
        {
            sp = speedLock;
            if (dontLoop == false)
            {
                SoundManager.instance.PlaySE(SoundManager.instance.fullSpeed);
                dontLoop = true;
            }
        }
        else
        { dontLoop = false; }
    }
    private void FixedUpdate()
    {

        if (!Playercontroller.kill && !DestructTile.tileBreak) //평소 상태
        {
            transform.Translate(Vector2.up * Time.fixedDeltaTime * sp);
            Playercontroller.killLoop = false;
            reaction = false;

            if(sp < PGravity.power)
                sp += Time.fixedDeltaTime * PGravity.power / divide;

            else if (sp < speedLock)
                sp += Time.fixedDeltaTime * sp / divide;


            if (Input.GetKey(KeyCode.W) && Playercontroller.energy >= 0 )// && sp > 0 && sp < speedLock ) || (sp < PGravity.power))
            {
                ParticleEfManager.gravityVal = PGravity.power;
            }
            else if (Input.GetKey(KeyCode.S) && Playercontroller.energy >= 0)// && sp > 0)
            {
                //sp += Time.fixedDeltaTime * PGravity.power / divide;
                ParticleEfManager.gravityVal = -PGravity.power;
            }


        }
        else if (Playercontroller.kill || DestructTile.tileBreak) //적 처치, 블록 파괴 상태
        {
            transform.Translate(Vector2.down * Time.fixedDeltaTime * sp * 10); //반대로 올라감
            sp += 5f * Time.fixedDeltaTime;
            StartCoroutine(WaitReact());
        }


    }
    IEnumerator WaitReact()
    {
        reaction = true;
        yield return new WaitForSeconds(0.35f);
        sp = 1.0f;
        Playercontroller.kill = false; //초기화

        DestructTile.tileBreak = false;
        //Debug.Log("Wait");
    }
}





