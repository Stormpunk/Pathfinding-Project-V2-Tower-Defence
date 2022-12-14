using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Mono.Cecil.Cil;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }
    //the position of the tile in the games grid ^
    public bool Walkable { get; set; }
    public bool isEmpty;
    public Vector2 worldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2));
        }
    }
    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        Walkable = true;
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
    }
}
