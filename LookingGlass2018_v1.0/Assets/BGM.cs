using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    public AudioClip myAC;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectsWithTag("BGM").Length > 1)
        {
            Destroy(this.gameObject);

            AudioSource myAS = gameObject.AddComponent<AudioSource>();
            myAS.loop = true;
            myAS.clip = myAC;
            myAS.Play();
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
	}
}
