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
    public float blood;
    public float maxBlood = 1;
    bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

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

        if (movement.magnitude < 0.1)
        {
            isMoving = false;
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BlueBall"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("RedBall"))
        {
            SendMessage("TakeDamage", 1);
            GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedBall"))
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void TakeDamage(float damage)
    {
        blood -= damage;

        blood = Mathf.Clamp(blood, 0, maxBlood);

        PlayerPrefs.SetFloat("PlayerBlood", blood);
        if (blood == 0)
        {
            isDead = true;
            GetComponent<SpriteRenderer>().color = Color.black;
            isMoving = false;
        }
    }
}
