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

    //imposter stuff
    public bool investigate;

    void Start()
    {
        investigate=false;

        isPlayer = false;
        target=GameObject.FindWithTag("house").transform;
        zrb= GetComponent<Rigidbody2D>();
        isPaused=false;
        pauseTimer=0f;
        currentPauseDuration=0;

        //Imposter stuff event?
        PlayerController.susBehavior.AddListener(imposter);
        //PlayerController.sus.AddListener(timeLeft());
    }

    IEnumerator timeLeft(float time, float timeLet){
        Debug.Log(Time.time - time);
        /*if(timeLeft<0)
            
        timeLeft=(Time.time-time)-timeLeft;*/
        //after time is up
        yield return new WaitForSeconds(timeLet);
        //PlayerController.sus.RemoveAllListeners();
        StopCoroutine(timeLeft(time,timeLet));
    }
    void imposter(){
        StartCoroutine(timeLeft(Time.time, 10f));
        if(!isPlayer)
            target=GameObject.Find("Imposter").transform;
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
    void movement()
    {  
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        zrb.velocity = new Vector2(horizontalInput, verticalInput) * moveSpeed;   
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
        if(gameObject==null)
            PlayerController.susBehavior.RemoveListener(imposter);

        if(target==null)
            target=GameObject.FindWithTag("house").transform;
        //if not clicked and/or in the players zombie toolkit
        //they move around
        if(!isPlayer){
            //move toward house gameobject?
            //try to destroy
            npcMovement(); 
        }
        else if(isPlayer){
            movement();
            //maybe add animation of pushing?
        }
    }
}
