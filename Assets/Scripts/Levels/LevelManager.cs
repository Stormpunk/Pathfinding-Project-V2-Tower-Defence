using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    #region Sprites and GameObjects
    [SerializeField]
    private GameObject[] tilePrefabs;
    private Point startPortal, endPortal;
    [SerializeField]
    private GameObject startPort;
    [SerializeField]
    private GameObject endPort;
    [SerializeField]
    private Transform map;

    public Portal StartPortal;
    public Portal EndPortal;    

    #endregion

    [SerializeField]
    private CameraMove cameraMove;
    private Point mapSize;
    private Stack<Node> finalPath;
    public Stack<Node> FinalPath
    {
        get
        {
            if(finalPath == null)
            {
                GeneratePath();
            }
            return new Stack<Node>(new Stack<Node>(finalPath));
        }
    }
    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float Tilesize
    {
        get {return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
        //Ensures that every time the game is loaded the level is built
    }
    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        string[] mapData = ReadLevelText();
        mapSize = new Point(mapData[0].ToCharArray().Length, mapData.Length);
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;
        Vector3 maxTile = Vector3.zero;
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        //figures out how big the tiles are and makes it so that they can be placed in the correct position
        float tileSize = tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(),x,y, worldStart);

            }
        }
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;
        cameraMove.SetLimits(new Vector3(maxTile.x + tileSize, maxTile.y - Tilesize));
        SpawnPortal();

    }
    //once this is all done it will create a full level
    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (Tilesize * x), worldStart.y - (Tilesize * y),0), map);

    }
    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level2") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split('-');
    }
    private void SpawnPortal()
    {
        startPortal = new Point(0, 4);
        GameObject tmp =  Instantiate(startPort, Tiles[startPortal].GetComponent<TileScript>().worldPosition, Quaternion.identity);
        StartPortal = tmp.GetComponent<Portal>();
        StartPortal.name = "Start Portal";
        endPortal = new Point(19, 7);
        Instantiate(endPort, Tiles[endPortal].GetComponent<TileScript>().worldPosition, Quaternion.identity);
    }
    public bool InBounds(Point pos)
    {
        return pos.X >= 0 && pos.Y >= 0 && pos.X < mapSize.X && pos.Y < mapSize.Y;
    }
    public void GeneratePath()
    {
        finalPath = Astar.GetPath(startPortal, endPortal);
    }
}
