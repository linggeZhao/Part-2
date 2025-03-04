using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement;
    public float speed = 3;
    Rigidbody2D rb;
    Animator animator;
    bool clickingOnself = false;
    public float health;
    public float maxHealth = 5;
    bool isDead = false;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        health = PlayerPrefs.GetFloat("KnightHealth", maxHealth);
        SendMessage("SetHealth", health);
        if (health == 0)
        {
            //die?
            isDead = true;
            animator.SetTrigger("Death");
        }

    }

    private void FixedUpdate()
    {
        if(isDead) return;

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
        if (isDead) return;

        if (Input.GetMouseButtonDown(0) && !clickingOnself && !EventSystem.current.IsPointerOverGameObject ())
        {
            destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        }
        animator.SetFloat("Movement", movement.magnitude);

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnMouseDown()
    {
        if (isDead) return;

        clickingOnself  = true;
        SendMessage("TakeDamage", 1);
        
    }
    private void OnMouseUp()
    {
        clickingOnself = false;
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log(damage);
        health -= damage;

        health = Mathf.Clamp(health, 0, maxHealth);

        PlayerPrefs.SetFloat("KnightHealth", health);
        if (health == 0)
        {
            //die?
            isDead = true;
            animator.SetTrigger("Death");
        }
        else
        {
            isDead = false;
            animator.SetTrigger("TakeDamage");
        }
    }


}
