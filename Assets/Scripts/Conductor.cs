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

    public bool isPlaying;
    public float crotchet;

    private AudioSource _song;

    private float _bpm;
    private float offset;
    private double _startTick;
    private double _sampleRate;
    private double _nextTick;

    private List<BeatEventHandler> _readyEvents = new();

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
        crotchet = 60 / _bpm;
        offset = 0;
        _song = null;
        isPlaying = false;
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

    private void OnSongStart()
    {
        _startTick = AudioSettings.dspTime;
        _sampleRate = AudioSettings.outputSampleRate;
        _nextTick = _startTick * _sampleRate;
        isPlaying = true;
    }

    public void Update()
    {
        lock(_readyEvents)
        {
            foreach(var handler in _readyEvents)
            {
                handler.Update();
            }

            _readyEvents.Clear();
        }
    }

    public void AudioTick()
    {
        foreach(var handler in _eventHandlers)
        {
            if (OnBeat(handler.lastBeat, handler.beatType))
            {
                lock (_readyEvents)
                {
                    _readyEvents.Add(handler);
                    handler.lastBeat = NextBeat(handler.lastBeat, handler.beatType);
                }
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

    public float BeatsToSeconds(float beats)
    {
        return beats * crotchet;
    }

    private float GetSongPosition()
    {
        return (float)(AudioSettings.dspTime - _startTick) - offset;
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
            case (BeatType.FULL_BEAT): return crotchet;
            case (BeatType.HALF_BEAT): return crotchet / 2;
            case (BeatType.QUARTER_BEAT): return crotchet / 4;
            case (BeatType.EIGHTH_BEAT): return crotchet / 8;
            case (BeatType.SIXTEENTH_BEAT): return crotchet / 16;
        }

        return 0;
    }
}
