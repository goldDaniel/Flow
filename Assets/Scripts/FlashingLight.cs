using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : BeatListener
{
    public Conductor conductor;
    public uint numHalfBeats;

    private bool _islitup = false;
    private uint _beatsPassed;

    public override void Start()
    {
        base.Start();
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        _beatsPassed = 0;
        numHalfBeats = 2;
    }
    public override void OnHalfBeat()
    {
        _beatsPassed++;
        if (_beatsPassed == numHalfBeats)
        {
            Flash();
            _beatsPassed = 0;
        }
    }

    private void Flash()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if(_islitup)
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.clear;
        }

        _islitup = !_islitup;
    }
}
