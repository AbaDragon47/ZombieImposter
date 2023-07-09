using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treeshake : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("shake",true);
        }
        else
        {
            anim.SetBool("shake", false);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("shake",false);
        }
        else
        {
            anim.SetBool("shake", true);
        }
    }
}
