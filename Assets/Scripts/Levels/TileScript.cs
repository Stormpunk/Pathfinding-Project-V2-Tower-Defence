using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Mono.Cecil.Cil;
using Unity.VisualScripting;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }
    //the position of the tile in the games grid ^
    public bool Walkable;
    public bool isEmpty;
    public bool isWithinTowerRange = false;
    public Vector2 worldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }
    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        if (this.gameObject.CompareTag("Path"))
        {
            Walkable = true;
        }
        else
        {
            Walkable = false;
        }
        isEmpty = true;
        transform.SetParent(parent);
        transform.position = worldPos;
        this.GridPosition = gridPos;
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }
    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.clickedButt != null)
        {
            if (Input.GetMouseButtonDown(0) && isEmpty == true)
            {
                PlaceTower();
                isEmpty = false;
                Walkable = false;
            }
        }
        if (Input.GetMouseButton(1))
        {
            GameManager.Instance.DropTower();
        }
        //Debug.Log(GridPosition.X + ", " + GridPosition.Y);
    }

    private void PlaceTower()
    {
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.clickedButt.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);
        GameManager.Instance.BuyTower();
        //Spawns a tower and parents it to the tile it's being placed on
        float towerRadius = tower.GetComponent<TowerScript>().TowerRange;
        //this should be the radius used for calculating which tiles are in a tower's path but it doesn't work, not sure why. Will fix if I can figure it out but it works if I punch in a number so fuck it.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tower.transform.position, 2);
        //for whatever reason towerRadius doesn't actually work, fudging it for now.
        //uses the tile the tower is being placed on and draws an overlapcircle around it
        foreach (Collider2D collider in colliders)
        {
            //if the overlapcircle finds any colliders (Read: Tiles) it will set their towerRange bool to true
            TileScript tile = collider.GetComponent<TileScript>();
            if (tile != null)
            {
                tile.isWithinTowerRange = true;
            }
        }
    }
}
