using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatListener : MonoBehaviour
{
    public BeatType beatType = BeatType.FULL_BEAT;

    public virtual void Start()
    {
        var conductor = Conductor.Get();
        conductor.RegisterListener(this, beatType);
    }

    public abstract void BeatAction();
}
