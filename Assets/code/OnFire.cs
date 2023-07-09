using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFire : MonoBehaviour
{
    public GameObject fire;
    public float burntime;

    // Start is called before the first frame update
    void Start()
    {
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "fire")
        {
            fire.SetActive(true);
            Invoke("Burn", burntime);

        }
    }

    void Burn()
    {
        Destroy(gameObject);
    }
}
