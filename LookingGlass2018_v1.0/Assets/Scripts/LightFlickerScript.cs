using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerScript : MonoBehaviour {

    Light lt;
    public float minIntensity;
    public float maxIntensity;
    public float minFlicker;
    public float maxFlicker;

    private float timer;
    private float rollover;

	// Use this for initialization
	void Start () {
        lt = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > rollover)
        {
            rollover = UnityEngine.Random.Range(minFlicker, maxFlicker);
            timer = 0;
            lt.intensity = UnityEngine.Random.Range(minIntensity, maxIntensity);
        }
	}
}
