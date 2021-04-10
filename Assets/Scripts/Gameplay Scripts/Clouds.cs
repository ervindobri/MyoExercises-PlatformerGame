using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float speed = 0.005f;
    private bool left; //left or right moving
    public float bound = 2.3f;

    void Update()
    {
        Movement();
    }
    void Movement()
    {
  
        if (left)
        {

            Vector3 pos = transform.position;
            pos.x += speed;
            transform.position = pos;
            if (transform.position.x > bound + 0.13f)
            {
                left = false;
                bound = Random.Range(transform.position.x - 4f, transform.position.x + 4f);
            }
            //print("Left:" + left);
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x -= speed;
            transform.position = pos;
            if (transform.position.x < bound + 0.5f)
            {
                left = true;
                bound = Random.Range(transform.position.x - 4f, transform.position.x + 4f);
            }
            //print("Left:" + left);
        }
    }
}
