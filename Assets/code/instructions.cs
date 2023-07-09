using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructions : MonoBehaviour
{

    public GameObject instr;

    // Start is called before the first frame update
    void Start()
    {
        instr.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.I))
        {
            instr.SetActive(true);
        }
        else
        {
            instr.SetActive(false);
        }
    }
}
