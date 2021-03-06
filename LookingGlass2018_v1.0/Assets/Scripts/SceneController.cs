﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public RotationController myRC;

    public int nextIndex;

    private float timer;

    void Start()
    {
        myRC = gameObject.GetComponent<RotationController>();
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > .15f)
        {
            if (myRC.button_state)
            {
                SceneManager.LoadScene(nextIndex);
            }
        }
    }
}

