using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    bool selected;
    SpriteRenderer baseRenderer;
    public Color selectedColor;
    public Color unselectedColor;
    Rigidbody2D rb;
    public float speed = 1000;

    // Start is called before the first frame update
    void Start()
    {
        baseRenderer = GetComponent<SpriteRenderer>();
        Selected(false);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    public void Selected(bool selected)
    {
        if (selected)
        {
            baseRenderer.color = selectedColor;
        }
        else
        {
            baseRenderer.color = unselectedColor;
        }
    }
    private void OnMouseDown()
    {
        Controller.SetSelectedPlayer(this);
    }

    public void Move(Vector2 direction)
    {
        rb.AddForce (direction * speed);
    }
}
