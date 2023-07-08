using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimcontroller : MonoBehaviour
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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        //Debug.Log(horizontalInput);
        //Debug.Log(verticalInput);

        if(horizontalInput < 0 || verticalInput != 0)
        {
           anim.SetBool("isRunning", true);
           anim.SetBool("isRunningRight", false);

         
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isRunningRight", false);
        }

        if(horizontalInput > 0 || verticalInput != 0)
        {
           anim.SetBool("isRunning", false);
           anim.SetBool("isRunningRight", true);

         
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isRunningRight", false);
        }
    }

   
}
