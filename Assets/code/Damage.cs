using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    public float health,maxHealth=100f;

    void Start()
    {
        health=maxHealth;
    }

    public void TakeDamage(float damageAmt)
    {
        StartCoroutine(gameObject.GetComponent<shake>().shaking(3f));
        health -= damageAmt;
        if(health<=0)
            Destroy(gameObject);
    }
}
