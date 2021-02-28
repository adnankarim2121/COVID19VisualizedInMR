using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyCube : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sphere" )
        {
            Destroy(collision.gameObject);
            Console.Write("Collided!");
        }
    }
}
