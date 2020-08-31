using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    private Flock agentFlock;

    public Flock AgentFlock 
    {
        get
        {return agentFlock;} 
        
    }

    private Collider agentCollider;

    public Collider AgentCollider
    {
        get
        {return agentCollider;} 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialise(Flock flock)
    {
        agentFlock = flock;
    }
    public void Move(Vector3 velocity)
    {
        if (transform.forward != Vector3.zero)
        {
            transform.forward = velocity;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position;
        }
//        transform.forward = velocity;
//        transform.position += velocity * Time.deltaTime;
    }
    
    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 velocity = transform.forward;
        float maxSpeed = 5f;
        
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }
}
