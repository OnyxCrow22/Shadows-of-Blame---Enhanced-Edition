using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool oPInstance;

    private List<GameObject> pooledObjs = new List<GameObject>();
    public int amountToPool;

    [SerializeField] private GameObject targetObj;

    void Awake()
    {
        oPInstance = this;   
    }

    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject newObj = Instantiate(targetObj);
            newObj.SetActive(false);
            pooledObjs.Add(newObj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjs.Count; i++)
        {
            if (!pooledObjs[i].activeInHierarchy)
            {
                return pooledObjs[i];
            }
        }
        return null;
    }
}
