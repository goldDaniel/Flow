using PathCreation;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;

    private float _distanceTravelledAlongPath = 0;

    void Update()
    {
        var conductor = Conductor.Get();

        if (conductor.isPlaying)
        {
            int beatsToComplete = pathCreator.bezierPath.NumSegments;
            float secondsToTravelPath = conductor.BeatsToSeconds(beatsToComplete);

            float step = pathCreator.path.length / secondsToTravelPath;

            _distanceTravelledAlongPath += step * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelledAlongPath);
        }
    }
}
