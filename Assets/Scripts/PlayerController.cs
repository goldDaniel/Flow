using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(4.0f, 32.0f)] public float speed = 4.0f;

    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 dir = new();
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

        _transform.position += speed * Time.deltaTime * dir;
    }
}
