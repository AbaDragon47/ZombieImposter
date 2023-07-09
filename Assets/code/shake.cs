using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    public float trembleMagnitude = 0.1f;   // The maximum distance the object will move
    public float trembleSpeed = 10f;        // The speed of the trembling effect
    public float trembleDuration = 0.5f;    // The duration of the trembling effect

    private Vector3 initialPosition;        // The initial position of the object
    void Start()
    {
        initialPosition = transform.position; 
    }

    public IEnumerator shaking(float t)
    {
        float elapsedTime = 0f;

        while (elapsedTime < trembleDuration)
        {
            // Calculate a random offset based on trembleMagnitude
            Vector3 offset = new Vector3(Random.Range(-trembleMagnitude, trembleMagnitude),
                                        Random.Range(-trembleMagnitude, trembleMagnitude),
                                        0f);

            // Update the object's position
            transform.position = initialPosition + offset;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

    // Reset the object's position to the initial position
    transform.position = initialPosition;      
    }
}
