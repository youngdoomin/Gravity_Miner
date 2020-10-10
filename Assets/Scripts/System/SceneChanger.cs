using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public GameObject[] keepObj;

    private void Awake()
    {
        for (int i = 0; i < keepObj.Length; i++)
        {
            DontDestroyOnLoad(keepObj[i]);
        }
    }
}
