using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButt : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Sprite sprite;
    public int cost;

    public GameObject TowerPrefab
    {
        get { return towerPrefab; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
    }
}
