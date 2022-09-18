using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public AudioSource Song;

    private float _bpm;
    private float _crotchet;
    private float offset;
    private double _dspTimeSong;
    private float _lastBeat;

    private List<BeatListener> _listeners = new();

    public void Start()
    {
        _bpm = 156;
        _crotchet = 60 / _bpm;
        offset = 0;
        _dspTimeSong = 0;
        _lastBeat = 0;

        Song = GetComponent<AudioSource>();
        Song.Play();
        OnSongStart();
    }

    public void Update()
    {
        if(OnHalfBeat(_lastBeat))
        {
            foreach(var listener in _listeners)
            {
                listener.OnHalfBeat();
            }

            _lastBeat = IncrementLastBeat(_lastBeat);
        }
    }

    public void RegisterListener(BeatListener listener)
    {
        _listeners.Add(listener);
    }

    public void OnSongStart()
    {
        _dspTimeSong = AudioSettings.dspTime;
    }

    public float GetSongPosition()
    {
        return (float)(AudioSettings.dspTime - _dspTimeSong) * Song.pitch - offset;
    }

    public bool OnHalfBeat(float check)
    {
        return check + _crotchet / 2 < GetSongPosition();
    }

    public float IncrementLastBeat(float time)
    {
        return time + _crotchet / 2;
    }
}
