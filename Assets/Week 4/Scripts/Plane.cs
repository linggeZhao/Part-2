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

    private bool isInDangerZone = false;
    public float dangerZoneRadius = 2.0f;
    public float tooCloseDistance = 0.5f;

    public bool isLanding = false;


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
        Vector3 planePosition = transform.position;

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(planePosition);

        bool isOutsideScreen = screenPoint.x < 0 || screenPoint.y < 0 || screenPoint.x > 1 || screenPoint.y > 1;

        if (isOutsideScreen)
        {
            Destroy(gameObject);
        }

        if (isLanding)
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, dangerZoneRadius);

        bool foundInDangerZone = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                foundInDangerZone = true;
                DestroyIfTooClose(collider);
            }
        }

        if (foundInDangerZone != isInDangerZone)
        {
            isInDangerZone = foundInDangerZone;

            if (isInDangerZone)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
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
    void DestroyIfTooClose(Collider2D other)
    {
        Vector3 currentPosition3D = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 otherPosition3D = new Vector3(other.transform.position.x, other.transform.position.y, 0);

        float distance = Vector3.Distance(currentPosition3D, otherPosition3D);
        if (distance < tooCloseDistance)
        {
            Destroy(gameObject);
        }
    }
}
