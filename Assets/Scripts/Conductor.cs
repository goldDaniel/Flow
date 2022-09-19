using System.Collections.Generic;
using UnityEngine;

public enum BeatType
{
    FULL_BEAT,
    HALF_BEAT,
    QUARTER_BEAT,
    EIGHTH_BEAT,
    SIXTEENTH_BEAT
}

public class Conductor
{
    private class BeatEventHandler
    {
        public float lastBeat;
        public readonly BeatType beatType;
        public List<BeatListener> listeners = new();

        public BeatEventHandler(BeatType beatType) { this.beatType = beatType; }

        public void RegisterListener(BeatListener listener)
        {
            listeners.Add(listener);
        }

        public void Update()
        {
            foreach (var listener in listeners)
            {
                listener.BeatAction();
            }
        }
    }

    private AudioSource _song;

    private float _bpm;
    private float _crotchet;
    private float offset;
    private double _dspTimeSong;

    private BeatEventHandler[] _eventHandlers =
    {
        new BeatEventHandler(BeatType.FULL_BEAT),
        new BeatEventHandler(BeatType.HALF_BEAT),
        new BeatEventHandler(BeatType.QUARTER_BEAT),
        new BeatEventHandler(BeatType.EIGHTH_BEAT),
        new BeatEventHandler(BeatType.SIXTEENTH_BEAT)
    };

    private static Conductor _instance;

    private Conductor()
    {
        Reset();
    }

    public void Reset()
    {
        _bpm = 156;
        _crotchet = 60 / _bpm;
        offset = 0;
        _dspTimeSong = 0;
        _song = null;
    }

    public static Conductor Get()
    {
        if(_instance == null)
        {
            _instance = new();
        }

        return _instance;
    }

    public void SetSong(AudioSource source)
    {
        _song = source;
    }

    public void Play()
    {
        _song.Play();
        OnSongStart();
    }

    public void Update()
    {
        foreach(var handler in _eventHandlers)
        {
            if (OnBeat(handler.lastBeat, handler.beatType))
            {
                handler.Update();
                handler.lastBeat = NextBeat(handler.lastBeat, handler.beatType);
            }
        }
    }

    public void RegisterListener(BeatListener listener, BeatType beatType)
    {
        foreach(var handler in _eventHandlers)
        {
            if(handler.beatType == beatType)
            {
                handler.RegisterListener(listener);
            }
        }
    }

    private void OnSongStart()
    {
        _dspTimeSong = AudioSettings.dspTime;
    }

    public float BeatsToSeconds(float beats)
    {
        return beats * _crotchet;
    }

    private float GetSongPosition()
    {
        return (float)(AudioSettings.dspTime - _dspTimeSong) - offset;
    }

    private bool OnBeat(float check, BeatType beatType)
    {
        return check + GetBeat(beatType) <= GetSongPosition();
    }

    private float NextBeat(float time, BeatType beatType)
    {
        return time + GetBeat(beatType);
    }

    private float GetBeat(BeatType beatType)
    {
        switch (beatType)
        {
            case (BeatType.FULL_BEAT): return _crotchet;
            case (BeatType.HALF_BEAT): return _crotchet / 2;
            case (BeatType.QUARTER_BEAT): return _crotchet / 4;
            case (BeatType.EIGHTH_BEAT): return _crotchet / 8;
            case (BeatType.SIXTEENTH_BEAT): return _crotchet / 16;
        }

        return 0;
    }
}
