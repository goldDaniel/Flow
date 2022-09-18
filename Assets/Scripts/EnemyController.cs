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
        _transform.position = DetermineSpeed(2, new Vector3(0, 0, 0), new Vector3(1, -1, 0)) * Time.deltaTime * new Vector3(1, 0, 0);
    }

    public float DetermineSpeed(uint numHalfBeats, Vector3 start, Vector3 stop)
    {
        Vector3 travel = stop - start;
        float time = _conductor.HalfBeatsToSeconds(numHalfBeats);

        return travel.magnitude / time;
    }
}
