using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : MonoBehaviour
{
    public Transform target;
    public float avoidCollision = 4f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) >= avoidCollision)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction * Time.deltaTime);
        }
    }
}
