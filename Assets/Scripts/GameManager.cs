using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int gold;
    public TowerButt clickedButt { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        gold = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickTower(TowerButt towerButt)
    {
        this.clickedButt = towerButt;
        gold -= this.clickedButt.cost;
        if(gold < 0)
        {
            Debug.Log("No Money");
            DropTower();
            gold = 0;
        }
        else
        {
            this.clickedButt = towerButt;
            MouseHover.Instance.Activate(towerButt.Sprite);
        }
    }
    public void BuyTower()
    {
        clickedButt = null;
        MouseHover.Instance.Deactivate();
    }
    public void DropTower()
    {
        this.clickedButt = null;
        MouseHover.Instance.Deactivate();
    }
}
