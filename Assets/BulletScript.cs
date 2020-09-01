using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float velocity;
    private Vector3 start;

    private void Start()
    {
        start = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        if(Vector3.Distance(transform.position, start) > 50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
