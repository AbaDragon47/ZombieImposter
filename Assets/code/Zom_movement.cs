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

    //timer b4 zombie switches back
    IEnumerator timeLeft(float timeLet)
    {
        //after time is up
        yield return new WaitForSeconds(timeLet);
        Debug.Log("Times up");
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().isActive=true;
        Destroy(this.gameObject);
        StopCoroutine(timeLeft(timeLet));
    }

    //starts ^^^ frame by frame
    void imposter(){
        //Debug.Log("heard event");
        if(isPlayer)
            StartCoroutine(timeLeft(20f)); 
        target=GameObject.Find("Imposter").transform; //targeting imposter zombie
    }
    // i need to fix ranMove so ignore
    /*void ranMove(){
        if (Random.Range(0f, 1f) < 0.05f)
            GetComponent<Rigidbody2D>().velocity= Vector2.left*moveSpeed;
        else
           GetComponent<Rigidbody2D>().velocity= Vector2.right*moveSpeed;
    }*/
    // Update is called once per frame
    void npcMovement()
    { //to make zom move and stumble 
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
    {  //player movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        zrb.velocity = new Vector2(horizontalInput, verticalInput) * moveSpeed;   
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        //Debug.Log(coll.gameObject.CompareTag("house"));
        if (coll.gameObject.CompareTag("house")||coll.gameObject.CompareTag("Player")||coll.collider.gameObject.name.Equals("Imposter"))
        {
            //checking what zombie hit
            GameObject tar=coll.gameObject;
            //Debug.Log("Collision occurred with the desired object."+tar.transform);
            Attack zomAttack= GetComponent<Attack>();
            zomAttack.Hit(tar);
        }
    }
    void Update()
    {
        //Debug.Log(target.name);
        if(isPlayer)
        {
            //Debug.Log("zom is supposed to move");
            target=null;
            movement();
            //maybe add animation of pushing?
        }

        if(target==null)
        {   
            
            if(GameObject.Find("Imposter")!=null)
                target=GameObject.Find("Imposter").transform;
            else if(Physics2D.Distance(gameObject.GetComponent<Collider2D>(),GameObject.FindWithTag("Player").GetComponent<Collider2D>()).distance<15f)
                 target=GameObject.FindWithTag("Player").transform;
            else
                target=GameObject.FindWithTag("house").transform;
                      
        }
           
        //if not clicked and/or in the players zombie toolkit
        //they move around
        if(!isPlayer)
        {
            if(GameObject.Find("Imposter")!=null)
                target=GameObject.Find("Imposter").transform;
            else if(Physics2D.Distance(gameObject.GetComponent<Collider2D>(),GameObject.FindWithTag("Player").GetComponent<Collider2D>()).distance<7f)
                 target=GameObject.FindWithTag("Player").transform;
            else
                target=GameObject.FindWithTag("house").transform;
            //move toward house gameobject?
            //try to destroy
            npcMovement();
             
        }
        if(gameObject==null){
            PlayerController.susBehavior.RemoveListener(imposter);
            Destroy(this.gameObject);
        }
            
        
    }
}
