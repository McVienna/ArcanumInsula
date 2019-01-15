using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour {

    public AudioClip footsteps_sound;
    public AudioSource footstep_source;
    CharacterController cc;

    // Use this for initialization
    void Start()
    {
        footstep_source = this.gameObject.GetComponent<AudioSource>();
        footstep_source.clip = footsteps_sound;
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f)
        {
            footstep_source.Play();
        }

    }
}

