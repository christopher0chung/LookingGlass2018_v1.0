using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerScript : MonoBehaviour {

   Light lt;

	// Use this for initialization
	void Start () {
        lt = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        lt.range = Random.Range(5, 10);
	}
}
