using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    public Transform asteroidPrefab;
    //public List<Transform> asteroids = new List<Transform>();
    public int fieldRadius = 100;
    public int asteroidCount = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        //List<Transform> asteroids = new List<Transform>();
        
        for (int i = 0; i < asteroidCount; i++)
        {
            //asteroids.Add(asteroidPrefabs[i]);
            Transform temp = Instantiate(asteroidPrefab, Random.insideUnitSphere * fieldRadius, Random.rotation);
            temp.localScale = temp.localScale * Random.Range(.5f, 5f);
        }
        
    }
}
