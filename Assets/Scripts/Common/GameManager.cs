using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] pooledObjects;
    private List<GameObject> oldObj = new List<GameObject>();
    public Transform spawnPos;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        GetPooledObject();
    }

    public GameObject GetPooledObject()
    {
        int randomIndex = Random.Range(0, pooledObjects.Length);
        Debug.Log("Index" + randomIndex);
        GameObject go = pooledObjects[randomIndex];
        GameObject[] allChildren = GetComponentsInChildren<GameObject>();
        foreach (GameObject child in allChildren)
        {
            child.SetActive(true);
        }
        go.gameObject.transform.position = new Vector3(transform.position.x, -spawnPos.position.y, transform.position.z);
        go.SetActive(true);
        oldObj.Add(go);

        return null;
    }

    public void Destroyed()
    {
        oldObj[0].SetActive(false);
        oldObj.RemoveAt(0);
        GetPooledObject();
    }
}
