using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : BeatListener
{
    public Conductor conductor;

    private bool _islitup = false;

    public override void Start()
    {
        base.Start();
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        _halfBeatsPassed = 0;
        numHalfBeats = 2;
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

    protected override void BeatAction()
    {
        Flash();
    }
}
