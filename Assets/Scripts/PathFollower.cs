using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public Conductor conductor;

    private float _distanceTravelledAlongPath = 0;

    void Update()
    {
        int beatsToComplete = pathCreator.bezierPath.NumSegments;
        float secondsToTravelPath = conductor.BeatsToSeconds(beatsToComplete);

        float step = pathCreator.path.length / secondsToTravelPath / 2;

        _distanceTravelledAlongPath += step * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelledAlongPath);
    }
}
