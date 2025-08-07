using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChange += (object sender, StoveCounter.OnStateChangeEventArgs args) => {
            bool showVisual = (args.state == StoveCounter.State.Frying || args.state == StoveCounter.State.Fried);
            stoveOnGameObject.SetActive(showVisual);
            particlesGameObject.SetActive(showVisual);

        };
    }
}
