using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour {

    public Vector3 Jump;
    public float JumpForce = 2;

    public bool IsGrounded;

    Rigidbody Rig;

    void Start()
    {
        Rig = GetComponent<Rigidbody>();
        Jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay(Collision collision)
    {
        IsGrounded = true;
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f; // Time.deltaTime nachlesen https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);

        if(Input.GetKey("space") && IsGrounded)
        {
            Rig.AddForce(Jump * JumpForce, ForceMode.Impulse);
            IsGrounded = false;
        }

    }
}
