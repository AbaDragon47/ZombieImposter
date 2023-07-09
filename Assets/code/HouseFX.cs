using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFX : MonoBehaviour
{
    private Animator anim;
    public float houselevel;
    public float househealth;
    public float maxHealth= 100;

    
    string[] levels;
    int nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        nextLevel=-1;
        levels= new string[]{"level2","level3","level4","level5"};
        anim = GetComponent<Animator>();
        houselevel = 1f;
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
            houselevel += .5f;
            Set();
        }
        if(other.tag=="wood")
        {
            houselevel += .25f;
            Set();
        }
        
    }
    public void Set()
    {
        if(houselevel%1==0)
        {
            nextLevel+=nextLevel==4?0:1;
            anim.SetBool(levels[nextLevel], true);
            maxHealth = houselevel*1000;
            gameObject.GetComponentInParent<Damage>().health+=(maxHealth-gameObject.GetComponentInParent<Damage>().health)/2;
            
        }
        
    }
    

    /*public void TakeDamage(float damageAmt)
    {
        househealth -= damageAmt;
        if(househealth<=0)
            Destroy(gameObject);
    }*/
}
