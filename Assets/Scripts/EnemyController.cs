using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BeatListener
{
    public float speed = 0.5f;

    private Transform _transform;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        _transform = GetComponent<Transform>();
    }

    public override void OnHalfBeat()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        _transform.position += speed * new Vector3(1, 0, 0);
    }
}
