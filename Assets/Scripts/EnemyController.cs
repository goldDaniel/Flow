using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BeatListener
{
    public float speed = 0;

    private uint _numHalfBeats;
    private Transform _transform;
    private uint _beatsPassed;

    public override void Start()
    {
        base.Start();

        _transform = GetComponent<Transform>();
        _numHalfBeats = 2;
    }

    public void Update()
    {
        _transform.position += speed * Time.deltaTime * new Vector3(1, 0, 0).normalized;
    }

    public override void OnHalfBeat()
    {
        _beatsPassed++;
        if(_beatsPassed == _numHalfBeats)
        {
            speed = DetermineSpeed(2, new Vector3(0, 0, 0), new Vector3(1, 0, 0));
        }
        if(_beatsPassed == _numHalfBeats + 2)
        {
            speed = 0;
            _beatsPassed = 0;
        }
    }

    public float DetermineSpeed(uint numHalfBeats, Vector3 start, Vector3 stop)
    {
        Vector3 travel = stop - start;
        float time = _conductor.HalfBeatsToSeconds(numHalfBeats);

        return travel.magnitude / time;
    }
}
