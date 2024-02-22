using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GoalkeeperController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 2;
    Vector2 position;
    public float maxDistance = 3;


    private void FixedUpdate()
    {
        if (Controller.SelectedPlayer == null)
        {
            return;
        }
        position = ((Vector2)transform.position + (Vector2)Controller.SelectedPlayer.transform.position)/2;
        Vector2 distance = (Vector2)transform.position - position;
        if (distance.magnitude > maxDistance)
        {
            position = (Vector2)transform.position + maxDistance * -distance.normalized;
        }
        rb.MovePosition(Vector2.MoveTowards(rb.position, position, speed * Time.deltaTime));
    }
}
