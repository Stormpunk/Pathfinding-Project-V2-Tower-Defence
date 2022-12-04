using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugTiles : MonoBehaviour
{
    [SerializeField]
    private Text f;
    [SerializeField]
    private Text g;
    [SerializeField]
    private Text h;

    public Text F
    {
        get { return f; }
        set { this.f = value; }
    }
    public Text G
    {
        get { return g; }
        set { this.g = value; }
    }
    public Text H
    {
        get { return h; }
        set { this.h = value; }
    }
}

