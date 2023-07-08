using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFX : MonoBehaviour
{
    private Animator anim;
    public int houselevel;
    public int househealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        houselevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(houselevel == 2)
        {
            anim.SetBool("level2", true);
            househealth = 20;
        }
        else if(houselevel == 3)
        {
            anim.SetBool("level3", true);
            househealth = 30;
        }
        else if(houselevel == 4)
        {
            anim.SetBool("level4", true);
            househealth = 40;
        }
        else if(houselevel == 5)
        {
            anim.SetBool("level5", true);
            househealth = 50;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "metal")
        {
            houselevel += 1;
        }
        
    }
}
