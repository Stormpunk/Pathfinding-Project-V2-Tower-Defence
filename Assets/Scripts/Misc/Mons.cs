using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Mons : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Stack<Node> path;

    public Point GridPosition { get;  set; }
    private Vector3 destination;
    [SerializeField]
    private GameObject gameManager;
    #region Health and Damage
    [SerializeField]
    private int damage;
    public int health;
    private int bounty;
    #endregion

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Update()
    {
        Walk();
        if(health <= 0)
        {
            Destroy(this.gameObject);
            gameManager.GetComponent<GameManager>().AddGold(bounty);
        }
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
    public void DamageMe(int damage)
    {
        health -= damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndPortal"))
        {
            gameManager.GetComponent<GameManager>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
