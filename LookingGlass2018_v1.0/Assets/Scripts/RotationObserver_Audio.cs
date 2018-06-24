using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObserver_Audio : MonoBehaviour {

    private AudioSource _AS1;
    private AudioSource _AS2;

    [SerializeField] private RotationController _rotationController;

    public AudioClip ac1;
    public AudioClip ac2;

    // Use this for initialization
    void Start()
    {
        _AS1 = gameObject.AddComponent<AudioSource>();
        _AS2 = gameObject.AddComponent<AudioSource>();

        Debug.Assert(_rotationController != null, "RotationController reference is missing");

        _AS1.clip = ac1;
        _AS2.clip = ac2;
        _AS1.loop = true;
        _AS2.loop = true;
        _AS1.Play();
        _AS2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _AS1.volume = Mathf.Lerp(1, 0, _rotationController.rotAngle / 360);
        _AS2.volume = Mathf.Lerp(0, 1, _rotationController.rotAngle / 360);
    }
}
