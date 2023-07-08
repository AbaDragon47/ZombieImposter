using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zom_movement : MonoBehaviour
{
    // Start is called before the first frame update
    //npc stuff
    public float minPauseDuration = 1f;  
    public float maxPauseDuration = 3f; 
    private float currentPauseDuration;  
    private float pauseTimer; 
    private bool isPaused;

    public bool isPlayer;
    public Rigidbody2D zrb;
    public Transform target;
    public float moveSpeed = 3.5f;

    void Start()
    {
        isPlayer = false;
        target=GameObject.FindWithTag("house").transform;
        zrb= GetComponent<Rigidbody2D>();
        isPaused=false;
        pauseTimer=0f;
        currentPauseDuration=0;
    }
    void ranMove(){
        if (Random.Range(0f, 1f) < 0.05f)
            GetComponent<Rigidbody2D>().velocity= Vector2.left*moveSpeed;
        else
           GetComponent<Rigidbody2D>().velocity= Vector2.right*moveSpeed;
    }
    // Update is called once per frame
    void npcMovement(){
        
        if (isPaused)
        {
            // Zombie NPC is currently paused
            pauseTimer += Time.deltaTime;

            if (pauseTimer >= currentPauseDuration)
            {
                // Pause duration is over, resume movement
                isPaused = false;
                pauseTimer = 0f;
                currentPauseDuration = Random.Range(minPauseDuration, maxPauseDuration);
            }
        }
        else
        {
            Vector2 direction = (Vector2)target.position- (Vector2)GetComponent<Rigidbody2D>().position;
            direction.Normalize();

            GetComponent<Rigidbody2D>().velocity = direction * moveSpeed;
            //ranMove();
            // Check if the zombie NPC should pause
            if (Random.Range(0f, 1f) < 0.01f)  // Adjust the probability as needed
            {
                isPaused = true;
                pauseTimer = 0f;
                currentPauseDuration = Random.Range(minPauseDuration, maxPauseDuration);

                // Stop the zombie NPC's movement
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("house")||coll.gameObject.CompareTag("Player"))
        {
            GameObject tar=coll.gameObject;
            Debug.Log("Collision occurred with the desired object.");
            Attack zomAttack= GetComponent<Attack>();
            zomAttack.Hit(tar);
        }
    }
    void Update()
    {
        //if not clicked and/or in the players zombie toolkit
        //they move around
        if(!isPlayer){
            //move toward house gameobject?
            //try to destroy
            npcMovement(); 
        }
    }
}
