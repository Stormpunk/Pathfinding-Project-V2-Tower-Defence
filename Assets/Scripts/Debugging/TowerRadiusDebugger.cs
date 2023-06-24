using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRadiusDebugger : MonoBehaviour
{
    public BaseTower baseTower;
    public GameObject rangeVisual;
    // Start is called before the first frame update
    void Start()
    {
        if(baseTower == null)
        {
            Debug.LogError("No BaseTower Assigned!");
            return;
        }
        float radius = baseTower.towerRange;
        if(rangeVisual != null)
        {
            rangeVisual.transform.localScale = new Vector3(radius * 2, 0.01f, radius * 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
