using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductorCoordinator: MonoBehaviour
{
    public AudioSource audioSource; 

    void Start()
    {
        var conductor = Conductor.Get();
        conductor.SetSong(audioSource);
        conductor.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Conductor.Get().Update();
    }
}
