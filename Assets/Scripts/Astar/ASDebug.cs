using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ASDebug : MonoBehaviour
{
    [SerializeField]
    private TileScript start, goal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Astar.GetPath(start.GridPosition);
        }
    }
    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(2))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();   
                if(tmp != null)
                {
                    if (start == null)
                    {
                        start = tmp;
                    }
                    else if (goal == null)
                    {
                        goal = tmp;
                    }
                }
            }
        }
    }
}
