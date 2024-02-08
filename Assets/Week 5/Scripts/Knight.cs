using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement;
    public float speed = 3;
    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        movement = destination - (Vector2)transform.position;

        if(movement .magnitude < 0.1)
        {
            movement = Vector2.zero;
        }

        rb.MovePosition (rb.position + movement.normalized * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        }
        animator.SetFloat("Movement", movement.magnitude);
    }
}
