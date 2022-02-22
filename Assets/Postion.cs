using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Postion : MonoBehaviour
{
    GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            g = GameObject.FindGameObjectWithTag("Player");
            transform.position = new Vector3(g.transform.position.x, 4.44f, g.transform.position.z);

        }
    }
}
