using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    public AudioClip myAC;

	// Use this for initialization
	void Start () {
		if (GameObjects.Find("BGM").Count > 1)
        {
            Destroy(this.gameObject);

            AudioSource myAS = gameObject.AddComponent<AudioSource>();
            myAS.loop = true;
            myAS.clip = myAC;
            myAC.Play();
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
	}
}
