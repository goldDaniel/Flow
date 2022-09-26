using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductorCoordinator: MonoBehaviour
{
    public AudioSource audioSource;

    private double _startTick;
    private double _sampleRate;
    private double _nextTick;

    void Start()
    {
        var conductor = Conductor.Get();
        conductor.SetSong(audioSource);
        conductor.Play();

        _startTick = AudioSettings.dspTime;
        _sampleRate = AudioSettings.outputSampleRate;
        _nextTick = _startTick * _sampleRate;
    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (!Conductor.Get().isPlaying) return;

        double samplesPerTick = _sampleRate * Conductor.Get().crotchet;
        double sample = AudioSettings.dspTime * _sampleRate;
        int dataLen = data.Length / channels;

        for (int n = 0; n < dataLen; n++)
        {
            // for (int i = 0; i < channels; i++)  {data[n * channels + i] = ...} sets data per channel
            while (sample + n >= _nextTick)
            {
                _nextTick += samplesPerTick;
                Conductor.Get().AudioTick();
                StartCoroutine(ConductorUpdate());
            }
        }
    }

    private IEnumerator ConductorUpdate()
    {
        Conductor.Get().Update();
        yield return null;
    }
}
