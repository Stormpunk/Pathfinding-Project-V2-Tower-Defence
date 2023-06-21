using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int gold;
    public TowerButt clickedButt { get; private set; }
    public TextMeshProUGUI currencyText;
    public ObjectPooler myPool { set; get; }

    private void Awake()
    {
        myPool = GetComponent<ObjectPooler>();  
    }
    // Start is called before the first frame update
    void Start()
    {
        gold = 500;
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = "Current Gold: " + gold.ToString();
    }
    public void PickTower(TowerButt towerButt)
    {
        this.clickedButt = towerButt;
        if(gold >= this.clickedButt.cost)
        {
            gold -= this.clickedButt.cost;
            this.clickedButt = towerButt;
            MouseHover.Instance.Activate(towerButt.Sprite);
        }
        else
        {
            Debug.Log("No Money");
            DropTower();
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
    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        int monIndex = Random.Range(0, 3);
        string type = string.Empty;

        switch (monIndex)
        {
            case 0:
                type = "PinkMon";
                break;
            case 1:
                type = "OwlMon";
                break;
            case 2:
                type = "DudeMon";
                break;
        }
      Mons mons =  myPool.GetObject(type).GetComponent<Mons>();
        mons.Spawn();
        yield return new WaitForSeconds(2.5f);
    }
}
