using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject planePrefab;
    private float timer;
    private float timerTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerTarget)
        {
            Instantiate(planePrefab);
            timer = 0;
            timerTarget = Random.Range(1, 5);
        }
    }
}
