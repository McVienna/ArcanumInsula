using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour {

    public AudioClip backMusic;
    AudioSource fxSound;

	// Use this for initialization
	void Start () {
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
