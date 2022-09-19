using UnityEngine;

public abstract class BeatListener : MonoBehaviour
{
    public uint numHalfBeats = 2;

    protected uint _halfBeatsPassed;

    public virtual void Start()
    {
        var conductor = Conductor.Get();
        conductor.RegisterListener(this);
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
