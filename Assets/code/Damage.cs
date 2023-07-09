using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;
    public healthbar healthbar;
    public float pain;
   
    public float health,maxHealth=100f;

    void Start()
    {
        health=maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            StartCoroutine(LoadScene());
        }
    }

    public void TakeDamage(float damageAmt)
    {
        StartCoroutine(gameObject.GetComponent<shake>().shaking(3f));
        health -= damageAmt;
        Debug.Log("taking damage");
        healthbar.SetHealth(health);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Zombie")
        {
            health -= pain;
            healthbar.SetHealth(health);
        }
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
