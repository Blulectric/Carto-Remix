using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanProjectiles : MonoBehaviour
{
    public float timer = 3.0f;
    private float startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + timer)
        {
            Destroy(gameObject);
        }
    }
}
