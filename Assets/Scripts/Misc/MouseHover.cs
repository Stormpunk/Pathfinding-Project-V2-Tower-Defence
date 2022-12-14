using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MouseHover : Singleton<MouseHover>
{
    private SpriteRenderer sRender;
    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }
    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    public void Activate (Sprite sprite)
    {
        this.sRender.sprite = sprite;
    }
    public void Deactivate()
    {
        this.sRender.sprite = null;
    }
}
