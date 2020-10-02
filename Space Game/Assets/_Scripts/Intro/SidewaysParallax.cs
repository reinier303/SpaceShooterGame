using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysParallax : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += new Vector3(speed,0, 0) * Time.deltaTime;
        //TODO:Fix the jank with non connecting sprites
        /*
        if(transform.position.x <= -30.72f)
        {
            transform.position = new Vector3(30.72f - 0.1f, 0, 0);
        }
        */
    }
}
