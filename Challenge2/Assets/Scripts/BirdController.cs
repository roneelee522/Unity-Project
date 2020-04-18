using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float speed;
    private float oldPosition = 0.0f;


    void Start()
    {
        oldPosition = transform.position.x;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));

        if (transform.position.x < oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.x > oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        oldPosition = transform.position.x; // update the variable with the new position so we can chack against it next frame
    }
}