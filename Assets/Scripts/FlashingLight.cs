using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : BeatListener
{
    private SpriteRenderer _sprite;
    private bool _islitup = false;

    public override void Start()
    {
        base.Start();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = Color.white;
    }

    private void Flash()
    {
        if(_islitup)
        {
            _sprite.color = Color.yellow;
        }
        else
        {
            _sprite.color = Color.clear;
        }

        _islitup = !_islitup;
    }

    public override void BeatAction()
    {
        Flash();
    }
}
