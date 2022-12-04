using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    public GameObject GetObject(string type)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].name == type)
            {
                GameObject newObject = Instantiate(prefabs[i]);
                newObject.name = type;
                return newObject;
            }
        }
        return null;
    }
}
