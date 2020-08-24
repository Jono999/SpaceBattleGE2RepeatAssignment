using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentConfig : MonoBehaviour
{
    public float Rc;
    public float Rs;
    public float Ra;
    public float Ravoid;

    public float Kc;
    public float Ks;
    public float Ka;
    public float Kw;
    public float Kavoid;

    public float maxFieldOfViewAngle = 180;

    public float WanderJitter;
    public float WanderRadius;
    public float WanderDistance;

    public float maxAcceleration;
    public float maxVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
