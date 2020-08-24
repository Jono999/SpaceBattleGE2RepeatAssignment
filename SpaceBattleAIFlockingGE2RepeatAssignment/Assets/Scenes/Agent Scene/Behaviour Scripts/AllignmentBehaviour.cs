using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behaviour/Allignment")]
public class AllignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current allignment
        if (context.Count == 0)
            return agent.transform.forward;
        
        //add all points together and average
        Vector3 allignmentMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            allignmentMove += item.transform.forward;
        }
        allignmentMove /= context.Count;
        
        return allignmentMove;
    }
}
