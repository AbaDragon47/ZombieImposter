using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public bool isActive;
    public GameObject[] zoms;
    public float minAddDist;

    // Start is called before the first frame update
    void Start()
    {
        //maybe figure out a way to draw the radius?
        rb= GetComponent<Rigidbody2D>();
        minAddDist=5f;
        zoms= new GameObject[2];
        isActive = true;
        moveSpeed = 5f;
        zoms[0]= gameObject;
        
    }
    //maybe include method as to how logic works
    void movement()
    {
        if(isActive){
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(horizontalInput, verticalInput) * moveSpeed; 
        }
    }
    GameObject Select()
    {
        float rotate = Input.GetAxis("Horizontal");
        int currentObject=0;
        while(!Input.GetKeyDown(KeyCode.Return))
        {
            if(rotate>0)
            {
                currentObject+=1;
                currentObject%=zoms.Length;
            }
            if(rotate<0)
            {
                currentObject-=1;
                currentObject=currentObject<0?zoms.Length-1:currentObject;
            }
        }
        //current player isnt active
        isActive = false;
        return zoms[currentObject];
        
    }

    GameObject addZoms(GameObject[] allZoms)
    {
        GameObject closest= allZoms[0];
        foreach (GameObject zombie in allZoms)
        {
            float distance = Vector2.Distance(transform.position,zombie.transform.position);
            if(distance< minAddDist){
                if(distance<Vector2.Distance(transform.position,closest.transform.position)){
                    closest=zombie;
                }      
            }
        }
        return closest;
    }
    

    // Update is called once per frame
    void Update()
    {
        movement();
        //choosing zoms
        if(Input.GetKeyDown(KeyCode.Z))
        {   
            //use Linq vvv
            int index=Array.FindIndex(zoms, i=> i==null);
            zoms[index]=addZoms(GameObject.FindGameObjectsWithTag("Zombie"));
        }
        //if tab is pressed you cycle thro all the zoms in array
        //with arrow or a and d keys to become zombie
        //is active turns off
       // if(Input.GetKeyDown(KeyCode.Tab))
            //Select().isActive;==> it doesnt like this? idk why?
        //whichever player is active cam pans to them


    }
}
