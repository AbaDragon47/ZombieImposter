using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "house")
        {
            Destroy(gameObject);
        }
    }

}
