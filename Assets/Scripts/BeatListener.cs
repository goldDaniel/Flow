using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatListener : MonoBehaviour
{
    protected BeatType _beatType = BeatType.FULL_BEAT;

    public virtual void Start()
    {
        _beatType = BeatType.FULL_BEAT;

        var conductor = Conductor.Get();
        conductor.RegisterListener(this, _beatType);
    }

    public abstract void BeatAction();
}
