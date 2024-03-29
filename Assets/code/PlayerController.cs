using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class PlayerController : MonoBehaviour
{
    bool needsToSwap;
    bool tab;

    GameObject swap;

   
    

    [SerializeField] private GameObject floatText;
    public Rigidbody2D rb;
    public float moveSpeed;
    public bool isActive;
    public GameObject[] zoms;
    public float minAddDist;

    //for swapping mechanic
    int currentObject=0;

    //to make sure no infinite loop
    Coroutine myRoutine;

    //lemme create an event for imposter
    public static UnityEvent susBehavior;
    

    // Start is called before the first frame update
    void Start()
    {   
        

        susBehavior= new UnityEvent();
        tab=false;
        needsToSwap=false;

        //maybe figure out a way to draw the radius?
        rb= GetComponent<Rigidbody2D>();
        minAddDist=3.2f;
        zoms= new GameObject[2];
        isActive = true;
        moveSpeed = 4f;
        zoms[0]= gameObject;

        

        
    }
    void show(string text)
    {
        if(floatText)
        {
            GameObject prefa = Instantiate(floatText,gameObject.transform.position,Quaternion.identity);
            prefa.GetComponentInChildren<TextMesh>().text=text;
        }
    }

    //maybe include method as to how logic works
    void movement()
    {  
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontalInput, verticalInput) * moveSpeed;   
    }

    /*IEnumerator Select()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
           
            int rotate = (int)Input.GetAxisRaw("Horizontal");
            int currentObject=0;
            if(Input.GetKey(KeyCode.Return))
                StopCoroutine(Select());
            if(Input.anyKey)
            {
                currentObject+=rotate;
                currentObject%=zoms.Length;
                currentObject=currentObject<0?zoms.Length-1:currentObject;
            }  
            
            
            swap=zoms[currentObject];
            
        }
    }*/
    GameObject Select()
    {
        if(needsToSwap)
        {
            //int rotate = (int)Input.GetAxisRaw("Horizontal");
            
            if(Input.GetKey(KeyCode.Return))
                needsToSwap=false;
            if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
            {
                currentObject+=1;
                currentObject=currentObject>zoms.Length-1?0:currentObject;
            }
            if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
            {
                currentObject-=1;
                currentObject=currentObject<0?zoms.Length-1:currentObject;
            } 
            Debug.Log(currentObject);
            
            
            swap=zoms[currentObject];

           // Debug.Log(swap.name);
            show(swap.name);

        }
        return swap;
       
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
        if(closest==gameObject){
            show("none");
        }
            
        else{
            show("+1");
        }
        Debug.Log("work");
            
        return closest;
        


    }
    

    // Update is called once per frame
    void Update()
    {

      


        if(GameObject.Find("Imposter")==false)
            isActive=true;
        if(isActive){
            //gameObject.GetComponent<CinemachineVirtualCamera>().enabled=true;
            movement();
            //choosing zoms
            if(Input.GetKey(KeyCode.Z))
            {   
                //use Linq vvv
                int index=Array.FindIndex(zoms, i=> i==null);
                if(index>=0)
                {
                    GameObject x=addZoms(GameObject.FindGameObjectsWithTag("Zombie"));
                    Debug.Log(x.name);
                    if(x!=gameObject)
                        zoms[index]=x;
                }
                    
                else
                {
                    GameObject x=addZoms(GameObject.FindGameObjectsWithTag("Zombie"));
                    Debug.Log("nope"+x.name);
                    //addZoms(GameObject.FindGameObjectsWithTag("Zombie"));
                }
                
            }
            //if tab is pressed you cycle thro all the zoms in array
            //with arrow or a and d keys to become zombie
            //is active turns off
            if(Input.GetKey(KeyCode.Tab))
                tab=true;
            //Debug.Log(tab);
            if(tab)
            {
               /* if(myRoutine==null)
                    myRoutine= StartCoroutine(Select());
                else
                {*/
                needsToSwap=true;
                GameObject zomzom= Select(); 
                if(zomzom!=gameObject&& !needsToSwap)
                {
                    Debug.Log(zomzom.name);
                    zomzom.name="Imposter";
                    //human player isnt active
                    isActive = false;
                    zomzom.GetComponent<Zom_movement>().isPlayer=true;
                    // zomzom.GetComponent<CinemachineVirtualCamera>().enabled=true;
                    tab=false;
                    //susBehavior.Invoke();
                }
                //}
                

                
            //whichever player is active cam pans to them

            }
        }
        else{
         //   gameObject.GetComponent<CinemachineVirtualCamera>().enabled=false;
            rb.velocity= new Vector2(0,0); 
            susBehavior.Invoke();
            //throw new ArgumentException("Human is currently a npc");
        }

        
    }
}
