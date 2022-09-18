using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatListener : MonoBehaviour
{
    public uint numHalfBeats = 2;

    protected Conductor _conductor;
    protected uint _halfBeatsPassed;

    public virtual void Start()
    {
        _conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        _conductor.RegisterListener(this);
    }

    public virtual void OnHalfBeat()
    {
        _halfBeatsPassed++;
        if (_halfBeatsPassed == numHalfBeats)
        {
            BeatAction();
            _halfBeatsPassed = 0;
        }
    }

    protected abstract void BeatAction();
}
