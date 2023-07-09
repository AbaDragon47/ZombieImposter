using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFX : MonoBehaviour
{
    private Animator anim;
    public int houselevel;
    public float househealth;
    public float maxHealth= 10;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        houselevel = 1;
        househealth=gameObject.GetComponentInParent<Damage>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "metal")
        {
            houselevel += 1;
        }
        if(houselevel == 2)
        {
            anim.SetBool("level2", true);
            maxHealth = 200;
            gameObject.GetComponent<Damage>().health+=(maxHealth-gameObject.GetComponent<Damage>().health)/2;
            
        }
        else if(houselevel == 3)
        {
            anim.SetBool("level3", true);
            maxHealth = 300;
            gameObject.GetComponent<Damage>().health+=(maxHealth-gameObject.GetComponent<Damage>().health)/2;
            gameObject.GetComponent<Damage>().maxHealth=maxHealth;
        }
        else if(houselevel == 4)
        {
            anim.SetBool("level4", true);
            maxHealth = 400;
            gameObject.GetComponent<Damage>().health+=(maxHealth-gameObject.GetComponent<Damage>().health)/2;
            gameObject.GetComponent<Damage>().maxHealth=maxHealth;
            
        }
        else if(houselevel == 5)
        {
            anim.SetBool("level5", true);
            maxHealth = 500;
            gameObject.GetComponent<Damage>().health+=(maxHealth-gameObject.GetComponent<Damage>().health)/2;
            gameObject.GetComponent<Damage>().maxHealth=maxHealth;
            
        }
        
    }

    /*public void TakeDamage(float damageAmt)
    {
        househealth -= damageAmt;
        if(househealth<=0)
            Destroy(gameObject);
    }*/
}
