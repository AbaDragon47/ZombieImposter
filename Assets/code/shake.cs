using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    Vector2 startingPos;
    public float speed; //how fast it shakes
    public float amount; //how much it shakes
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        amount = 1.0f;

        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y; 
    }

    public IEnumerator shaking(float t)
    {
        Vector3 lol= new Vector3();;
        float timer=0f;
        while(timer<t)
        {
            Debug.Log("shake");
            timer+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
            lol.x = startingPos.x + (Mathf.Sin(Time.time * speed) * amount );
            //lol.y = startingPos.y + (Mathf.Sin(Time.time * speed) * amount) ;
            gameObject.transform.position = lol;
        }
        gameObject.GetComponent<Transform>().position=startingPos;
        StopCoroutine(shaking(t));
        
    }
}
