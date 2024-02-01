using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float newPointThreshold = 0.2f;
    Vector2 lastPosition;
    LineRenderer lineRenderer;
    Rigidbody2D rigidbody;
    Vector2 currentPosition;
    public float speed = 1;
    public AnimationCurve landing;
    float timerValue;
    Transform transform;
    public Sprite[] planeSprites = new Sprite[4];

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;

        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        lineRenderer.SetPosition(0, transform.position + new Vector3(Random.Range(-5,5),Random.Range(-5,5),0));
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        speed = Random.Range(1, 3);
        GetComponent <SpriteRenderer >().sprite = planeSprites[(int)Random.Range(0,4)];

    }

    private void FixedUpdate()
    {
        currentPosition = transform .position;
        if(points.Count > 0 )
        {
            Vector2 direction = points[0] - currentPosition;
            float angle = Mathf .Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rigidbody.rotation = -angle; 
        }
        rigidbody.MovePosition(rigidbody.position + (Vector2)transform.up * speed * Time.deltaTime);
    }

    private void Update()
    {
        if(Input .GetKey(KeyCode.Space))
        {
            timerValue += 0.5f * Time.deltaTime;
            float interpolation = landing.Evaluate (timerValue);
            if(transform .localScale .z < 0.1f)
            {
                Destroy(gameObject);
            }
            transform .localScale = Vector3.Lerp(Vector3.one, Vector3.zero, interpolation);
        }


        lineRenderer.SetPosition(0, transform.position);
        if (points.Count > 0)
        {
            if(Vector2.Distance (currentPosition , points[0]) < newPointThreshold)
            {
                points.RemoveAt (0);

                for(int i = 0; i < lineRenderer .positionCount  - 2; i++)
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
                }
                lineRenderer.positionCount--;
            }
        }
    }

    private void OnMouseDown()
    {
        points = new List<Vector2>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Vector2 .Distance (lastPosition , newPosition) > newPointThreshold)
        {
            points.Add(newPosition);
            lineRenderer.positionCount++;
            lineRenderer .SetPosition(lineRenderer.positionCount - 1, newPosition);
            lastPosition = newPosition;
        }
    }
}
