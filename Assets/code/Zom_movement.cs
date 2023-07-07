using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zom_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPlayer;
    void Start()
    {
        isPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if not clicked and/or in the players zombie toolkit
        //they move around
        if(!isPlayer){
            //move toward house gameobject?
            //try to destroy
        }
    }
}
