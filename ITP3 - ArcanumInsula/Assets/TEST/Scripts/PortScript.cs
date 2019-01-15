using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortScript : MonoBehaviour {

    public GameObject m_enemyPrefab;
    private GameObject m_PortPoint;
    private GameObject m_enemy;
    private Transform m_portToTransform;
    GameObject Reference;
    PortEnemy ReferenceScript;
    private GameObject Enemy;

	// Use this for initialization
	void Start () {
        triggeredPort = false;
        m_portToTransform = null;
        Reference = GameObject.FindGameObjectWithTag("port_point");
        ReferenceScript = Reference.GetComponent<PortEnemy>();

	}
	
	// Update is called once per frame
	void Update () {

		if(triggeredPort == true)
        {
            Port(m_enemy, this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            Vector3 temp;
            m_PortPoint = GameObject.FindGameObjectWithTag("port_point");
            m_enemy = GameObject.FindGameObjectWithTag("enemy");
            m_portToTransform = m_PortPoint.transform;

            temp = m_portToTransform.position;
            temp.z = 0.0f;
            m_portToTransform.position = temp;

            m_portToTransform.rotation = new Quaternion(0, 0, 0, 0);
            triggeredPort = true;
        }
    }

    private void Port(GameObject enemy, GameObject portTo)
    {
        //Get Variables to meassure the distance between enemy and portStone
        int enemyX = 0;
        int enemyY = 0;

        int ritualX = 0;
        int ritualY = 0;

        Vector2 enemy_XY;
        Vector2 ritual_XY;

        //Extract the dimension values in order to cast them as int for comprasion
        enemyX = (int)enemy.transform.position.x;
         enemyY = (int)enemy.transform.position.y;

        ritualX = (int)portTo.transform.position.x;
        ritualY = (int)portTo.transform.position.y;

        enemy_XY = new Vector2(enemyX, enemyY);
        ritual_XY = new Vector2(ritualX, ritualY);

        if (enemy_XY == ritual_XY)
        {
            triggeredPort = false;
            Destroy(m_enemy);
            Debug.Log("X:" + m_portToTransform.position.x + " Y: " + m_portToTransform.position.y + " Z: " + m_portToTransform.position.z);
            Destroy(this.gameObject);
            ReferenceScript.doPort = true;
        }
    }

    private bool triggeredPort { get; set; }
}
