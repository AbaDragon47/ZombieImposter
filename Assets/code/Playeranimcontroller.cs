using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimcontroller : MonoBehaviour
{
    private Animator anim;
    public GameObject audiomaker;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audiomaker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        //Debug.Log(horizontalInput);
        //Debug.Log(verticalInput);

        if(horizontalInput != 0 || verticalInput != 0)
        {
            audiomaker.SetActive(true);

            if(horizontalInput == -1 || verticalInput != 0)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isRunningRight", false);
            
            }
            else if(horizontalInput == 1 || verticalInput != 0)
            {
                anim.SetBool("isRunningRight", true);
                anim.SetBool("isRunning", false);
            }
            
           

         
        }
        else
        {
            anim.SetBool("isRunningRight", false);
            anim.SetBool("isRunning", false);
            audiomaker.SetActive(false);
        }
     
       
    }

   
}
