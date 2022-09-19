using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BeatListener
{
    public float speed = 0;

    private Transform _transform;

    public override void Start()
    {
        base.Start();

        _transform = GetComponent<Transform>();
        numHalfBeats = 2;
    }

    public void Update()
    {
        _transform.position += speed * Time.deltaTime * new Vector3(1, 0, 0).normalized;
    }

    private float DetermineSpeed(int numHalfBeats, Vector3 start, Vector3 stop)
    {
        var conductor = Conductor.Get();

        Vector3 travel = stop - start;
        float time = conductor.HalfBeatsToSeconds(numHalfBeats);

        return travel.magnitude / time;
    }

    protected override void BeatAction()
    {
        if (speed == 0)
        {
            speed = DetermineSpeed(2, new Vector3(0, 0, 0), new Vector3(1, 0, 0));
        }
        else
        {
            speed = 0;
        }
    }
}
