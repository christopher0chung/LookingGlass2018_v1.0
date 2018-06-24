using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastForward : MonoBehaviour {

    TextMesh myTM;

    float timer;

    int displayedTime;

    void Start () {
        myTM = GetComponent<TextMesh>();
        timer = 15.99f;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        displayedTime = (int)timer;

        myTM.text = displayedTime.ToString();

        if (timer <= 0)
        {
            SceneManager.LoadScene(0);
        }
	}
}
