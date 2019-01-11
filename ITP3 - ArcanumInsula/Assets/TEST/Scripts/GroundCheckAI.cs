using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckAI : MonoBehaviour {

    // Use this for initialization
    private float m_movementSpeed = 2.0f;
    private GameObject m_other = null;
    private Transform m_otherTransoform = null;

	void Start ()
    {
        BoxCollider groundCollider = this.GetComponent<BoxCollider>();

        if(groundCollider.isTrigger == false)
        {
            groundCollider.isTrigger = true;
        }
	}
	
	// Update is called once per frame
	void Update () {


        if (stoneEntered == true)
        {
            float step = m_movementSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, m_otherTransoform.position, step);

            Debug.Log("STONE (UPDATECALL)");
        }
        else if(stoneEntered == false)
        {
            Debug.Log("NO STONE (UPDATECALL)");
        }
        else
        {
            Debug.Log("Other than stone!");
        }
      
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ritual_Stone")
        {
            stoneEntered = true;

            m_other = GameObject.FindGameObjectWithTag("ritual_Stone");
            m_otherTransoform = m_other.transform;

            Debug.Log("STONE (Update Call)");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "ritual_Stone")
        {
            stoneEntered = false;

            Debug.Log("NO STONE (Update Call)");
        }
    }

    private bool stoneEntered { get; set; }
}
