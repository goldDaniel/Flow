using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _transform;

    [Range(4.0f, 32.0f)] public float speed = 4.0f;

    
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 dir = new Vector3();
        if(Input.GetKey("left"))
        {
            dir.x -= 1;
        }
        if (Input.GetKey("right"))
        {
            dir.x += 1;
        }
        if (Input.GetKey("up"))
        {
            dir.y += 1;
        }
        if (Input.GetKey("down"))
        {
            dir.y -= 1;
        }

        if(dir.sqrMagnitude > 0)
        {
            dir.Normalize();
        }

        _transform.position += dir * speed * Time.deltaTime;
    }
}
