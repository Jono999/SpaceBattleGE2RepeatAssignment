using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Transform agentPrefab;
    public int nAgents;
    public List<Agent> agents;
    
    public List<Predator> predators;

    public float bound;
    public float spawnRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        agents = new List<Agent>();
        Spawn(agentPrefab,nAgents);
        
        agents.AddRange(FindObjectsOfType<Agent>());
        predators.AddRange(FindObjectsOfType<Predator>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(Transform prefab, int n)
    {
        for (int i = 0; i < n; i++)
        {
            var obj = Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), 
                                 0, Random.Range(-spawnRadius, spawnRadius)),
                                  Quaternion.identity);
        }
    }

    public List<Agent> getNeighbours(Agent agent, float radius)
    {
        //return null;
        List<Agent> r = new List<Agent>();
        foreach (var otherAgent in agents)
        {
            if (otherAgent == agent)
                continue;

                if (Vector3.Distance(agent.position, otherAgent.position) <= radius)
                {
                    r.Add(otherAgent);
                }
        }
        
        return r;
    }
    
    public List<Predator> getPredators(Agent agent, float radius)
    {
        //return null;
        List<Predator> r = new List<Predator>();
        
        foreach (var predator in predators)
        {
            if (Vector3.Distance(agent.position, predator.position) <= radius)
            {
                r.Add(predator);
            }
        }
        return r;
    }
    
}
