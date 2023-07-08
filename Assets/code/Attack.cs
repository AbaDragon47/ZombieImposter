using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public float damageAmount = .1f;
    void Start()
    {
        
    }

    public void Hit(GameObject target)
    {
        //maybe make a chance algo?
        // Check if the target has a HealthSystem script attached
        Debug.Log(target.name);
        Damage healthSystem = target.GetComponent<Damage>();
        if (healthSystem != null&&Random.Range(0f, 1f) < 0.1f) 
        {
            healthSystem.TakeDamage(damageAmount);
        }
        else
            Debug.Log("no health system lmao");
    }

    
}
