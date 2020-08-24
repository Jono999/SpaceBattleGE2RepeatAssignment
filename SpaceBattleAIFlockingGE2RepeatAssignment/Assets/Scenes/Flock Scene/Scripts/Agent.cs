using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public World world;
    public AgentConfig configuration;
    
    // Start is called before the first frame update
    void Start()
    {
        world = FindObjectOfType<World>();
        configuration = FindObjectOfType<AgentConfig>();
        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), 0,Random.Range(-3, 3));
        
        //debugWanderCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime;

        acceleration = Combine();//allignment();//separation();//cohesion();//
        acceleration = Vector3.ClampMagnitude(acceleration, configuration.maxAcceleration);
        
        velocity = velocity + acceleration * t;
        velocity = Vector3.ClampMagnitude(velocity, configuration.maxVelocity);
        
        position = position + velocity * t;
        
        wrapAround(ref position, -world.bound, world.bound);
        
        transform.position = position;
        
        if (velocity.magnitude > 0)
            transform.LookAt(position + velocity);
        
    }

    Vector3 cohesion()
    {
        // cohesion behaviour
        Vector3 r = new Vector3();
        int countAgents = 0;
        //get all nearby neighbours inside radius rc of this agent
        var neighbours = world.getNeighbours(this, configuration.Rc);
        //no nearby neighbours no desire for cohesion
        if (neighbours.Count == 0)
        {
            return r;
        }
        //find the centre mass of all neighbours
        foreach (var agent in neighbours)
        {
            if (isInFieldOfView(agent.position))
            {
               r += agent.position;
               countAgents++;
            }
           
        }

        if (countAgents == 0)
            return r;
        
        r /= countAgents;
        //a vector from our position towards Centre of Mass r
        r = r - this.position;
        r = Vector3.Normalize(r);
        return r;
    }
    
    Vector3 separation()
    {
        //separation behaviour
        //steer in the opposite direction from each nearby neighbour
        Vector3 r = new Vector3();
        //get all nearby neighbours
        var neighbours = world.getNeighbours(this, configuration.Rs);
        // no nearby neighbours no separation desire 
        if (neighbours.Count == 0)
        {
            return r;
        }
        //add the contribution of each neighbour towards me
        foreach (var agent in neighbours)
        {
            if (isInFieldOfView(agent.position))
            {
                Vector3 towardsMe = this.position - agent.position;
                //force contribution will vary inversely proportional to distance
                if (towardsMe.magnitude > 0)
                {
                    r += towardsMe.normalized / towardsMe.magnitude;
                } 
            }
           
        }
        return r.normalized;
    }
    
    Vector3 allignment()
    {
        //allignment behaviour
        //steer agent to match direction and speed of neighbours
        Vector3 r = new Vector3();
        //get all neighbours
        var neighbours = world.getNeighbours(this, configuration.Ra);
        //no neighbours means no one to align to
        if (neighbours.Count == 0)
        {
            return r;
        }
        //match direction and speed == match velocity
        //do this for all neighbours
        foreach (var agent in neighbours)
            if (isInFieldOfView(agent.position))
            r += agent.velocity;
        
            return r.normalized;
    }
    
    virtual protected Vector3 Combine()
    {
        //combine all desired behaviours with weighted sum
        Vector3 r =   configuration.Kc * cohesion()
                    + configuration.Ks * separation()
                    + configuration.Ka * allignment()
                    + configuration.Kw * wander()
                    + configuration.Kavoid * avoidEnemy();
        return r;
    }

    float wrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
            value = min;
        else if (value < min)
            value = max;
        return value;
    }

    void wrapAround(ref Vector3 v, float min, float max)
    {
        v.x = wrapAroundFloat(v.x, min, max);
        v.y = wrapAroundFloat(v.y, min, max);
        v.z = wrapAroundFloat(v.z, min, max);
    }

    bool isInFieldOfView(Vector3 objectToCheck)
    {
        return Vector3.Angle(this.velocity, objectToCheck - this.position) <= configuration.maxFieldOfViewAngle;
    }

    private Vector3 wanderTarget;
   // private GameObject debugWanderCube;

    protected Vector3 wander()
    {
        // wander steer behaviour that looks purposeful
        float jitter = configuration.WanderJitter * Time.deltaTime;
        
        // add a small random vector to the targets position
        wanderTarget += new Vector3(RandomBinomial() * jitter, 0, RandomBinomial() * jitter);

        // reproject the vector back to unit circle
        wanderTarget = wanderTarget.normalized;

        // increase length to same as radius of wander circle
        wanderTarget *= configuration.WanderRadius;

        //position the target in front of agent
        Vector3 targetInLocalSpace = wanderTarget + new Vector3(0, 0, configuration.WanderDistance);

        //project the target from local to world space
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);

        //debugWanderCube.transform.position = targetInWorldSpace;

        //steer towards it
        targetInWorldSpace -= this.position;

        return targetInWorldSpace.normalized;
    }
    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    Vector3 avoidEnemy()
    {
        //flee
        Vector3 r = new Vector3();
        var enemies = world.getPredators(this, configuration.Ravoid);
        if (enemies.Count == 0)
            return r;

        foreach (var enemy in enemies)
        {
            r += flee(enemy.position);
        }

        return r.normalized;
    }

    Vector3 flee(Vector3 target)
    {
        //run from target
        Vector3 desiredVelocity = (position - target).normalized * configuration.maxVelocity;
        
        //steer velocity in flee direction
        return desiredVelocity - velocity;
    }
}
