using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortEnemy : MonoBehaviour {

    public GameObject m_enemyPrefab;
    private GameObject m_newEnemy;
    private Transform m_portTo;
    private Rigidbody Rig;
    
    public bool doPort { get; set; }

	// Use this for initialization
	void Start ()
    {
        doPort = false;

        m_portTo = this.gameObject.transform;


        m_portTo.rotation = new Quaternion(0,0,0,0);
        m_portTo.localScale = new Vector3(1,1,1);
	}
	
	// Update is called once per frame
	void Update () {

        if (doPort == true)
        {
            Debug.Log("Do Port is set to true");

            m_newEnemy = GameObject.Instantiate(m_enemyPrefab, m_portTo);
            Rig = m_newEnemy.GetComponent<Rigidbody>();
            Rig.freezeRotation = true;

            doPort = false;

            GameObject.Destroy(this);
        }
	}
}
