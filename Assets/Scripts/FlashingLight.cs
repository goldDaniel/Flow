using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : BeatListener
{
    private bool _islitup = false;

    public override void Start()
    {
        base.Start();
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
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

    public override void BeatAction()
    {
        Flash();
    }
}
