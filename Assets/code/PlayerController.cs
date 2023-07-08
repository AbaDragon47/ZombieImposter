using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour
{
    GameObject swap;
    public Rigidbody2D rb;
    public float moveSpeed;
    public bool isActive;
    public GameObject[] zoms;
    public float minAddDist;

    //to make sure no infinite loop
    Coroutine myRoutine;

    //lemme create an event for imposter
    public static UnityEvent susBehavior;
    

    // Start is called before the first frame update
    void Start()
    {   
        susBehavior= new UnityEvent();


        //maybe figure out a way to draw the radius?
        rb= GetComponent<Rigidbody2D>();
        minAddDist=2.2f;
        zoms= new GameObject[2];
        isActive = true;
        moveSpeed = 5f;
        zoms[0]= gameObject;

        
    }
    //maybe include method as to how logic works
    void movement()
    {  
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontalInput, verticalInput) * moveSpeed;   
    }

    IEnumerator Select()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
           
            int rotate = (int)Input.GetAxisRaw("Horizontal");
            int currentObject=0;
            if(Input.GetKeyDown(KeyCode.Return))
                StopCoroutine(Select());
            if(Input.anyKey)
            {
                currentObject+=rotate;
                currentObject%=zoms.Length;
                currentObject=currentObject<0?zoms.Length-1:currentObject;
            }  
            
            
            //human player isnt active
            isActive = false;
            swap=zoms[currentObject];
             Debug.Log(swap.name);
            GameObject zomzom= swap;
            Debug.Log(zomzom.name);
            zomzom.name="Imposter";
            if(zomzom!=gameObject){
                zomzom.GetComponent<Zom_movement>().isPlayer=true;
                susBehavior.Invoke(); 
            }
        }
    }

    GameObject addZoms(GameObject[] allZoms)
    {
        //add to players list of zombies
        if(allZoms.Length==0)
            throw new ArgumentException("no zoms at all");
        float distance=(transform.position-allZoms[0].transform.position).magnitude;
        GameObject closest=gameObject;
        foreach (GameObject zombie in allZoms)
        {
            Vector2 origin = transform.position;
            Vector2 direction = (Vector2)zombie.transform.position - origin;

            distance= direction.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(origin,direction,distance);

            if(hit.collider != null){
               // Debug.Log("Distance"+distance);
                float wow= (float)((closest.transform.position-transform.position).magnitude);
                //Debug.Log(wow);
                if(distance<minAddDist&& closest==gameObject)
                    closest=zombie;
                else if(distance<minAddDist&&closest!=gameObject){
                    if(distance<wow)
                        closest=zombie;
                }            
            }
                
        }
        if(closest==gameObject)
            throw new ArgumentException("none in viscinity");
        return closest;
        


    }
    

    // Update is called once per frame
    void Update()
    {
        if(isActive){
        
            movement();
            //choosing zoms
            if(Input.GetKeyDown(KeyCode.Z))
            {   
                //use Linq vvv
                int index=Array.FindIndex(zoms, i=> i==null);
                if(index>=0)
                    zoms[index]=addZoms(GameObject.FindGameObjectsWithTag("Zombie"));
            }
            //if tab is pressed you cycle thro all the zoms in array
            //with arrow or a and d keys to become zombie
            //is active turns off
            if(Input.GetKey(KeyCode.Tab))
            {
                if(myRoutine==null)
                    myRoutine= StartCoroutine(Select());

                
            //whichever player is active cam pans to them

            }
        }
        else{
            throw new ArgumentException("Human is currently a npc");
        }

        
    }
}
