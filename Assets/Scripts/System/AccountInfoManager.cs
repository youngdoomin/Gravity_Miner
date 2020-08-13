using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfoManager : MonoBehaviour
{
    public static AccountInfoManager account;
    public AccountInfo info;
    private void Awake()
    {

        if (account != null && account != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            account = this;
        }

        DontDestroyOnLoad(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
