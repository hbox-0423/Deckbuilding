using System.Collections.Generic;
using UnityEngine;
public class Pooling : MonoBehaviour
{
    public GameObject Prefab;
    private List<GameObject> pool = new List<GameObject>();

    public GameObject Get()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject newObj = Instantiate(Prefab);
        pool.Add(newObj);
        return newObj;
    }
}
