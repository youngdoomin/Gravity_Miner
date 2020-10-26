using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public GameObject[] keepObj;
    public static SceneChanger instance = null;

    private void Awake()
    {
        if (instance != null && instance != this) //중복되는 오브젝트가 있으면
        {
            foreach (GameObject obj in keepObj) //부속 오브젝트 삭제
                Destroy(obj);

            Destroy(this.gameObject); //자기자신 삭제
            return;
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
            for (int i = 0; i < keepObj.Length; i++)
            {
                DontDestroyOnLoad(keepObj[i]);
            }
        }
    }
}
