using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatListener : MonoBehaviour
{
    public Conductor _conductor;
    protected BeatType _beatType = BeatType.FULL_BEAT;

    public virtual void Start()
    {
        _conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        _beatType = BeatType.FULL_BEAT;
        _conductor.RegisterListener(this, _beatType);
    }

    public abstract void BeatAction();
}
