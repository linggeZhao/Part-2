using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement;
    public float speed = 5;
    bool isMoving = false;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        if (isMoving)
        {
            MovePlayer();
        }

        animator.SetFloat("Move", movement.magnitude);
    }

    void MovePlayer()
    {
        movement = destination - (Vector2)transform.position;

        if (movement.magnitude < 0.1f)
        {
            isMoving = false;
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }
}
