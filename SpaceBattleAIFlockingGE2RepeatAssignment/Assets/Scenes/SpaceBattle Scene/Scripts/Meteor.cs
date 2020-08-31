using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    
   // private Vector3 rotateVector = new Vector3(0, 0, 100f);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateObj();
        //transform.Rotate (new Vector3(0, 20f, 0) * Time.deltaTime);
        //transform.RotateAround(this.transform.parent.position, this.transform.up, 90f * Time.deltaTime);
        //this.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), 90f * Time.deltaTime);
        transform.Rotate(new Vector3(Time.deltaTime * 10f, Time.deltaTime * 15f,Time.deltaTime * 20f));
    }
    
//    private void RotateObj()
//    {
//        transform.Rotate(rotateVector);
//    }
}
