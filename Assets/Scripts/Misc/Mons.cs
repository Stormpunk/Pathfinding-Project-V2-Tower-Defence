using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mons : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Stack<Node> path;

    public Point GridPosition { get;  set; }
    private Vector3 destination;


    private void Update()
    {
        Walk();
    }
    public void Spawn()
    {
        transform.position = LevelManager.Instance.StartPortal.transform.position;

        SetPath(LevelManager.Instance.FinalPath);
    }
    private void Walk()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (transform.position == destination)
        {
            if(path != null && path.Count > 0)
            {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().worldPosition;
            }
        }
    }
    private void SetPath(Stack<Node> newPath)
    {
        if(newPath != null)
        {
            path = newPath;
            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().worldPosition; 
        }
    }
}
