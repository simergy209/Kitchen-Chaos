using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Play the sizzle sound just when the meat is on the stove and not burned, used with the OnStateChange event from StoveCounte class
    private void Start()
    {
        stoveCounter.OnStateChange += (sender, args) =>
        {
            if(args.state == StoveCounter.State.Fried || args.state == StoveCounter.State.Frying)
                audioSource.Play();
            else
                audioSource.Pause();
        };

    }
}
