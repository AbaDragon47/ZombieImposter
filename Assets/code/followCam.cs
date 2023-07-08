using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class followCam : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera virtualCamera;
    void Start()
    {
        virtualCamera.Follow= GameObject.Find("Kevin").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Imposter")!=null)
            virtualCamera.Follow= GameObject.Find("Imposter").transform;
        else    
             virtualCamera.Follow= GameObject.Find("Kevin").transform;
    }
}
