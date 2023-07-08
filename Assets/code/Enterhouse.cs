using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enterhouse : MonoBehaviour
{
    public GameObject gx;

    // Start is called before the first frame update
    void Start()
    {
        gx.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gx.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gx.SetActive(true);
        }
    }
}
