using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secDEstroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float sec = 1f;
    void Start()
    {
        Destroy(gameObject,sec);
    }

}
