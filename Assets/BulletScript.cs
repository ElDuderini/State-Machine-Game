using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Tooltip("Speed the bullets move")]
    public float speed = 20;

    private Vector3 start;

    private void Start()
    {
        //get starting position at start
        start = transform.position;
    }

    void Update()
    {
        //move the bullet forward at the given speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //if the bullet goes too far, destroy it
        if(Vector3.Distance(transform.position, start) > 50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //destroy the bullet if an object is hit
        Destroy(gameObject);
    }
}
