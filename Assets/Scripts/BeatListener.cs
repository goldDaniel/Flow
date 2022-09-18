using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatListener : MonoBehaviour
{
    protected Conductor _conductor;

    public virtual void Start()
    {
        _conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        _conductor.RegisterListener(this);
    }

    public abstract void OnHalfBeat();
}
