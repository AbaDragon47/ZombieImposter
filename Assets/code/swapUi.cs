using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapUi : MonoBehaviour
{
    public GameObject kevinswap;
    public GameObject zombieswap;
    public bool iskevin;
    public bool iszombie;


    // Start is called before the first frame update
    void Start()
    {
        kevinswap.SetActive(false);
        zombieswap.SetActive(false);
        iskevin = false;
        iszombie = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //pullswap();

        if(Input.GetKey(KeyCode.Tab))
        {
            if(iskevin == true)
            {
                iskevin = false;
                iszombie = true;
            }
            if(iszombie)
            {
                iskevin = true;
                iszombie = false;
            }
            else
            {
                iskevin = true;
                iszombie = false;
            }
        }



        if(iskevin)
        {
            kevinswap.SetActive(true);
            zombieswap.SetActive(false);
        }
        else if(iszombie)
        {
            kevinswap.SetActive(false);
            zombieswap.SetActive(true);
        }
        else
        {
            kevinswap.SetActive(false);
            zombieswap.SetActive(false);
        }
    }
    void pullswap()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            if(iskevin)
            {
                iskevin = false;
                iszombie = true;
            }
            if(iszombie)
            {
                iskevin = true;
                iszombie = false;
            }
            else
            {
                iskevin = true;
                iszombie = false;
            }
        }
        
    }

    
}
