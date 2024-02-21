using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 4;
    public float leftBound = -5;
    public float rightBound = 5;
    Vector2 direction = new Vector2(1, 0);

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();
    }

    void MoveObstacle()
    {
        rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction.x *= -1;
    }


}
